using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Windows.Forms;


namespace EquellaMetadataUtility
{
    public partial class ViewItemXMLForm : Form
    {
        public MainForm mainForm;
        public string XMLtext;
        XmlDocument itemXMLDoc = new XmlDocument();
        XmlDocument itemXMLFilteredDoc = new XmlDocument();
        public string itemID;
        public int itemVersion;
        public bool copyToStaging;
        public EquellaClient equellaClient;
        public bool dirtyUI = false;
        public bool editModeEnabled = false;
        private bool flippingCheckbox = false;

        private string[] systemNodes = {
                                                "/xml/item/name",
                                                "/xml/item/description",
                                                "/xml/item/@itemstatus",
                                                "/xml/item/@id",
                                                "/xml/item/@version",
                                                "/xml/item/@key",
                                                "/xml/item/@itemdefid",
                                                "/xml/item/@moderating",
                                                "/xml/item/datecreated",
                                                "/xml/item/datemodified",
                                                "/xml/item/dateforindex",
                                                "/xml/item/owner",
                                                "/xml/item/rating",
                                                "/xml/item/badurls",
                                                "/xml/item/moderation",
                                                "/xml/item/newitem",
                                                "/xml/item/staging",
                                                "/xml/item/attachments",
                                                "/xml/item/navigationNodes",
                                                "/xml/item/rights",
                                                "/xml/item/url",
                                                "/xml/item/history"
                                            };

        public ViewItemXMLForm()
        {
            InitializeComponent();
            ViewItemXMLForm_Resize();
        }

        private void ViewItemXMLForm_Load(object sender, EventArgs e)
        {
            this.Text = "Viewing " + itemID + "/" + itemVersion.ToString();
            if (mainForm.systemMetadataHidden) chkHideSystemMetadata.Checked = true;
            RefreshXML(true);
        }

        private void ViewItemXMLForm_Resize(object sender, EventArgs e)
        {
            ViewItemXMLForm_Resize();
        }

        private void ViewItemXMLForm_Resize()
        {
            int buttonMargin = 16;

            fctbItemXml.Width = this.Width - 18;
            fctbItemXml.Height = this.ClientSize.Height - fctbItemXml.Top - btnClose.Height - buttonMargin;

            btnClose.Left = this.ClientSize.Width - btnClose.Width - buttonMargin / 2;
            btnClose.Top = this.ClientSize.Height - btnClose.Height - buttonMargin / 2;
            btnEditSave.Top = btnClose.Top;
            btnEditSave.Left = this.Width - btnEditSave.Width - btnClose.Width - buttonMargin * 2;
            btnRefresh.Top = btnClose.Top;
            btnGoUp.Top = btnClose.Top + buttonMargin / 4;
            btnGoDown.Top = btnGoUp.Top;
        }

        public void RefreshXML(bool refreshFromServer)
        {
            int cursorPos = getLineNumberOfCaret();
            int selectionStart = fctbItemXml.SelectionStart;

            if (refreshFromServer)
            {
                try
                {
                    mainForm.login();
                    equellaClient = mainForm.equellaClient;

                    string unindentedItemXML = equellaClient.getItem(itemID, itemVersion, "");

                    equellaClient.logout();

                    // load the filtered and unfiltered XML
                    itemXMLDoc.LoadXml(unindentedItemXML);
                    updateFilteredXmlfromUnfiltered(true);
                }
                catch (Exception err)
                {
                    
                    MessageBox.Show(err.Message, "Error retrieving item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xw.Formatting = Formatting.Indented;

            if (chkHideSystemMetadata.Checked)
                itemXMLFilteredDoc.WriteTo(xw);
            else
                itemXMLDoc.WriteTo(xw);

            string indentedItemXMLString = sw.ToString();
            fctbItemXml.Text = indentedItemXMLString;
            fctbItemXml.SelectionStart = cursorPos;

            fctbItemXml.Navigate(cursorPos);
            fctbItemXml.SelectionStart = selectionStart;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                RefreshXML(true);
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewItemXMLForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F5 )&&(!editModeEnabled))
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    RefreshXML(true);
                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception err)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void viewXmlFind_Click(object sender, EventArgs e)
        {
            if(editModeEnabled)
                fctbItemXml.ShowReplaceDialog();
            else
                fctbItemXml.ShowFindDialog();
        }

        private void updateFilteredXmlfromUnfiltered(bool copy)
        {
            if (copy)
            {
                // copy XML from unfiltered XML
                itemXMLFilteredDoc = (XmlDocument)itemXMLDoc.Clone();
            }

            // filter out system nodes
            for (int i = 0; i < systemNodes.Length; i++)
            {
                XmlNodeList nodesToRemove = itemXMLFilteredDoc.SelectNodes(systemNodes[i]);
                for (int j = 0; j < nodesToRemove.Count; j++)
                {
                    if (nodesToRemove[j].NodeType == XmlNodeType.Attribute)
                    {
                        ((XmlAttribute)nodesToRemove[j]).OwnerElement.RemoveAttribute(nodesToRemove[j].Name);
                    }
                    else
                    {
                        nodesToRemove[j].ParentNode.RemoveChild(nodesToRemove[j]);
                    }

                    // trim <item> if it has no child nodes
                    XmlNode itemNode = itemXMLFilteredDoc.SelectSingleNode("/xml/item");
                    if (itemNode != null)
                        if (itemNode.ChildNodes.Count == 0)
                            itemNode.ParentNode.RemoveChild(itemNode);
                }
            }
        }

        private void updateUnfilteredXmlfromFiltered()
        {
            updateFilteredXmlfromUnfiltered(false);
            XmlDocument itemXMLTempDoc = (XmlDocument)itemXMLFilteredDoc.Clone();
            XmlNode itemTempNode;

            // get item node or append one if it does not exist
            itemTempNode = itemXMLTempDoc.FirstChild.SelectSingleNode("/xml/item");
            if (itemTempNode == null)
            {
                itemTempNode = itemXMLTempDoc.FirstChild.AppendChild(itemXMLTempDoc.CreateElement("item"));
            }

            // copy system nodes from unfiltered item XML into filtered XML
            for (int i = 0; i < systemNodes.Length; i++)
            {
                XmlNode nodeToClone = itemXMLDoc.SelectSingleNode(systemNodes[i]);
                if (nodeToClone != null)
                {
                    // import node and append to temp XML (parent should always be /xml/item)
                    if (nodeToClone.NodeType == XmlNodeType.Element)
                    {
                        itemTempNode.AppendChild(itemXMLTempDoc.ImportNode(nodeToClone, true));
                    }
                    if (nodeToClone.NodeType == XmlNodeType.Attribute)
                    {
                        itemTempNode.Attributes.Append((XmlAttribute)itemXMLTempDoc.ImportNode(nodeToClone, true));
                    }
                }

            }
            // clone itemXMLTempDoc to itemXMLDoc
            itemXMLDoc = (XmlDocument)itemXMLTempDoc.Clone();
        }

        private void chkHideSystemMetadata_CheckedChanged(object sender, EventArgs e)
        {
            if (!flippingCheckbox)
            {
                try
                {
                    if (chkHideSystemMetadata.Checked)
                    {
                        mainForm.systemMetadataHidden = true;
                        if (editModeEnabled)
                        {
                            itemXMLDoc.LoadXml(fctbItemXml.Text);
                            updateFilteredXmlfromUnfiltered(true);
                        }
                    }
                    else
                    {
                        mainForm.systemMetadataHidden = false;
                        if (editModeEnabled)
                        {
                            itemXMLFilteredDoc.LoadXml(fctbItemXml.Text);
                            updateUnfilteredXmlfromFiltered();
                        }
                    }
                    RefreshXML(false);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    flippingCheckbox = true;
                    if (chkHideSystemMetadata.Checked)
                    {
                        chkHideSystemMetadata.Checked = false;
                    }
                    else
                    {
                        chkHideSystemMetadata.Checked = true;
                    }
                }
            }
            else
            {
                flippingCheckbox = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (editModeEnabled)
            {
                try
                {
                    bool cancelEdit = true;
                    if (dirtyUI)
                    {
                        DialogResult dialogResult = MessageBox.Show(this, "Do you want to cancel and discard changes?", "EQUELLA Metadata Utility", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        if (dialogResult == DialogResult.No)
                        {
                            cancelEdit = false;
                        }
                    }

                    if (cancelEdit)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        // cancel item edit and reload XML
                        equellaClient.cancelItemEdit(itemID, itemVersion);
                        RefreshXML(true);
                        dirtyUI = false;
                        equellaClient.logout();

                        // reset form to read-only mode
                        this.Text = "Viewing " + itemID + "/" + itemVersion.ToString();
                        fctbItemXml.ReadOnly = true;
                        fctbItemXml.AcceptsTab = false;
                        btnEditSave.Text = "Edit";
                        btnClose.Text = "Close";
                        editModeEnabled = false;
                        btnRefresh.Enabled = true;
                        btnGoDown.Enabled = true;
                        btnGoUp.Enabled = true;
                        this.Enabled = true;
                        toolStripViewXml.BackColor = SystemColors.Control;
                        chkHideSystemMetadata.BackColor = toolStripViewXml.BackColor;

                        this.Cursor = Cursors.Arrow;
                    }
                }
                catch (Exception err)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(err.Message, "Error on cancel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else this.Close();
        }

        private void ViewItemXMLForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            // check if dirty and ask if to save
            if (dirtyUI)
            {
                DialogResult dialogResult = MessageBox.Show(this, "Do you want to save your changes before closing?", "EQUELLA Metadata Utility", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        saveItem();
                        itemXMLDoc.LoadXml(equellaClient.getItem(itemID, itemVersion, ""));
                        equellaClient.logout();
                        mainForm.updateDataGridResultsRow(itemID, itemVersion, itemXMLDoc.FirstChild);
                        this.Cursor = Cursors.Arrow;
                    }
                    catch (Exception err)
                    {
                        e.Cancel = true;
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show(err.Message, "Error while saving and logging out", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                if (dialogResult == DialogResult.No)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        equellaClient.logout();
                        this.Cursor = Cursors.Arrow;
                    }
                    catch (Exception err)
                    {
                        this.Cursor = Cursors.Arrow;
                    }
                }
                if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                // not dirty but still logout if currently in itemediting session
                if (editModeEnabled)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        equellaClient.logout();
                        this.Cursor = Cursors.Arrow;
                    }
                    catch (Exception err)
                    {
                        this.Cursor = Cursors.Arrow;
                    }
                }
            }
        }
        private void btnEditSave_Click(object sender, EventArgs e)
        {
            if (!editModeEnabled)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    // set form to edit mode
                    this.Text = "Editing " + itemID + "/" + itemVersion.ToString();
                    fctbItemXml.ReadOnly = false;
                    fctbItemXml.AcceptsTab = true;
                    btnEditSave.Text = "Save";
                    btnClose.Text = "Cancel";
                    editModeEnabled = true;
                    btnRefresh.Enabled = false;
                    fctbItemXml.Focus();

                    // start item editing session
                    mainForm.login();
                    equellaClient = mainForm.equellaClient;
                    equellaClient.unlock(itemID, itemVersion);
                    string itemEditingXml = equellaClient.editItem(itemID, itemVersion, copyToStaging);
                    itemXMLDoc.LoadXml(itemEditingXml);
                    updateFilteredXmlfromUnfiltered(true);
                    RefreshXML(false);
                    btnGoDown.Enabled = false;
                    btnGoUp.Enabled = false;
                    dirtyUI = false;
                    toolStripViewXml.BackColor = Color.Pink;
                    chkHideSystemMetadata.BackColor = toolStripViewXml.BackColor;
                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception err)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(err.Message, "Error editing item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.Enabled = false;
                    
                    // save item
                    saveItem();

                    // refresh XML
                    RefreshXML(true);
                    dirtyUI = false;

                    // reload the datagrid row
                    mainForm.updateDataGridResultsRow(itemID, itemVersion, itemXMLDoc.FirstChild);

                    // reset form to read-only mode
                    this.Text = "Viewing " + itemID + "/" + itemVersion.ToString();
                    fctbItemXml.ReadOnly = true;
                    fctbItemXml.AcceptsTab = false;
                    btnEditSave.Text = "Edit";
                    btnClose.Text = "Close";
                    editModeEnabled = false;
                    btnRefresh.Enabled = true;
                    btnGoDown.Enabled = true;
                    btnGoUp.Enabled = true;
                    this.Enabled = true;
                    toolStripViewXml.BackColor = SystemColors.Control;
                    chkHideSystemMetadata.BackColor = toolStripViewXml.BackColor;
                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception err)
                {
                    this.Enabled = true;
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(err.Message, "Error saving item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveItem()
        {
            // update itemXmlDoc from filteredItemXmlDoc
            if (chkHideSystemMetadata.Checked)
            {
                itemXMLFilteredDoc.LoadXml(fctbItemXml.Text);
                updateUnfilteredXmlfromFiltered();
                RefreshXML(false);
            }
            else
            {
                itemXMLDoc.LoadXml(fctbItemXml.Text);
            }

            // write XML to item 
            equellaClient.saveItem(itemXMLDoc.InnerXml, false);
        }

        private void fctbItemXml_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (editModeEnabled)
            {
                btnEditSave.Enabled = true;
                dirtyUI = true;
            }
        }

        private int getLineNumberOfCaret()
        {
            string textBeforeCaret;
            int occurenceCount = 0;
            int pos;

            textBeforeCaret = fctbItemXml.Text.Substring(0, fctbItemXml.SelectionStart);
            for (int i = 0; i < textBeforeCaret.Length; i++)
            {
                if (textBeforeCaret[i] == Convert.ToChar("\n")) occurenceCount++;
            }
            return occurenceCount;
        }

        private void goUpOrDown(bool goUp)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int gridRow = mainForm.getItemGridRow(itemID, itemVersion);

                // increment or decrement row position with "loop around"
                if (goUp)
                    if (gridRow == 0)
                    {
                        gridRow = mainForm.getGridRowCount() - 1;
                    }
                    else
                    {
                        gridRow--;
                    }
                else
                {
                    if (gridRow == mainForm.getGridRowCount() - 1)
                    {
                        gridRow = 0;
                    }
                    else
                    {
                        gridRow++;
                    }
                }
                MainForm.itemversionid itemVersionID = mainForm.getItemIDByGridRow(gridRow);
                if (itemVersionID.itemID != "")
                {
                    itemID = itemVersionID.itemID;
                    itemVersion = itemVersionID.itemVersion;
                    RefreshXML(true);
                    this.Text = "Viewing " + itemID + "/" + itemVersion.ToString();
                    mainForm.selectGridRow(gridRow);
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(err.Message, "Error retrieving item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoDown_Click(object sender, EventArgs e)
        {
            goUpOrDown(false);
        }

        private void btnGoUp_Click(object sender, EventArgs e)
        {
            goUpOrDown(true);
        }

    }
}
