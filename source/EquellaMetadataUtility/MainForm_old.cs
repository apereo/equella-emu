using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml.Serialization;


namespace EquellaMetadataUtility
{
    public partial class MainForm : Form
    {
        private const string endPointFolder = "/services/SoapService41";
        private const int maxLogBuffer = 500000;
        private const int systemColumns = 4; //checkbox, itemID, itemVersion, itemName
        private const string passPhrase = "Pas7pr@se";
        private const string saltValue = "s@1tV@1ue";
        private const string hashAlgorithm = "SHA1";
        private const int passwordIterations = 2;
        private const string initVector = "@1B2c3D4e5F6g7H8";
        private const int keySize = 256;
        private const int maxLogFileSize = 10000000;

        private EquellaClient equellaClient;
        private string lastErrorDescription;
        private bool stopProcessing = false;
        private bool processing = false;
        private ArrayList modifierList = new ArrayList();
        private string currentProfileFile = "";
        private StreamWriter eventLog;
        private ArrayList resultsXpaths = new ArrayList();
        private string[] nonEditableNodes = {
                                                "/xml/item/name",
                                                "/xml/item/description",
                                                "/xml/item/@itemstatus",
                                                "/xml/item/@id",
                                                "/xml/item/@version",
                                                "/xml/item/@key",
                                                "/xml/item/itemdefid",
                                                "/xml/item/datecreated",
                                                "/xml/item/datemodified",
                                                "/xml/item/dateforindex",
                                                "/xml/item/owner",
                                                "/xml/item/rating",
                                                "/xml/item/badurls",
                                                "/xml/item/moderation",
                                                "/xml/item/newitem",
                                                "/xml/item/attachments",
                                                "/xml/item/navigationnodes",
                                                "/xml/item/url",
                                                "/xml/item/history"
                                            };

        private enum logOutputTarget { displayOnly, fileOnly, displayAndFile }

        public string launchProfile;

        public MainForm()
        {
            InitializeComponent();

            this.Text = "EMU - EQUELLA Metadata Utility";

            rdioEqUser.Checked = true;
            rdioAllCollections.Checked = true;

            grpSharedSecret.Enabled = false;

            cmbSortOrder.SelectedItem = "Rank";
            cmbLogOutput.SelectedIndex = 0;

            this.KeyPreview = true;

            currentProfileFile = Properties.Settings.Default.lastProfile;
            
        }

        public string FormEndPoint(string baseUrl)
        {
            string endPointUrl;
            if (baseUrl.Length > 0 && baseUrl.Substring(baseUrl.Length - 1, 1) == "/")
                endPointUrl = baseUrl.Substring(0, baseUrl.Length - 1) + endPointFolder;
            else
                endPointUrl = baseUrl + endPointFolder;

            return endPointUrl;
        }

        // function to test the EQUELLA connection
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // string endPoint = FormEndPoint(txtInstitutionUrl.Text);
                // equellaClient = new EquellaClient(endPoint);
                login();
                equellaClient.logout();
                this.Cursor = Cursors.Arrow;

                MessageBox.Show("Successfully logged in and out of EQUELLA at " + txtInstitutionUrl.Text, "Successful Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception err)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (equellaClient != null) equellaClient.Close();
        }

        // login the equellaClient using the settings from the Connection tab
        public void login()
        {
            try
            {
                string endPoint = FormEndPoint(txtInstitutionUrl.Text);
                equellaClient = new EquellaClient(endPoint);
                if (rdioEqUser.Checked)
                {
                    equellaClient.login(txtUsername.Text, txtPassword.Text);
                }
                else
                {
                    equellaClient.loginSharedSecret(txtSSUsername.Text, txtSharedSecretID.Text, txtSharedSecret.Text);
                }
            }
            catch (Exception err)
            {
                // form a friendly error description
                switch (err.Message)
                {
                    case "com.tle.exceptions.secret":
                        lastErrorDescription = "Problem with shared secret";
                        break;
                    case "com.tle.exceptions.badtokenid":
                        lastErrorDescription = "Problem with shared secret ID";
                        break;
                    case "com.tle.exceptions.badusername":
                        lastErrorDescription = "Problem with username";
                        break;
                    case "Fault: java.lang.NullPointerException":
                        lastErrorDescription = "System error. If using a shared secret check that it is correctly configured in EQUELLA.";
                        break;
                    default:
                        lastErrorDescription = err.Message;
                        break;
                }
                if (err.Message.Contains("<html lang=\"en\">"))
                {
                    lastErrorDescription = "Receiving back a web page instead of a SOAP response from '" + txtInstitutionUrl.Text + "'. This is often caused by an incorrect institution URL.";
                }
                if (err.Message.Contains("There was no endpoint listening at"))
                {
                    if (err.InnerException.Message.Contains("(404) Not Found"))
                    {
                        lastErrorDescription = "Could not locate correct SOAP endpoint on this EQUELLA institution. Check that the version of EQUELLA at '" + txtInstitutionUrl.Text + "' is 4.1 or higher.";
                    }
                    else
                    {
                        lastErrorDescription = "No network response from '" + txtInstitutionUrl.Text + "'. Check that the institution URL is correct and EQUELLA is available.";
                    }
                }
                if (err.Message.Contains("Invalid URI:"))
                {
                    lastErrorDescription = "'" + txtInstitutionUrl.Text + "' is an invalid institution URL. It should be of the form \"http://server_address/insitution_name\"";
                }

                throw new System.InvalidOperationException(lastErrorDescription);
            }
        }

        private void rdioEqUser_CheckedChanged(object sender, EventArgs e)
        {
            if (rdioEqUser.Checked == true)
            {
                grpEQUser.Enabled = true;
                grpSharedSecret.Enabled = false;
            }
            else
            {
                grpSharedSecret.Enabled = true;
                grpEQUser.Enabled = false;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // TODO: check if dirty and ask if save

        }

        // run query
        private void cmdRunQuery_Click(object sender, EventArgs e)
        {
            try
            {
                // connect to EQUELLA
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                login();
                this.Cursor = Cursors.Arrow;
            
                processing = true;
                stopToolStripButton.Enabled = true;
                tabMain.Enabled = false;
                tabMain.SelectTab(2);
                
                // clear results from last query
                dataGridResults.Rows.Clear();
                dataGridResults.Visible = true;

                toolStripProgressBar.Visible = true;
                toolStripStatusResultsCount.Text = "0 selected";
                statusStrip1.Refresh();
                tabMain.Refresh();

                // populate query variables from data in the Query tab
                //freetext query
                string freetext = txtFreeTextQuery.Text;
                // collection filter
                var collections = new EquellaMetadataUtility.EQUELLA.ArrayOfString {};
                if (rdioSpecificCollections.Checked)
                {
                    for (int i = 0; i < lstCollections.Items.Count; i++)
                    {
                        collections.Add(equellaClient.getCollectionUUID(lstCollections.Items[i].ToString()));
                    }
                }
                //where statement
                string whereClause = txtWhereStatement.Text;
                //only live
                bool onlyLive = !chkIncludeNonLive.Checked;
                // order type
                int orderType;
                switch (cmbSortOrder.SelectedItem.ToString())
                {
                    case "Rank":
                        orderType = 0;
                        break;
                    case "Date Modified":
                        orderType = 1;
                        break;
                    case "Name":
                        orderType = 2;
                        break;
                    default:
                        orderType = 0;
                        break;
                }
                //reverse order
                bool reverseOrder = chkReverseOrder.Checked;

                // reveal system columns where specified
                if (chkIncludeColumnsName.Checked)
                    dataGridResults.Columns[1].Visible = true;
                else
                    dataGridResults.Columns[1].Visible = false;
                if (chkIncludeColumnsID.Checked)
                    dataGridResults.Columns[2].Visible = true;
                else
                    dataGridResults.Columns[2].Visible = false;
                if (chkIncludeColumnsVersion.Checked)
                    dataGridResults.Columns[3].Visible = true;
                else
                    dataGridResults.Columns[3].Visible = false;

                resultsXpaths.Clear();
                resultsXpaths.Add("/xml/item/name");
                resultsXpaths.Add("/xml/item/@id");
                resultsXpaths.Add("/xml/item/@version");

                // set column headers
                dataGridResults.ColumnCount = systemColumns;
                string columnXpath;
                string columnHeader;
                for (int i = 0; i < lstColumns.Items.Count; i++)
                {
                    string columnStatement = lstColumns.Items[i].ToString();
                    int splitIndex = columnStatement.ToLower().IndexOf(" as ");
                    if (splitIndex != -1)
                    {
                        columnXpath = columnStatement.Substring(0, splitIndex).Trim();
                        columnHeader = columnStatement.Substring(splitIndex + 4).Trim();
                    }
                    else
                    {
                        columnXpath = columnStatement;
                        columnHeader = columnStatement;
                    }
                    dataGridResults.Columns.Add("column" + i.ToString(), columnHeader);
                    //dataGridResults.Columns["column" + i.ToString()].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridResults.Columns["column" + i.ToString()].ReadOnly = true;
                    resultsXpaths.Add(columnXpath);
                }

                XmlDocument resultsXML = new XmlDocument();
                XmlDocument resultsCountXML = new XmlDocument();

                const int pageSize = 97;
                int offset = 0;
                toolStripProgressBar.Step = 1;

                // get count of records prior to running query
                resultsCountXML.LoadXml(equellaClient.searchItems(freetext, collections, whereClause, onlyLive, orderType, reverseOrder, 0, 1));
                int resultsCount = Convert.ToInt32(resultsCountXML.SelectSingleNode("results/available").InnerXml);

                if (resultsCount > 0)
                {

                    // get results in chunks of pageSize
                    int pagesRequired = resultsCount / pageSize + 1;
                    toolStripProgressBar.Maximum = pagesRequired;

                    object[] rowCells = new object[dataGridResults.ColumnCount];
                    int currentPageSize;
                    toolStripStatusProgressText.Visible = true;
                    int rowCount = 0;
                    string xpath;
                    Utils utils = new Utils();

                    for (int j = 0; j < pagesRequired; j++)
                    {
                        DrawingControl.SuspendDrawing(dataGridResults);

                        // increment progress bar
                        toolStripProgressBar.Value = j + 1;
                        toolStripStatusProgressText.Text = "Retrieving " + Convert.ToString(offset) + "/" + resultsCount;

                        // get chunk from EQUELLA
                        resultsXML.LoadXml(equellaClient.searchItems(freetext, collections, whereClause, onlyLive, orderType, reverseOrder, offset, pageSize));

                        // get current page size
                        currentPageSize = Convert.ToInt32(resultsXML.SelectSingleNode("results/@count").Value);

                        // loop through chunk
                        for (int i = 0; i < currentPageSize; i++)
                        {
                            rowCount += 1;

                            // set system columns
                            rowCells[0] = false;
                            rowCells[1] = resultsXML.ChildNodes[0].ChildNodes[i].SelectSingleNode("xml/item/name").InnerXml;
                            rowCells[2] = resultsXML.ChildNodes[0].ChildNodes[i].SelectSingleNode("xml/item/@id").Value;
                            rowCells[3] = resultsXML.ChildNodes[0].ChildNodes[i].SelectSingleNode("xml/item/@version").Value;

                            // get values from xpaths and use Value prop for attributes and InnerXml for nodes
                            for (int k = 0; k < lstColumns.Items.Count; k++)
                            {
                                // trim leading slash if it exists
                                if (resultsXpaths[k + systemColumns - 1].ToString().StartsWith("/"))
                                {
                                    xpath = resultsXpaths[k + systemColumns - 1].ToString().Substring(1);
                                }
                                else
                                {
                                    xpath = resultsXpaths[k + systemColumns - 1].ToString();
                                }
                                var xmlReturnNode = resultsXML.ChildNodes[0].ChildNodes[i].SelectSingleNode(xpath);

                                // test node has been returned
                                if (xmlReturnNode != null)
                                {
                                    // write either element textnode value or attribute value to datagrid cell
                                    if (xmlReturnNode.NodeType == XmlNodeType.Element)
                                    {
                                        XmlNode nameTextNode = utils.GetTextNode((XmlElement)xmlReturnNode);
                                        // wrtie text node unless it doesn't exist in which case write empty string
                                        if (nameTextNode != null)
                                            rowCells[k + systemColumns] = nameTextNode.Value;
                                        else
                                            rowCells[k + systemColumns] = "";
                                    }
                                    else
                                        if (xmlReturnNode.NodeType == XmlNodeType.Attribute)
                                        {
                                            rowCells[k + systemColumns] = xmlReturnNode.Value;
                                        }
                                }
                                else
                                {
                                    rowCells[k + systemColumns] = "<null>";
                                }
                            }

                            // add row to grid
                            dataGridResults.Rows.Add(rowCells);

                            dataGridResults.Rows[rowCount - 1].HeaderCell.Value = rowCount.ToString();

                            // check for user events
                            Application.DoEvents();

                            // check if query has been cancelled
                            if (stopProcessing) break;
                        }
                        // show chunk
                        DrawingControl.ResumeDrawing(dataGridResults);

                        // check if query has been cancelled
                        if (stopProcessing) break;

                        offset += pageSize;

                    }

                    dataGridResults.Columns[0].Width = 29; 
                    dataGridResults.Columns[0].Width = 30;
                    dataGridResults.Visible = true;
                    clearToolStripButton.Text = "Clear unselected results";
                    clearToolStripButton.Enabled = true;

                    toolStripProgressBar.Value = toolStripProgressBar.Maximum;
                }
                else
                {
                    dataGridResults.Visible = false;
                }

                // logout of EQUELLA
                equellaClient.logout();

                processing = false;
                stopProcessing = false;

                toolStripProgressBar.Visible = false;
                toolStripProgressBar.Value = 0;

                toolStripStatusResultsCount.Text = dataGridResults.Rows.Count.ToString() + " results";
                toolStripStatusProgressText.Visible = false;
                
                stopToolStripButton.Enabled = false;

            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            processing = false;
            stopProcessing = false;

            toolStripProgressBar.Visible = false;
            toolStripProgressBar.Value = 0;

            toolStripStatusResultsCount.Text = dataGridResults.Rows.Count.ToString() + " results";
            toolStripStatusProgressText.Visible = false;

            stopToolStripButton.Enabled = false;

            tabMain.Enabled = true;

        }

        private void btnStopProcessing_Click(object sender, EventArgs e)
        {
            stopProcessing = true;
        }

        private void tabQueryResults_Click(object sender, EventArgs e)
        {
            stopToolStripButton.Visible = true;
            stopToolStripButton.Enabled = false;
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabMain.SelectedIndex)
            {
                case 2:
                    clearToolStripButton.Text = "Clear unselected results";
                    if (processing)
                    {
                        clearToolStripButton.Enabled = false;
                    }
                    else
                    {
                        clearToolStripButton.Enabled = true;
                        stopToolStripButton.Enabled = false;
                    }
                    break;
                case 4:
                    clearToolStripButton.Text = "Clear log";
                    if (processing)
                    {
                        clearToolStripButton.Enabled = false;
                    }
                    else
                    {
                        clearToolStripButton.Enabled = true;
                        stopToolStripButton.Enabled = false;
                    }
                    break;
                default:
                    clearToolStripButton.Enabled = false;
                    clearToolStripButton.Text = "Clear";
                    break;
            }
        }

        // add column to columns to return in results
        private void btnColumnAdd_Click(object sender, EventArgs e)
        {
            if (cmbNewItem.Text != "")
            {
                lstColumns.Items.Add(cmbNewItem.Text);
                lstColumns.ClearSelected();
                lstColumns.SelectedIndex = lstColumns.Items.Count - 1;
            }
        }

        // remove columns from columns to return in results
        private void btnRemove_Click(object sender, EventArgs e)
        {
            while (lstColumns.SelectedItem != null)
            {
                lstColumns.Items.RemoveAt(lstColumns.SelectedIndex);
            }

        }

        // move up columns from columns to return in results
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (lstColumns.SelectedItem != null)
            {
                if (lstColumns.SelectedIndex != 0)
                {
                    for (int i = 0; i < lstColumns.SelectedIndices.Count; i++)
                    {
                        int selectedIndex = lstColumns.SelectedIndices[i];
                        var temp = lstColumns.Items[selectedIndex];
                        lstColumns.Items[selectedIndex] = lstColumns.Items[selectedIndex - 1];
                        lstColumns.Items[selectedIndex - 1] = temp;
                        lstColumns.SetSelected(selectedIndex, false);
                        lstColumns.SetSelected(selectedIndex - 1, true);

                    }
                }
            }
        }

        // move down columns from columns to return in results
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (lstColumns.SelectedItem != null)
            {
                if (lstColumns.SelectedIndices[lstColumns.SelectedIndices.Count - 1] != lstColumns.Items.Count - 1)
                {
                    for (int i = lstColumns.SelectedIndices.Count - 1; i > -1; i--)
                    {
                        int selectedIndex = lstColumns.SelectedIndices[i];
                        var temp = lstColumns.Items[selectedIndex];
                        lstColumns.Items[selectedIndex] = lstColumns.Items[selectedIndex + 1];
                        lstColumns.Items[selectedIndex + 1] = temp;
                        lstColumns.SetSelected(selectedIndex, false);
                        lstColumns.SetSelected(selectedIndex + 1, true);

                    }
                }
            }
        }

        // control applicability of reverse order check box
        private void cmbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbSortOrder.SelectedItem == "Rank")
            {
                chkReverseOrder.Checked = false;
                chkReverseOrder.Enabled = false;
            }
            else
            {
                chkReverseOrder.Enabled = true;
            }
        }

        private void btnAddCollection_Click(object sender, EventArgs e)
        {
            try
            {
                Collections collections = new Collections();
                login();
                collections.equellaClient = equellaClient;
                collections.ShowDialog(this);
                if (collections.okPressed)
                {
                    // populate lstCollection with selected collections from collection form
                    for (int i = 0; i < collections.selectedCollections.Count; i++)
                    {
                        if (!lstCollections.Items.Contains(collections.selectedCollections[i]))
                        {
                            lstCollections.Items.Add(collections.selectedCollections[i]);
                        }
                    }
                    rdioSpecificCollections.Checked = true;
                }
                collections.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveCollection_Click(object sender, EventArgs e)
        {
            while (lstCollections.SelectedItem != null)
            {
                lstCollections.Items.Remove(lstCollections.SelectedItem);
            }
        }

        // enable/disable collections list box
        private void rdioAllCollections_CheckedChanged(object sender, EventArgs e)
        {
            if (rdioAllCollections.Checked)
            {
                lstCollections.ClearSelected();
                lstCollections.Enabled = false;
                btnAddCollection.Enabled = false;
                btnRemoveCollection.Enabled = false;
            }
            else
            {
                lstCollections.Enabled = true;
                btnAddCollection.Enabled = true;
                btnRemoveCollection.Enabled = true;

            }
        }

        // move around controls as necessary when form resizes
        private void MainForm_Resize(object sender, EventArgs e)
        {
            tabMain.Width = this.Width - 15;
            tabMain.Height = this.Height - 85;
            dataGridResults.Width = tabMain.Width - 6;
            dataGridResults.Height = tabMain.Height - 58;
            panelResultsBackground.Width = tabMain.Width - 6;
            panelResultsBackground.Height = tabMain.Height - 58;
            richTextLog.Width = tabMain.Width - 5;
            richTextLog.Height = tabMain.Height - 26;
            lstViewModifiers.Width = tabMain.Width - 200;
            btnMoveModifierDown.Left = tabMain.Width - 75;
            btnMoveModifierUp.Left = tabMain.Width - 75;
            btnEditModifier.Left = tabMain.Width - 75;
            btnRemoveModifier.Left = tabMain.Width - 75;

        }


        public void runBulkUpdate(bool testOnly)
        {
            // determine log output settings
            logOutputTarget logTarget;
            if(chkCreateLogFiles.Checked)
            {
                logTarget = logOutputTarget.displayAndFile;
            }
            else
            {
                logTarget = logOutputTarget.displayOnly;
            }

            try
            {

                // determine how many items will be processed
                int scheduledItemsCount = 0;
                int gridItemsCount = 0;
                int firstFewToUpdate = 0;

                if (rdioUpdateAllResults.Checked)
                {
                    gridItemsCount = dataGridResults.Rows.Count;
                }

                if (rdioSelectedResults.Checked)
                {
                    gridItemsCount = countCheckedResultsRows();
                }

                // check if "first few" checkbox is checked and if the number is less than the grid specified amount
                if (chkOnlyUpdateFirstFew.Checked)
                {
                    if (!int.TryParse(txtFirstFewToUpdate.Text.Trim(), out firstFewToUpdate))
                    {
                        throw new System.InvalidOperationException("Error: Number of items to update must be an integer.");
                    }

                    if (firstFewToUpdate < gridItemsCount)
                    {
                        scheduledItemsCount = firstFewToUpdate;
                    }
                    else
                    {
                        scheduledItemsCount = gridItemsCount;
                    }
                }
                else
                {
                    scheduledItemsCount = gridItemsCount;
                }

                string itemCountStatement = scheduledItemsCount.ToString("#,#") + " items";
                if (scheduledItemsCount == 1) itemCountStatement = "1 item";

                if (testOnly || MessageBox.Show(this, "This will update " + itemCountStatement + " in EQUELLA at " +
                    txtInstitutionUrl.Text + ".\n\nContinue?", "Batch Update", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    int logFileCount = 0;
                    string startLogFilename = "";
                    string currentLogFilename = "";
                    if (logTarget != logOutputTarget.displayOnly)
                    {
                        // start new log file
                        if (txtLogFileLocation.Text == "") throw new System.InvalidOperationException("Error: Log files folder in Connection tab not specified.");
                        if (!Directory.Exists(txtLogFileLocation.Text)) throw new System.InvalidOperationException("Error: Log files folder '" + txtLogFileLocation.Text + "' in Connection tab does not exist.");
                        startLogFilename = txtLogFileLocation.Text + "\\" + String.Format("{0:yyyy-MM-dd-HHmmss}", DateTime.Now);
                        currentLogFilename = startLogFilename + "." + String.Format("{0:0000}", logFileCount) + ".txt";
                        if (!File.Exists(currentLogFilename))
                        {
                            eventLog = new StreamWriter(currentLogFilename);
                        }
                        else
                        {
                            eventLog = File.AppendText(currentLogFilename);
                        }
                    }

                    if (testOnly)
                    {
                        addLogEvent("Performing test run on " + itemCountStatement + " at " + txtInstitutionUrl.Text, true, "Blue", logTarget);
                    }
                    else
                    {
                        addLogEvent("Updating " + itemCountStatement + " at " + txtInstitutionUrl.Text, true, "Blue", logTarget);
                    }

                    processing = true;
                    stopProcessing = false;
                    stopToolStripButton.Enabled = true;
                    clearToolStripButton.Enabled = false;
                    clearToolStripButton.Enabled = false;
                    tabMain.Enabled = false;
                    tabMain.SelectTab(4);

                    this.Cursor = Cursors.WaitCursor;
                    login();
                    this.Cursor = Cursors.Arrow;

                    toolStripProgressBar.Maximum = scheduledItemsCount;
                    toolStripProgressBar.Visible = true;
                    toolStripStatusProgressText.Visible = true;
                    toolStripStatusProgressText.Text = "";

                    string itemID;
                    int itemVersion;
                    string itemName;
                    string oldItemXMLstring;
                    string newItemXMLstring;

                    int itemsProcessed = 0;
                    int successCount = 0;
                    int errorCount = 0;

                    bool itemRequiresUpdating = false;
                    for (int i = 0; i < dataGridResults.Rows.Count; i++)
                    {
                        // check if only processing first few and it's been reached
                        if (chkOnlyUpdateFirstFew.Checked)
                            if (itemsProcessed >= scheduledItemsCount)
                                break;

                        // determine if item requires processing
                        if (rdioSelectedResults.Checked)
                        {
                            if (Convert.ToBoolean(dataGridResults.Rows[i].Cells[0].Value))
                                itemRequiresUpdating = true;
                            else
                                itemRequiresUpdating = false;
                        }
                        else
                            itemRequiresUpdating = true;

                        // update item if required
                        if (itemRequiresUpdating)
                        {
                            toolStripProgressBar.Value = itemsProcessed + 1;

                            // get item id, version and name from hidden system columns
                            itemName = Convert.ToString(dataGridResults.Rows[i].Cells[1].Value);
                            itemID = Convert.ToString(dataGridResults.Rows[i].Cells[2].Value);
                            itemVersion = Convert.ToInt16(dataGridResults.Rows[i].Cells[3].Value);

                            if (testOnly)
                            {
                                addLogEvent("TESTING ITEM " + (itemsProcessed + 1).ToString(), true, "Blue", logTarget);
                            }
                            else
                            {
                                addLogEvent("UPDATING ITEM " + (itemsProcessed + 1).ToString(), true, "Blue", logTarget);
                            }
                            addLogEvent(" \"" + itemName + "\", ID: " + itemID + ", Version: " + itemVersion.ToString(), false, "Blue", logTarget);
                            addLogEvent(" Reading current metadata...", false, "Blue", logTarget);

                            try
                            {

                                // get item for editing
                                equellaClient.unlock(itemID, itemVersion);
                                oldItemXMLstring = equellaClient.editItem(itemID, itemVersion, false);

                                // log input xml appropriately
                                if (cmbLogOutput.SelectedIndex != 2 || logTarget != logOutputTarget.displayOnly)
                                {
                                    // write either indented or unindented item xml to display log
                                    if (cmbLogOutput.SelectedIndex == 0 || logTarget != logOutputTarget.displayOnly)
                                    {
                                        string indentedItemXMLString;
                                        XmlDocument xmlDoc = new XmlDocument();
                                        xmlDoc.LoadXml(oldItemXMLstring);
                                        StringWriter sw = new StringWriter();
                                        XmlTextWriter xw = new XmlTextWriter(sw);
                                        xw.Formatting = Formatting.Indented;
                                        xmlDoc.WriteTo(xw);
                                        indentedItemXMLString = sw.ToString();
                                        // write indented item xml to log file and/or log display
                                        if (logTarget != logOutputTarget.displayOnly)
                                            addLogEvent(indentedItemXMLString, false, "Gray", logOutputTarget.fileOnly);
                                        if (logTarget != logOutputTarget.fileOnly)
                                        {
                                            if (cmbLogOutput.SelectedIndex == 0) addLogEvent(indentedItemXMLString, false, "Gray", logOutputTarget.displayOnly);
                                            if (cmbLogOutput.SelectedIndex == 1) addLogEvent(oldItemXMLstring, false, "Gray", logOutputTarget.displayOnly);
                                        }
                                    }
                                    else
                                    {
                                        addLogEvent(oldItemXMLstring, false, "Gray", logOutputTarget.displayOnly);
                                    }
                                }

                                // update item XML metadata
                                newItemXMLstring = updateItemXML(oldItemXMLstring);

                                if (testOnly)
                                {
                                    addLogEvent(" Updating item with new metadata...", false, "Blue", logTarget);
                                }
                                else
                                {
                                    addLogEvent(" Generating new metadata...", false, "Blue", logTarget);
                                }

                                // log transformed xml appropriately
                                if (cmbLogOutput.SelectedIndex != 2 || logTarget != logOutputTarget.displayOnly)
                                {
                                    // write either indented or unindented item xml to display log
                                    if (cmbLogOutput.SelectedIndex == 0 || logTarget != logOutputTarget.displayOnly)
                                    {
                                        string indentedItemXMLString;
                                        XmlDocument xmlDoc = new XmlDocument();
                                        xmlDoc.LoadXml(newItemXMLstring);
                                        StringWriter sw = new StringWriter();
                                        XmlTextWriter xw = new XmlTextWriter(sw);
                                        xw.Formatting = Formatting.Indented;
                                        xmlDoc.WriteTo(xw);
                                        indentedItemXMLString = sw.ToString();
                                        // write indented item xml to log file and/or log display
                                        if (logTarget != logOutputTarget.displayOnly)
                                            addLogEvent(indentedItemXMLString, false, "Gray", logOutputTarget.fileOnly);
                                        if (logTarget != logOutputTarget.fileOnly)
                                        {
                                            if (cmbLogOutput.SelectedIndex == 0) addLogEvent(indentedItemXMLString, false, "Gray", logOutputTarget.displayOnly);
                                            if (cmbLogOutput.SelectedIndex == 1) addLogEvent(newItemXMLstring, false, "Gray", logOutputTarget.displayOnly);
                                        }
                                    }
                                    else
                                    {
                                        addLogEvent(newItemXMLstring, false, "Gray", logOutputTarget.displayOnly);
                                    }
                                }

                                if (testOnly)
                                {
                                    // cancel item editing
                                    equellaClient.cancelItemEdit(itemID, itemVersion);
                                    addLogEvent(" Item valid for updating.", false, "Blue", logTarget);
                                }
                                else
                                {
                                    // save item to EQUELLA
                                    equellaClient.saveItem(newItemXMLstring, true);
                                    addLogEvent(" Item sucessfully updated.", false, "Blue", logTarget);
                                }
                                // check if log file getting too big, if so close current one and create a new one
                                if (logTarget != logOutputTarget.displayOnly && eventLog.BaseStream.Length > maxLogFileSize)
                                {
                                    eventLog.Close();
                                    logFileCount += 1;
                                    currentLogFilename = startLogFilename + "." + String.Format("{0:0000}", logFileCount) + ".txt";
                                    if (!File.Exists(currentLogFilename))
                                    {
                                        eventLog = new StreamWriter(currentLogFilename);
                                    }
                                    else
                                    {
                                        eventLog = File.AppendText(currentLogFilename);
                                    }
                                }

                                // increment the success counter
                                successCount += 1;
                            }
                            catch (Exception err)
                            {
                                errorCount += 1;
                                addLogEvent(" ERROR: " + err.Message, false, "Red", logTarget);
                            }

                            itemsProcessed += 1;
                            toolStripStatusProgressText.Text = " Processing " + itemsProcessed.ToString() + "/" + scheduledItemsCount;
                            Application.DoEvents();
                            if (stopProcessing) break;
                        }
                    }
                    string outcomeReport = " Success:" + successCount.ToString() + " Errors:" + errorCount.ToString();
                    if (stopProcessing)
                    {
                        addLogEvent("Processing interrupted by user. " + outcomeReport, true, "Red", logTarget);
                    }
                    else
                    {
                        addLogEvent("Processing complete. " + outcomeReport, true, "Blue", logTarget);
                    }
                }
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Arrow;
                addLogEvent("Processing halted. An error occured: " + err.Message, true, "Red", logTarget);
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Close the log stream:
            try
            {
                eventLog.Close();
            }
            catch(Exception err) { }

            processing = false;
            stopProcessing = false;
            toolStripProgressBar.Visible = false;
            toolStripStatusProgressText.Visible = false;
            toolStripProgressBar.Value = 0;
            stopToolStripButton.Enabled = false;
            clearToolStripButton.Enabled = true;
            clearToolStripButton.Enabled = true;
            tabMain.Enabled = true;
        }

        // Updates XML based on update modifiers from Update tab
        private string updateItemXML(string oldItemXMLstring)
        {
            string newItemXMLstring = oldItemXMLstring;

            // iterate through modifiers
            for (int index = 0; index < modifierList.Count; index++)
            {
                // cast correct modifier object and validate and transform
                switch (modifierList[index].GetType().Name)
                {
                    case "ModifierUpdateText":
                        ModifierUpdateText updateTextModifier = (ModifierUpdateText)modifierList[index];
                        if (updateTextModifier.Enabled)
                        {
                            updateTextModifier.validate();
                            newItemXMLstring = updateTextModifier.transform(newItemXMLstring);
                        }
                        break;

                    case "ModifierAddXML":
                        ModifierAddXML addXMLModifier = (ModifierAddXML)modifierList[index];
                        if (addXMLModifier.Enabled)
                        {
                            addXMLModifier.validate();
                            newItemXMLstring = addXMLModifier.transform(newItemXMLstring);
                        }
                        break;
                    case "ModifierRenameNode":
                        ModifierRenameNode renameNodeModifier = (ModifierRenameNode)modifierList[index];
                        if (renameNodeModifier.Enabled)
                        {
                            renameNodeModifier.validate();
                            newItemXMLstring = renameNodeModifier.transform(newItemXMLstring);
                        }
                        break;
                    case "ModifierRemoveNode":
                        ModifierRemoveNode removeNodeModifier = (ModifierRemoveNode)modifierList[index];
                        if (removeNodeModifier.Enabled)
                        {
                            removeNodeModifier.validate();
                            newItemXMLstring = removeNodeModifier.transform(newItemXMLstring);
                        }
                        break;
                    default:
                        throw new System.InvalidOperationException(modifierList[index].GetType().GetType().Name + " not a recognized modifier");
                }

            }

            return newItemXMLstring;
        }

        private void btnRunBulkUpdate_Click(object sender, EventArgs e)
        {
            runBulkUpdate(false);
        }

        private void btnTestBulkUpdate_Click(object sender, EventArgs e)
        {
            runBulkUpdate(true);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            richTextLog.Clear();
        }

        // count selected rows
        private int countCheckedResultsRows()
        {
            int count = 0;
            for (int i = 0; i < dataGridResults.Rows.Count; i++)
            {
                if(Convert.ToBoolean(dataGridResults.Rows[i].Cells[0].Value) == true)
                {
                    count += 1;
                }
            }
            return count;
        }

        // respond to mouse up on the checkbox column
        private void dataGridResults_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridResults.CurrentCell.ColumnIndex == 0)
            {
                dataGridResults.EndEdit();
                toolStripSelectionCount.Text = Convert.ToString(countCheckedResultsRows()) + " selected";
            }
        }

        // select all results
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridResults.Rows.Count; i++)
            {
                dataGridResults.Rows[i].Cells[0].Value = true;
            }
            // update tool strip count
            toolStripSelectionCount.Text = Convert.ToString(countCheckedResultsRows()) + " selected";
        }

        // unselect all results
        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridResults.Rows.Count; i++)
            {
                dataGridResults.Rows[i].Cells[0].Value = false;
            }
            // update tool strip count
            toolStripSelectionCount.Text = "0 selected";
        }

        // inverts results selection
        private void btnInvertSelection_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridResults.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridResults.Rows[i].Cells[0].Value) == true)
                {
                    dataGridResults.Rows[i].Cells[0].Value = false;
                }
                else
                {
                    dataGridResults.Rows[i].Cells[0].Value = true;
                }
            }
            // update tool strip count
            toolStripSelectionCount.Text = Convert.ToString(countCheckedResultsRows()) + " selected";
        }

        private void chkOnlyUpdateFirstFew_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOnlyUpdateFirstFew.Checked)
                txtFirstFewToUpdate.Enabled = true;
            else
                txtFirstFewToUpdate.Enabled = false;
        }

        // add an event to the log window
        private void addLogEvent(string eventText, bool includeTime, string textColor, logOutputTarget outputTarget)
        {
            try
            {
                // format text
                string fullEventText = "";
                if (includeTime)
                {
                    fullEventText = DateTime.Now.ToLongTimeString() + ": " + eventText + "\n";
                }
                else
                {
                    fullEventText = eventText + "\n";
                }

                // if required write to display
                if (outputTarget == logOutputTarget.displayAndFile || outputTarget == logOutputTarget.displayOnly)
                {
                    richTextLog.SelectionColor = System.Drawing.Color.FromName(textColor);
                    DrawingControl.SuspendDrawing(richTextLog);

                    richTextLog.AppendText(fullEventText);

                    // if too long truncate the beginning of the log
                    if (richTextLog.Text.Length > maxLogBuffer)
                    {
                        richTextLog.SelectionStart = 0;
                        richTextLog.SelectionLength = fullEventText.Length - 1;
                        richTextLog.ReadOnly = false;
                        richTextLog.SelectedText = "";
                        richTextLog.ReadOnly = true;
                    }
                    // scroll to end
                    richTextLog.SelectionStart = richTextLog.Text.Length;
                    richTextLog.ScrollToCaret();
                }
                // if required write to file
                if (outputTarget == logOutputTarget.displayAndFile || outputTarget == logOutputTarget.fileOnly)
                {
                    eventLog.WriteLine(fullEventText);
                }

            }
            catch (Exception err)
            {
                richTextLog.SelectionColor = System.Drawing.Color.FromName("Red");
                richTextLog.AppendText(DateTime.Now.ToLongTimeString() + ": ERROR occured writing to log: " + err.Message + "\n");
                // scroll to end
                richTextLog.SelectionStart = richTextLog.Text.Length;
                richTextLog.ScrollToCaret();
            }

            DrawingControl.ResumeDrawing(richTextLog);
        }

        private void stopToolStripButton_Click(object sender, EventArgs e)
        {
            stopProcessing = true;
        }

        private void clearToolStripButton_Click(object sender, EventArgs e)
        {
            switch (tabMain.SelectedIndex)
            {
                case 2:
                    if (MessageBox.Show(this, "Clear all unselected rows?", "Clear Results", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        // remove all unselected rows
                        this.Cursor = Cursors.WaitCursor;
                        DrawingControl.SuspendDrawing(dataGridResults);
                        for (int i = dataGridResults.Rows.Count - 1; i >= 0; i--)
                        {
                            if (Convert.ToBoolean(dataGridResults.Rows[i].Cells[0].Value) == false)
                            {
                                dataGridResults.Rows.RemoveAt(i);
                            }
                        }
                        DrawingControl.ResumeDrawing(dataGridResults);
                        this.Cursor = Cursors.Arrow;
                        // update tool strip count
                        toolStripSelectionCount.Text = Convert.ToString(countCheckedResultsRows()) + " selected";
                        toolStripStatusResultsCount.Text = dataGridResults.Rows.Count.ToString() + " results";
                        if (dataGridResults.Rows.Count == 0)
                        {
                            dataGridResults.Visible = false;
                        }
                    }
                    break;
                case 4:
                    if (MessageBox.Show(this, "Clear the log?", "Clear Log", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        richTextLog.Clear();
                    }
                    break;
                default:
                    break;
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
            aboutBox.Close();
        }

        // refresh modifier list view
        private void refreshModifierList()
        {
            lstViewModifiers.Items.Clear();
            string[] itemArray;
            ListViewItem lstViewItem;
            foreach(object modifier in modifierList)
            {
                switch(modifier.GetType().Name)
                {
                    case "ModifierUpdateText":
                        ModifierUpdateText updateTextModifier = (ModifierUpdateText)modifier;
                        itemArray = new string[4] { updateTextModifier.Enabled.ToString(), "Update Text", updateTextModifier.xpath, updateTextModifier.updateText };
                        lstViewItem = new ListViewItem(itemArray);
                        //set color to red if invlaid
                        try
                        {
                            updateTextModifier.validate();
                            lstViewItem.ForeColor = Color.Black;
                        }
                        catch 
                        {
                            lstViewItem.ForeColor = Color.Red; 
                        }                        
                        lstViewModifiers.Items.Add(lstViewItem);
                        break;

                    case "ModifierAddXML":
                        ModifierAddXML addXMLModifier = (ModifierAddXML)modifier;
                        itemArray = new string[4] { addXMLModifier.Enabled.ToString(), "Add XML", addXMLModifier.xpath, addXMLModifier.addXML };
                        lstViewItem = new ListViewItem(itemArray);
                        //set color to red if invlaid
                        try
                        {
                            addXMLModifier.validate();
                            lstViewItem.ForeColor = Color.Black;
                        }
                        catch
                        {
                            lstViewItem.ForeColor = Color.Red;
                        }
                        lstViewModifiers.Items.Add(lstViewItem);
                        break;
                    case "ModifierRenameNode":
                        ModifierRenameNode renameNodeModifier = (ModifierRenameNode)modifier;
                        itemArray = new string[4] { renameNodeModifier.Enabled.ToString(), "Rename Node", renameNodeModifier.currentXpath, renameNodeModifier.renamedNode };
                        lstViewItem = new ListViewItem(itemArray);
                        //set color to red if invlaid
                        try
                        {
                            renameNodeModifier.validate();
                            lstViewItem.ForeColor = Color.Black;
                        }
                        catch
                        {
                            lstViewItem.ForeColor = Color.Red;
                        }
                        lstViewModifiers.Items.Add(lstViewItem);
                        break;
                    case "ModifierRemoveNode":
                        ModifierRemoveNode removeNodeModifier = (ModifierRemoveNode)modifier;
                        itemArray = new string[4] { removeNodeModifier.Enabled.ToString(), "Remove Node", removeNodeModifier.xpath, "" };
                        lstViewItem = new ListViewItem(itemArray);
                        try
                        {
                            removeNodeModifier.validate();
                            lstViewItem.ForeColor = Color.Black;
                        }
                        catch
                        {
                            lstViewItem.ForeColor = Color.Red;
                        }
                        lstViewModifiers.Items.Add(lstViewItem);
                        break;
                    default:
                        MessageBox.Show(modifier.GetType().Name + " not a recognized modifier", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        // add Update Text Modifier
        private void btnModifierUpdateText_Click(object sender, EventArgs e)
        {
            ModifierUpdateTextForm modifierUpdateTextForm = new ModifierUpdateTextForm();

            modifierUpdateTextForm.modifier = new ModifierUpdateText();
            modifierUpdateTextForm.ShowDialog(this);

            if (modifierUpdateTextForm.okPressed)
            {
                //add modifier to modifier list
                modifierList.Add(modifierUpdateTextForm.modifier);
                refreshModifierList();
            }
            modifierUpdateTextForm.Close();

        }

        // add Add XML Modifier
        private void btnModifierAddXml_Click(object sender, EventArgs e)
        {
            ModifierAddXMLForm modifierAddXMLForm = new ModifierAddXMLForm();

            modifierAddXMLForm.modifier = new ModifierAddXML();
            modifierAddXMLForm.ShowDialog(this);

            if (modifierAddXMLForm.okPressed)
            {
                //add modifier to modifier list
                modifierList.Add(modifierAddXMLForm.modifier);
                refreshModifierList();
            }
            modifierAddXMLForm.Close();
        }

        // add Rename Node Modifier
        private void btnModifierRenameNode_Click(object sender, EventArgs e)
        {
            ModifierRenameNodeForm modifierRenameNodeForm = new ModifierRenameNodeForm();

            modifierRenameNodeForm.modifier = new ModifierRenameNode();
            modifierRenameNodeForm.ShowDialog(this);

            if (modifierRenameNodeForm.okPressed)
            {
                //add modifier to modifier list
                modifierList.Add(modifierRenameNodeForm.modifier);
                refreshModifierList();
            }
            modifierRenameNodeForm.Close();
        }

        // add Remove Node Modifier
        private void btnModifierRemoveXml_Click(object sender, EventArgs e)
        {
            ModifierRemoveNodeForm modifierRemoveNodeForm = new ModifierRemoveNodeForm();

            modifierRemoveNodeForm.modifier = new ModifierRemoveNode();
            modifierRemoveNodeForm.ShowDialog(this);

            if (modifierRemoveNodeForm.okPressed)
            {
                //add modifier to modifier list
                modifierList.Add(modifierRemoveNodeForm.modifier);
                refreshModifierList();
            }
            modifierRemoveNodeForm.Close();
        }

        private void btnEditModifier_Click(object sender, EventArgs e)
        {
            editModifier();
        }

        private void lstViewModifiers_DoubleClick(object sender, EventArgs e)
        {
            editModifier();
        }

        public void editModifier()
        {
            ListView.SelectedIndexCollection indexes = lstViewModifiers.SelectedIndices;
            if (indexes.Count > 0)
            {
                int index = indexes[0];
                switch (modifierList[index].GetType().Name)
                {
                    case "ModifierUpdateText":
                        ModifierUpdateText updateTextModifier = (ModifierUpdateText)modifierList[index];

                        ModifierUpdateTextForm modifierUpdateTextForm = new ModifierUpdateTextForm();

                        modifierUpdateTextForm.modifier = updateTextModifier;
                        modifierUpdateTextForm.ShowDialog(this);

                        if (modifierUpdateTextForm.okPressed)
                        {
                            //update modifier in modifier list
                            modifierList[index] = modifierUpdateTextForm.modifier;
                            refreshModifierList();
                        }
                        modifierUpdateTextForm.Close();

                        break;

                    case "ModifierAddXML":
                        ModifierAddXML addXMLModifier = (ModifierAddXML)modifierList[index];

                        ModifierAddXMLForm modifierAddXMLForm = new ModifierAddXMLForm();

                        modifierAddXMLForm.modifier = addXMLModifier;
                        modifierAddXMLForm.ShowDialog(this);

                        if (modifierAddXMLForm.okPressed)
                        {
                            //update modifier in modifier list
                            modifierList[index] = modifierAddXMLForm.modifier;
                            refreshModifierList();
                        }
                        modifierAddXMLForm.Close();

                        break;
                    case "ModifierRenameNode":
                        ModifierRenameNode renameNodeModifier = (ModifierRenameNode)modifierList[index];

                        ModifierRenameNodeForm modifierRenameNodeForm = new ModifierRenameNodeForm();

                        modifierRenameNodeForm.modifier = renameNodeModifier;
                        modifierRenameNodeForm.ShowDialog(this);

                        if (modifierRenameNodeForm.okPressed)
                        {
                            //update modifier in modifier list
                            modifierList[index] = modifierRenameNodeForm.modifier;
                            refreshModifierList();
                        }
                        modifierRenameNodeForm.Close();

                        break;
                    case "ModifierRemoveNode":
                        ModifierRemoveNode removeNodeModifier = (ModifierRemoveNode)modifierList[index];

                        ModifierRemoveNodeForm modifierRemoveNodeForm = new ModifierRemoveNodeForm();

                        modifierRemoveNodeForm.modifier = removeNodeModifier;
                        modifierRemoveNodeForm.ShowDialog(this);

                        if (modifierRemoveNodeForm.okPressed)
                        {
                            //update modifier in modifier list
                            modifierList[index] = modifierRemoveNodeForm.modifier;
                            refreshModifierList();
                        }
                        modifierRemoveNodeForm.Close();

                        break;
                    default:
                        MessageBox.Show(modifierList[index].GetType().Name + " not a recognized modifier", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }

        }

        // move selected modifier up
        private void btnRemoveModifier_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = lstViewModifiers.SelectedIndices;
            if (indexes.Count > 0)
            {
                for (int index = indexes.Count - 1; index >= 0; index-- )
                {
                    modifierList.RemoveAt(indexes[index]);
                }
                refreshModifierList();

            }
        }

        // move selected modifier down
        private void btnMoveModifierUp_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = lstViewModifiers.SelectedIndices;
            if (indexes.Count > 0)
            {
                lstViewModifiers.Focus();
                if (indexes[0] != 0)
                {
                    int[] selectedListItems = new int[indexes.Count];
                    for (int i = 0; i < indexes.Count; i++)
                    {
                        int selectedIndex = indexes[i];
                        selectedListItems[i] = selectedIndex;
                        var temp = modifierList[selectedIndex];
                        modifierList[selectedIndex] = modifierList[selectedIndex - 1];
                        modifierList[selectedIndex - 1] = temp;
                    }
                    refreshModifierList();
                    for (int i = 0; i < selectedListItems.Length; i++)
                    {
                        lstViewModifiers.Items[selectedListItems[i] - 1].Selected = true;
                    }
                }
            }
        }

        private void btnMoveModifierDown_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = lstViewModifiers.SelectedIndices;
            if (indexes.Count > 0)
            {
                lstViewModifiers.Focus();
                if (indexes[indexes.Count - 1] != lstViewModifiers.Items.Count - 1)
                {
                    int[] selectedListItems = new int[indexes.Count];
                    for (int i = indexes.Count - 1; i > -1; i--)
                    {
                        int selectedIndex = indexes[i];
                        selectedListItems[i] = selectedIndex;
                        var temp = modifierList[selectedIndex];
                        modifierList[selectedIndex] = modifierList[selectedIndex + 1];
                        modifierList[selectedIndex + 1] = temp;
                    }
                    refreshModifierList();
                    for (int i = 0; i < selectedListItems.Length; i++)
                    {
                        lstViewModifiers.Items[selectedListItems[i] + 1].Selected = true;
                    }
                }
            }
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            export(false);
        }

        private void btnExportSelection_Click(object sender, EventArgs e)
        {
            export(true);
        }

        // export results datagrid to csv
        private void export(bool selectionOnly)
        {
            SaveFileDialog saveExportFileDialog = new SaveFileDialog();
            saveExportFileDialog.Filter = "CSV (Comma delimited) |*.csv|All files|*.*";
            if (saveExportFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    StreamWriter writer = new StreamWriter(saveExportFileDialog.FileName);
                    WriteToStream(writer, true, true, selectionOnly);
                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception err)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(err.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        public void WriteToStream(TextWriter stream, bool header, bool quoteall, bool selectionOnly)
        {
            if (header)
            {
                for (int i = 1; i <  dataGridResults.Columns.Count; i++)
                {
                    WriteItem(stream, dataGridResults.Columns[i].HeaderText, quoteall);
                    if (i < dataGridResults.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write('\n');
                }
            }
            foreach (DataGridViewRow row in dataGridResults.Rows)
            {
                for (int i = 1; i < dataGridResults.Columns.Count; i++)
                {
                    if (!selectionOnly || (bool)row.Cells[0].Value)
                    {
                        WriteItem(stream, row.Cells[i].Value, quoteall);
                        if (i < dataGridResults.Columns.Count - 1)
                            stream.Write(',');
                        else
                            stream.Write('\n');
                    }
                }
            }
            stream.Flush();
            stream.Close();
        }

        private void WriteItem(TextWriter stream, object item, bool quoteall)
        {
            if (item == null)
                return;
            string s = item.ToString();
            if (quoteall || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
            else
                stream.Write(s);
            stream.Flush();
        }

        // save profile
        private void saveProfile(string profileLocation)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                Profile profile = new Profile();

                // connection tab
                profile.institutionUrl = txtInstitutionUrl.Text;
                profile.sharedSecretLogin = rdioSharedSecret.Checked;
                profile.username = txtUsername.Text;
                // encrypt password
                profile.password = RijndaelSimple.Encrypt(txtPassword.Text,
                                                            passPhrase,
                                                            saltValue,
                                                            hashAlgorithm,
                                                            passwordIterations,
                                                            initVector,
                                                            keySize);
                profile.ssUsername = txtSSUsername.Text;
                profile.sharedSecretID = txtSharedSecretID.Text;
                // encrypt shared secret
                profile.sharedSecret = RijndaelSimple.Encrypt(txtSharedSecret.Text,
                                                            passPhrase,
                                                            saltValue,
                                                            hashAlgorithm,
                                                            passwordIterations,
                                                            initVector,
                                                            keySize);
                profile.logFileLocation = txtLogFileLocation.Text;

                // query tab
                profile.whereStatement = txtWhereStatement.Text;
                profile.freetextQuery = txtFreeTextQuery.Text;
                profile.allCollections = rdioAllCollections.Checked;
                for (int i = 0; i < lstCollections.Items.Count; i++)
                {
                    profile.collectionsList.list.Add(lstCollections.Items[i]);
                }
                profile.columnsIncludeName = chkIncludeColumnsName.Checked;
                profile.columnsIncludeItemID = chkIncludeColumnsID.Checked;
                profile.columnsIncludeItemVersion = chkIncludeColumnsVersion.Checked;
                for (int i = 0; i < lstColumns.Items.Count; i++)
                {
                    profile.columnsList.list.Add(lstColumns.Items[i]);
                }
                profile.sortOrder = cmbSortOrder.SelectedItem.ToString();
                profile.reverseSort = chkReverseOrder.Checked;
                profile.includeNonLive = chkIncludeNonLive.Checked;

                // update tab
                profile.modifierList.list = modifierList;
                profile.updateAllResults = rdioUpdateAllResults.Checked;
                profile.updateFirstFewOnly = chkOnlyUpdateFirstFew.Checked;
                profile.updateFirstFewNumber = Convert.ToInt16(txtFirstFewToUpdate.Text);
                profile.logOutputSize = cmbLogOutput.SelectedIndex;
                profile.createLogFile = chkCreateLogFiles.Checked;

                XmlSerializer s = new XmlSerializer(typeof(Profile));
                TextWriter w = new StreamWriter(profileLocation);
                s.Serialize(w, profile);
                w.Close();
                currentProfileFile = profileLocation;
                this.Cursor = Cursors.Arrow;

            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(err.Message, "Profile Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadProfile(string profileLocation)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                currentProfileFile = profileLocation;

                XmlSerializer s = new XmlSerializer(typeof(Profile));
                TextReader r = new StreamReader(profileLocation);
                Profile profile;
                profile = (Profile)s.Deserialize(r);
                r.Close();

                // connection tab
                txtInstitutionUrl.Text = profile.institutionUrl;
                rdioSharedSecret.Checked = profile.sharedSecretLogin;
                rdioEqUser.Checked = !rdioSharedSecret.Checked;
                txtUsername.Text = profile.username;
                txtLogFileLocation.Text = profile.logFileLocation;

                // decrypt password
                txtPassword.Text = RijndaelSimple.Decrypt(profile.password,
                                                            passPhrase,
                                                            saltValue,
                                                            hashAlgorithm,
                                                            passwordIterations,
                                                            initVector,
                                                            keySize);
                txtSSUsername.Text = profile.ssUsername;
                txtSharedSecretID.Text = profile.sharedSecretID;
                // decrypt shared secret
                txtSharedSecret.Text = RijndaelSimple.Decrypt(profile.sharedSecret,
                                                            passPhrase,
                                                            saltValue,
                                                            hashAlgorithm,
                                                            passwordIterations,
                                                            initVector,
                                                            keySize);

                // query tab
                txtWhereStatement.Text = profile.whereStatement;
                txtFreeTextQuery.Text = profile.freetextQuery;
                rdioAllCollections.Checked = profile.allCollections;
                rdioSpecificCollections.Checked = !rdioAllCollections.Checked;
                lstCollections.Items.Clear();
                for (int i = 0; i < profile.collectionsList.list.Count; i++)
                {
                    lstCollections.Items.Add(profile.collectionsList.list[i]);
                }
                chkIncludeColumnsName.Checked = profile.columnsIncludeName;
                chkIncludeColumnsID.Checked = profile.columnsIncludeItemID;
                chkIncludeColumnsVersion.Checked = profile.columnsIncludeItemVersion;
                lstColumns.Items.Clear();
                for (int i = 0; i < profile.columnsList.list.Count; i++)
                {
                    lstColumns.Items.Add(profile.columnsList.list[i]);
                }
                cmbSortOrder.SelectedItem = profile.sortOrder;
                chkReverseOrder.Checked = profile.reverseSort;
                chkIncludeNonLive.Checked = profile.includeNonLive;

                // update tab
                modifierList = profile.modifierList.list;
                refreshModifierList();
                rdioUpdateAllResults.Checked = profile.updateAllResults;
                rdioSelectedResults.Checked = !rdioUpdateAllResults.Checked;
                chkOnlyUpdateFirstFew.Checked = profile.updateFirstFewOnly;
                txtFirstFewToUpdate.Text = profile.updateFirstFewNumber.ToString();
                cmbLogOutput.SelectedIndex = profile.logOutputSize;
                chkCreateLogFiles.Checked = profile.createLogFile;

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show("Profile load error:\n" + err.Message + "\n\nProfile file may be corrupt.", "Profile Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // save profile
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            launchSaveDialog();
        }

        private void launchSaveDialog()
        {
            SaveFileDialog saveProfileFileDialog = new SaveFileDialog();
            saveProfileFileDialog.Title = "Save Profile";
            saveProfileFileDialog.OverwritePrompt = true;
            try
            {
                if (currentProfileFile != "")
                {
                    saveProfileFileDialog.InitialDirectory = Path.GetDirectoryName(currentProfileFile);
                    saveProfileFileDialog.FileName = Path.GetFileName(currentProfileFile);
                }
            }
            finally { }
            saveProfileFileDialog.Filter = "EMU profile|*.emu";
            if (saveProfileFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveProfile(saveProfileFileDialog.FileName);
            }
        }

        // load profile
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openProfileFileDialog = new OpenFileDialog();
            openProfileFileDialog.Title = "Open Profile";
            if (currentProfileFile != "")
            {
                openProfileFileDialog.InitialDirectory = Path.GetDirectoryName(currentProfileFile);
            }
            openProfileFileDialog.Filter = "EMU profiles|*.emu|All files|*.*";
            if (openProfileFileDialog.ShowDialog() == DialogResult.OK)
            {
                loadProfile(openProfileFileDialog.FileName);
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (launchProfile != null)
                loadProfile(launchProfile);
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Clear all settings?", "Clear Settings", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {
                    // connection tab
                    txtInstitutionUrl.Text = "";
                    rdioSharedSecret.Checked = false;
                    rdioEqUser.Checked = !rdioSharedSecret.Checked;
                    txtUsername.Text = "";
                    // decrypt password
                    txtPassword.Text = "";
                    txtSSUsername.Text = "";
                    txtSharedSecretID.Text = "";
                    // decrypt shared secret
                    txtSharedSecret.Text = "";
                    txtLogFileLocation.Text = "";

                    // query tab
                    txtWhereStatement.Text = "";
                    txtFreeTextQuery.Text = "";
                    rdioAllCollections.Checked = true;
                    rdioSpecificCollections.Checked = !rdioAllCollections.Checked;
                    lstCollections.Items.Clear();
                    chkIncludeColumnsName.Checked = true;
                    chkIncludeColumnsID.Checked = true;
                    chkIncludeColumnsVersion.Checked = true;
                    lstColumns.Items.Clear();
                    cmbSortOrder.SelectedItem = "Rank";
                    chkReverseOrder.Checked = false;
                    chkIncludeNonLive.Checked = false;

                    // update tab
                    modifierList.Clear();
                    refreshModifierList();
                    rdioUpdateAllResults.Checked = false;
                    rdioSelectedResults.Checked = !rdioUpdateAllResults.Checked;
                    chkOnlyUpdateFirstFew.Checked = false;
                    txtFirstFewToUpdate.Text = "";
                    cmbLogOutput.SelectedIndex = 0;
                    chkCreateLogFiles.Checked = false;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Clear Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
             if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.S) 
             {
                 launchSaveDialog(); 
             }
 
        }

        private void btnSetLogFileLocation_Click(object sender, EventArgs e)
        {
            if (folderBrowserLogFileLocation.ShowDialog() == DialogResult.OK)
            {
                txtLogFileLocation.Text = folderBrowserLogFileLocation.SelectedPath;
            }
        }

        private void dataGridResults_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            // get item ID and version from data grid
            string itemID = dataGridResults.Rows[dataGridResults.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
            int itemVersion = Convert.ToInt16(dataGridResults.Rows[dataGridResults.SelectedCells[0].RowIndex].Cells[3].Value);


            // check if double clicked on non-header cell
            if (dataGridResults.SelectedCells[0].ColumnIndex > 0)
            {


                string xpath = resultsXpaths[dataGridResults.SelectedCells[0].ColumnIndex - 1].ToString();


                // open form and load note value into it
                ViewNodeXMLForm viewNodeXMLForm = new ViewNodeXMLForm();

                viewNodeXMLForm.nodeText = dataGridResults.SelectedCells[0].Value.ToString();
                viewNodeXMLForm.xpath = xpath;

                // check if xpath in nonEditableNodes
                bool xpathInNonEditableNodes = false;
                for (int i = 0; i < nonEditableNodes.Length; i++)
                {
                    if (nonEditableNodes[i].Length <= xpath.Length)
                    {
                        if (xpath.Substring(0, nonEditableNodes[i].Length) == nonEditableNodes[i])
                        {
                            xpathInNonEditableNodes = true;
                            break;
                        }
                    }
                }

                // if cell is not null and xpath is not system then set form to editable 
                if (!xpathInNonEditableNodes && dataGridResults.SelectedCells[0].Value.ToString() != "<null>")
                {
                    viewNodeXMLForm.editable = true;
                }

                // launch form
                viewNodeXMLForm.ShowDialog(this);

                if (viewNodeXMLForm.nodeEdited)
                {
                    login();

                    // get item for editing
                    equellaClient.unlock(itemID, itemVersion);
                    string oldItemXMLstring = equellaClient.editItem(itemID, itemVersion, false);
                    ModifierUpdateText modifierUpdateText = new ModifierUpdateText(xpath, viewNodeXMLForm.newNodeText);
                    string newItemXMLstring = modifierUpdateText.transform(oldItemXMLstring);

                    equellaClient.saveItem(newItemXMLstring, true);

                    // get item system name and update in grid
                    string updatedItemXMLString = equellaClient.getItem(itemID, itemVersion, "");
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(updatedItemXMLString);
                    XmlElement rootElement = (XmlElement)xmlDoc.GetElementsByTagName("xml")[0];
                    XmlNode nameNode = rootElement.SelectSingleNode("item/name");
                    Utils utils = new Utils();
                    XmlNode nameTextNode = utils.GetTextNode((XmlElement)nameNode);
                    dataGridResults.Rows[dataGridResults.SelectedCells[0].RowIndex].Cells[1].Value = nameTextNode.Value;
                    
                    dataGridResults.SelectedCells[0].Value = viewNodeXMLForm.newNodeText;

                    equellaClient.logout();
                }

                viewNodeXMLForm.Close();

            }
            else
            {
                // check if double clicked on row header cell
                if (dataGridResults.SelectedCells[0].ColumnIndex == 0 && dataGridResults.SelectedCells.Count > 1)
                {
                    // get item XML
                    login();
                    string unindentedItemXML = equellaClient.getItem(itemID, itemVersion, "");
                    equellaClient.logout();

                    // indent the XML
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(unindentedItemXML);
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    xw.Formatting = Formatting.Indented;
                    xmlDoc.WriteTo(xw);
                    string indentedItemXMLString = sw.ToString();

                    // open form and load indented xml into it
                    ViewItemXMLForm viewItemXMLForm = new ViewItemXMLForm();
                    viewItemXMLForm.XMLtext = indentedItemXMLString;
                    viewItemXMLForm.ShowDialog(this);
                }
            }

        }

        private void lstColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstColumns.SelectedIndex != -1)
            {
                cmbNewItem.Text = lstColumns.SelectedItem.ToString();
            }
        }

    }
    //DrawingControl for controlling a Windows control's redraw behavior
    public class DrawingControl
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
    } 
}
