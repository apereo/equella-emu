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
using LumenWorks.Framework.IO.Csv;


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

        public EquellaClient equellaClient;

        private ModifiersControl modifiersControl = new ModifiersControl();
        private Logger scriptLogger = new Logger();
        private Hashtable scriptVariables = new Hashtable();

        private string lastErrorDescription;
        private bool stopProcessing = false;
        private bool processing = false;
        private bool dirtyUI = false;
        private bool mainFormShown = false;
        private ArrayList modifierList = new ArrayList();
        private string currentProfileFile = "";
        private StreamWriter eventLog;
        private ArrayList resultsXpaths = new ArrayList();
        private ArrayList resultsXpathsSelected = new ArrayList();
        private string[] nonEditableNodes = {
                                                "/xml/item/name",
                                                "/xml/item/description",
                                                "/xml/item/@itemstatus",
                                                "/xml/item/@id",
                                                "/xml/item/@version",
                                                "/xml/item/@key",
                                                "/xml/item/@itemdefid",
                                                "/xml/item/datecreated",
                                                "/xml/item/datemodified",
                                                "/xml/item/dateforindex",
                                                "/xml/item/owner",
                                                "/xml/item/rating",
                                                "/xml/item/badurls",
                                                "/xml/item/moderation",
                                                "/xml/item/newitem",
                                                "/xml/item/attachments",
                                                "/xml/item/navigationNodes",
                                                "/xml/item/url",
                                                "/xml/item/history"
                                            };

        private object[] rowCells;
        private Utils utils = new Utils();

        public enum logOutputTarget { displayOnly, fileOnly, displayAndFile }
        public bool systemMetadataHidden = true; 

        public string launchProfile;

        public struct itemversionid
        {
            public string itemID;
            public int itemVersion;
        }

        public MainForm()
        {
            InitializeComponent();

            this.Text = "No Institution - EMU";

            rdioEqUser.Checked = true;
            rdioAllCollections.Checked = true;

            grpSharedSecret.Enabled = false;

            cmbSortOrder.SelectedItem = "Rank";
            cmbLogOutput.SelectedIndex = 0;

            this.KeyPreview = true;

            currentProfileFile = Properties.Settings.Default.lastProfile;

            scriptLogger.loggingForm = this;

            dirtyUI = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (launchProfile != null)
            {
                loadProfile(launchProfile);

            }

            // Set Modifier tool tips
            modifierToolTip.SetToolTip(btnModifierUpdateText, "Updates all text in matching elements or attributes by replacing the existing text");
            modifierToolTip.SetToolTip(btnModifierAddXml, "Appends an XML fragment to matching elements");
            modifierToolTip.SetToolTip(btnModifierCopyXml, "Clones an entire subtree and appends copies of it to all matching elements");
            modifierToolTip.SetToolTip(btnModifierRemoveXml, "Deletes each matching attribute or the entire subtree at eaching matching element");
            modifierToolTip.SetToolTip(btnModifierRenameNode, "Renames all matching elements or attributes");
            modifierToolTip.SetToolTip(btnModifierReplaceText, "Replaces all occurences of a string in each matching element of attribute");
            modifierToolTip.SetToolTip(btnModifierXSLT, "Applies an XSLT transformation to the entire XML document");

            dataGridColumns.Columns[0].DefaultCellStyle.Font = new Font("Courier New", 8.25f);


            MainForm_PositionObjects();
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
                if (!chkUseProxy.Checked)
                {
                    equellaClient = new EquellaClient(endPoint);
                }
                else
                {
                    equellaClient = new EquellaClient(endPoint, txtProxyAddress.Text, txtProxyUsername.Text, txtProxyPassword.Text);
                }
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
                if (err.Message.Contains("<html lang=\"en\">") || err.Message.Contains("the response message does not match the content type"))
                {
                    lastErrorDescription = "Receiving back a web page instead of a SOAP response from '" + txtInstitutionUrl.Text + "'. This is often caused by an incorrect institution URL.";
                }
                if (err.Message.Contains("The remote server returned an unexpected response"))
                {
                    lastErrorDescription = err.Message + " This is often caused by an incorrect institution URL.";
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
                    if (chkUseProxy.Checked)
                    {
                        lastErrorDescription = "Either Institution URL or Proxy Address could not be parsed.";
                    }
                    else
                    {
                        lastErrorDescription = "'" + txtInstitutionUrl.Text + "' is an invalid institution URL. It should be of the form \"http://server_address/insitution_name\"";
                    }
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
            dirtyUI = true;
        }


        // run query
        private void cmdRunQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string delimiter = txtDelimiter.Text.Replace("\\n", "\r\n");
                delimiter = delimiter.Replace("\\t", "\t");

                // connect to EQUELLA
                this.Cursor = Cursors.WaitCursor;
                
                processing = true;
                stopToolStripButton.Enabled = true;
                tabMain.Enabled = false;
                dataGridResults.Visible = false;
                lblSearchPanelBackground.Text = "Searching...";
                tabMain.SelectTab(2);
                Application.DoEvents();

                int limit;
                if (txtLimit.Text.Trim() == "")
                {
                    limit = -1;
                }
                else
                {
                    try
                    {
                        limit = Convert.ToInt32(txtLimit.Text.Trim());
                    }
                    catch (Exception err)
                    {
                        throw new System.InvalidOperationException("Error reading limit. " + err.Message);
                    }
                }

                login();

                // clear results from last query
                dataGridResults.Rows.Clear();

                toolStripProgressBar.Visible = true;
                toolStripStatusResultsCount.Text = "0 results";
                toolStripSelectionCount.Text = "0 selected";
                statusStrip1.Refresh();
                tabMain.Refresh();

                // populate query variables from data in the Query tab
                //freetext query
                string freetext = txtFreeTextQuery.Text;
                // collection filter
                var collections = new EquellaMetadataUtility.EQUELLA.ArrayOfString {};
                if ((rdioSpecificCollections.Checked)&&(txtItemUUID.Text.Trim() == ""))
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
                {
                    dataGridResults.Columns[3].Visible = true;
                    dataGridResults.Columns[3].Width = 40;
                }
                else
                    dataGridResults.Columns[3].Visible = false;

                // get previous column widths
                Hashtable lastColumnWidths = new Hashtable();
                for (int i = 0; i < resultsXpaths.Count; i++)
                {
                    lastColumnWidths[resultsXpaths[i]] = dataGridResults.Columns[i + 1].Width;
                }

                resultsXpaths.Clear();
                resultsXpathsSelected.Clear();
                resultsXpaths.Add("/xml/item/name");
                resultsXpaths.Add("/xml/item/@id");
                resultsXpaths.Add("/xml/item/@version");

                // set column headers
                dataGridResults.ColumnCount = systemColumns;
                string columnXpath;
                string columnHeader;
                for (int i = 0; i < dataGridColumns.Rows.Count - 1; i++)
                {
                    string columnStatement = dataGridColumns.Rows[i].Cells[0].Value.ToString();

                    // determine any aliases
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
                    dataGridResults.Columns["column" + i.ToString()].ReadOnly = true;
                    resultsXpaths.Add(columnXpath);
                    if (Convert.ToBoolean(dataGridColumns.Rows[i].Cells[1].Value))
                    resultsXpathsSelected.Add(i + 3);
                }
                Application.DoEvents();

                // set column widths
                for (int i = 0; i < resultsXpaths.Count; i++)
                {
                    if (lastColumnWidths.Contains(resultsXpaths[i]))
                    {
                        dataGridResults.Columns[i + 1].Width = (int)lastColumnWidths[resultsXpaths[i]];

                    }

                }

                // create results document
                XmlDocument resultsXML = new XmlDocument();
                XmlNode resultsNode = resultsXML.AppendChild(resultsXML.CreateElement("results"));

                
                int resultsCount;
                const int pageSize = 47;
                int offset = 0;
                toolStripProgressBar.Step = 1;

                // if retrieving by UUID then instead of searching "manufacture" resultsXML 
                // by "wrapping" item version XML with /results/result and adding @count and /available
                if (txtItemUUID.Text.Trim() != "")
                {
                    ArrayList itemVersions = new ArrayList();
                    int highestVersion = -2;
                    string highestVersionXMLString = "";

                    // if no version specified determine highest version
                    if (txtItemVersion.Text.Trim() == "")
                    {
                        try
                        {
                            // get highest version
                            try
                            {
                                // try for latest version (version = -1) regardless of status (not supported by all versions of EQUELLA)
                                highestVersionXMLString = equellaClient.getItem(txtItemUUID.Text.Trim(), -1, "");
                            }
                            catch (Exception err)
                            {
                                // failing latest any-version, try for latest live version (version = 0)
                                highestVersionXMLString = equellaClient.getItem(txtItemUUID.Text.Trim(), 0, "");
                            }
                            XmlDocument itemXml = new XmlDocument();
                            itemXml.LoadXml(highestVersionXMLString);
                            highestVersion = Convert.ToInt32(itemXml.SelectSingleNode("xml/item/@version").Value);
                        }
                        catch (Exception err)
                        {
                            // ignore "item not found" errors
                            if (!err.Message.Contains("Item not found")) throw new System.InvalidOperationException(err.Message);
                        }

                        // list all possible versions to attempt to retrieve
                        for (int i = highestVersion; i > 0; i--)
                        {
                            itemVersions.Add(i);
                        }
                    }
                    else
                    {
                        // use version specified by user
                        try
                        {
                            itemVersions.Add(Convert.ToInt32(txtItemVersion.Text.Trim()));
                        }
                        catch(Exception err)
                        {
                            MessageBox.Show("Error reading item version. " + err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                    // get item XML and append to result node and append to results XML document
                    XmlNode resultNode;

                    // iterate through all possible versions of item adding any found ones to resultsXml document
                    resultsCount = 0;
                    string itemXMLString;
                    foreach (int itemVersion in itemVersions)
                    {
                        try
                        {
                            // get item, form fragment and append to "manufactured" search results XML document
                            if (itemVersion != highestVersion)
                                itemXMLString = equellaClient.getItem(txtItemUUID.Text.Trim(), itemVersion, "");
                            else
                                itemXMLString = highestVersionXMLString;

                            XmlDocumentFragment xfrag = resultsXML.CreateDocumentFragment();
                            xfrag.InnerXml = itemXMLString;
                            resultNode = resultsNode.AppendChild(resultsXML.CreateElement("result"));
                            resultNode.AppendChild(xfrag);
                            resultsNode.AppendChild(resultNode);
                            resultsCount++;
                        }
                        catch(Exception err)
                        {
                            // ignore "item not found" errors
                            if (!err.Message.Contains("Item not found")) throw new System.InvalidOperationException(err.Message);
                        }
                    }
                    
                    // add results and available count
                    resultsNode.AppendChild(resultsXML.CreateElement("available")).AppendChild(resultsXML.CreateTextNode(resultsCount.ToString()));
                    resultsNode.Attributes.Append(resultsXML.CreateAttribute("count")).Value = resultsCount.ToString();

                }
                else
                {
                    // get count of records prior to running query
                    XmlDocument resultsCountXML = new XmlDocument();
                    string resultsCountString = equellaClient.searchItems(freetext, collections, whereClause, onlyLive, orderType, reverseOrder, 0, 1);
                    resultsCountXML.LoadXml(resultsCountString);
                    resultsCount = Convert.ToInt32(resultsCountXML.SelectSingleNode("results/available").InnerXml);
                }

                if (resultsCount > 0)
                {

                    // get results in chunks of pageSize
                    int pagesRequired = resultsCount / pageSize + 1;
                    toolStripProgressBar.Maximum = pagesRequired;

                    rowCells = new object[dataGridResults.ColumnCount];
                    int currentPageSize;
                    toolStripStatusProgressText.Visible = true;
                    int rowCount = 0;

                    for (int j = 0; j < pagesRequired; j++)
                    {
                        // hold off on revealing grid until first chunk is filled in
                        if (j > 1)
                        {
                            dataGridResults.Visible = true;
                            this.Cursor = Cursors.Arrow;
                        }

                        // increment progress bar
                        toolStripProgressBar.Value = j + 1;
                        toolStripStatusProgressText.Text = "Retrieving " + Convert.ToString(offset) + "/" + resultsCount;

                        // if retrieving by UUID then skip search and instead use resultsXML formed by UUID retreival above
                        if (txtItemUUID.Text.Trim() == "")
                        {
                            // get chunk from EQUELLA
                            resultsXML.LoadXml(equellaClient.searchItems(freetext, collections, whereClause, onlyLive, orderType, reverseOrder, offset, pageSize));
                        }

                        // get current page size
                        currentPageSize = Convert.ToInt32(resultsXML.SelectSingleNode("results/@count").Value);

                        // loop through chunk
                        for (int i = 0; i < currentPageSize; i++)
                        {
                            rowCount += 1;
                            
                            // add row to grid
                            dataGridResults.Rows.Add();
                            dataGridResults.Rows[rowCount - 1].HeaderCell.Value = rowCount.ToString();
                            populateDataGridResultsRow(rowCount - 1, resultsXML.ChildNodes[0].ChildNodes[i], delimiter);
                   
                            // check for user events
                            Application.DoEvents();

                            // check if query has been cancelled
                            if (stopProcessing) break;
                            
                            // check if limit specified and reached
                            if (limit != -1)
                                if (rowCount >= limit)
                                    break;
                        }

                        // update counts
                        toolStripStatusResultsCount.Text = dataGridResults.Rows.Count.ToString() + " results";
                        toolStripSelectionCount.Text = Convert.ToString(countCheckedResultsRows()) + " selected";

                        // check if query has been cancelled
                        if (stopProcessing) break;

                        // check if limit specified and reached
                        if (limit != -1)
                            if (rowCount >= limit)
                                break;

                        offset += pageSize;

                    }

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
                toolStripStatusProgressText.Visible = false;

                // workaround for scrollbar bug in data grid
                dataGridResults.Width += 1;
                dataGridResults.Width -= 1;
                
                stopToolStripButton.Enabled = false;
                lblSearchPanelBackground.Text = "No results.";
                this.Cursor = Cursors.Arrow;
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

            lblSearchPanelBackground.Text = "No results.";
            toolStripStatusResultsCount.Text = dataGridResults.Rows.Count.ToString() + " results";
            toolStripStatusProgressText.Visible = false;

            stopToolStripButton.Enabled = false;

            tabMain.Enabled = true;

        }

        public int getItemGridRow(string itemID, int itemVersion)
        {
            // find item/version in datagridresults
            for (int i = 0; i < dataGridResults.Rows.Count; i++)
            {
                if (dataGridResults.Rows[i].Cells[2].Value.ToString() == itemID)
                    if (dataGridResults.Rows[i].Cells[3].Value.ToString() == itemVersion.ToString())
                    {
                        return i;
                    }
            }
            return -1;
        }

        public itemversionid getItemIDByGridRow(int gridRow)
        {
            itemversionid itemVersionId;
            if (dataGridResults.Rows.Count > 0)
            {
                if (dataGridResults.Rows.Count < gridRow) gridRow = 0;
                itemVersionId.itemID = dataGridResults.Rows[gridRow].Cells[2].Value.ToString();
                itemVersionId.itemVersion = Convert.ToInt16(dataGridResults.Rows[gridRow].Cells[3].Value);
            }
            else
            {
                itemVersionId.itemID = "";
                itemVersionId.itemVersion = -1;
            }
            return itemVersionId;
        }

        public int getGridRowCount()
        {
            return dataGridResults.Rows.Count;
        }

        public void selectGridRow(int gridRow)
        {
            if (gridRow < dataGridResults.Rows.Count)
                dataGridResults.Rows[gridRow].Selected = true;
        }

        public void updateDataGridResultsRow(string itemID, int itemVersion, XmlNode resultNode)
        {
            // find item/version in datagridresults and update with resultNode
            
            string delimiter = txtDelimiter.Text.Replace("\\n", "\r\n");
            delimiter = delimiter.Replace("\\t", "\t");

            int gridRow = getItemGridRow(itemID, itemVersion);
            if (gridRow != -1) populateDataGridResultsRow(gridRow, resultNode, delimiter);
        }

        private void populateDataGridResultsRow(int gridRow, XmlNode resultNode, string delimiter)
        {

            bool selectRow = false;
            bool unmetSelectionFound = false;
            string xpath;

            // get values from xpaths and use Value prop for attributes and InnerXml for nodes
            for (int k = 0; k < resultsXpaths.Count; k++)
            {
                xpath = resultsXpaths[k].ToString();

                // if element is "result" then trim leading slash
                if (resultNode.Name == "result")
                {
                    if (xpath.Substring(0, 1) == "/") xpath = xpath.Substring(1);
                }

                var xmlReturnNodes = resultNode.SelectNodes(xpath);

                // test node has been returned
                if (xmlReturnNodes.Count > 0)
                {
                    // if one or more nodes has been returned and column is a selection column then mark row to be selected
                    if (resultsXpathsSelected.Contains(k)) selectRow = true;

                    // write either element textnode value or attribute value to datagrid cell
                    if (xmlReturnNodes[0].NodeType == XmlNodeType.Element)
                    {
                        // concatenate repeating nodes with delimiter
                        string value = "";
                        for (int l = 0; l < xmlReturnNodes.Count; l++)
                        {
                            XmlNode nameTextNode = utils.GetTextNode((XmlElement)xmlReturnNodes[l]);
                            // write text node unless it doesn't exist in which case write empty string
                            if (nameTextNode != null)
                                value += nameTextNode.Value + delimiter;
                            else
                                value += delimiter;
                        }
                        // trim last slash
                        dataGridResults.Rows[gridRow].Cells[k + 1].Value = value.Substring(0, value.Length - delimiter.Length);
                    }
                    else
                        if (xmlReturnNodes[0].NodeType == XmlNodeType.Attribute)
                        {
                            dataGridResults.Rows[gridRow].Cells[k + 1].Value = xmlReturnNodes[0].Value;
                        }
                }
                else
                {
                    dataGridResults.Rows[gridRow].Cells[k + 1].Value = "<null>";

                    // no matching nodes found so if column is a "selected" column and if so mark the row for un-selection
                    if (Convert.ToBoolean(resultsXpathsSelected.Contains(k))) unmetSelectionFound = true;
                }
            }
            // "trump" any met selections with an unselection if any found
            if (unmetSelectionFound) selectRow = false;

            // if row should be selected then select the checkbox
            if (selectRow) dataGridResults.Rows[gridRow].Cells[0].Value = true;
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
                case 0:
                    this.AcceptButton = btnTestConnection;
                    break;
                case 1:
                    this.AcceptButton = cmdRunQuery;
                    break;
                case 2:
                    clearToolStripButton.Text = "Clear unselected results";
                    this.AcceptButton = null;
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
                case 3:
                    this.AcceptButton = null;
                    break;
                case 4:
                    clearToolStripButton.Text = "Clear log";
                    this.AcceptButton = null;
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

        // move up columns from columns to return in results
        private void btnMoveUp_Click(object sender, EventArgs e)
        {

            DataGridView grid = dataGridColumns;
            try
            {
                int totalRows = grid.Rows.Count;
                int idx = grid.SelectedCells[0].OwningRow.Index;
                if (idx == 0)
                    return;
                int col = grid.SelectedCells[0].OwningColumn.Index;
                DataGridViewRowCollection rows = grid.Rows;
                DataGridViewRow row = rows[idx];
                rows.Remove(row);
                rows.Insert(idx - 1, row);
                grid.ClearSelection();
                grid.Rows[idx - 1].Cells[col].Selected = true;

            }
            catch { }


        }

        // move down columns from columns to return in results
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            DataGridView grid = dataGridColumns;
            try
            {
                int totalRows = grid.Rows.Count;
                int idx = grid.SelectedCells[0].OwningRow.Index;
                if (idx == totalRows - 2)
                    return;
                int col = grid.SelectedCells[0].OwningColumn.Index;
                DataGridViewRowCollection rows = grid.Rows;
                DataGridViewRow row = rows[idx];
                rows.Remove(row);
                rows.Insert(idx + 1, row);
                grid.ClearSelection();
                grid.Rows[idx + 1].Cells[col].Selected = true;
            }
            catch { }

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
            dirtyUI = true;
        }

        private void btnAddCollection_Click(object sender, EventArgs e)
        {
            try
            {
                Collections collections = new Collections();
                this.Cursor = Cursors.WaitCursor;
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
                            dirtyUI = true;
                        }
                    }
                    rdioSpecificCollections.Checked = true;
                }
                collections.Close();
                this.BringToFront();
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveCollection_Click(object sender, EventArgs e)
        {
            while (lstCollections.SelectedItem != null)
            {
                lstCollections.Items.Remove(lstCollections.SelectedItem);
                dirtyUI = true;
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
            dirtyUI = true;
        }

        // move around controls as necessary when form resizes
        private void MainForm_Resize(object sender, EventArgs e)
        {
            MainForm_PositionObjects();
        }
        private void MainForm_PositionObjects()
        {
            int buttonMargin = 15;

            tabMain.Width = this.ClientSize.Width;
            tabMain.Height = this.ClientSize.Height - statusStrip1.Height - tabMain.Top;
            
            panelResultsBackground.Width = tabMain.Width - 6;
            dataGridResults.Width = panelResultsBackground.Width;
            panelResultsBackground.Height = tabMain.Height - panelResultsBackground.Top - 2 * buttonMargin;
            dataGridResults.Height = panelResultsBackground.Height;
            
            richTextLog.Width = tabMain.Width - 5;
            richTextLog.Height = tabMain.Height - 26;

            int modifiersMgmtButtonsLeft = tabMain.Width - btnMoveModifierDown.Width - buttonMargin;
            lstViewModifiers.Width = modifiersMgmtButtonsLeft - btnModifierAddXml.Width - 2 * buttonMargin;
            btnMoveModifierDown.Left = modifiersMgmtButtonsLeft;
            btnMoveModifierUp.Left = modifiersMgmtButtonsLeft;
            btnEditModifier.Left = modifiersMgmtButtonsLeft;
            btnRemoveModifier.Left = modifiersMgmtButtonsLeft;

            int dataGridColumnsRight = tabMain.Width - btnMoveUp.Width - 2 * buttonMargin;
            if (tabMain.Width > cmdRunQuery.Left + cmdRunQuery.Width + buttonMargin)
            {
                dataGridColumns.Width = dataGridColumnsRight - dataGridColumns.Left;
                btnMoveUp.Left = dataGridColumnsRight + buttonMargin;
                btnMoveDown.Left = btnMoveUp.Left;

            }
            
            int ExecutionGrpBoxTop = tabMain.Height - tabMain.Top - grpboxExecution.Height;
            if (ExecutionGrpBoxTop > btnModifierScript.Top + btnModifierScript.Height)
            {
                
                dataGridColumns.Height = tabMain.Height - dataGridColumns.Top - txtDelimiter.Height - 3 * buttonMargin;
                txtDelimiter.Top = tabMain.Height - txtDelimiter.Height - (int)(2.5 * buttonMargin);
                lblDelimiter.Top = txtDelimiter.Top;
                grpCollections.Height = tabMain.Height - grpCollections.Top - cmbSortOrder.Height - 3 * buttonMargin;
                lblSortOrder.Top = grpCollections.Top + grpCollections.Height + buttonMargin;
                cmbSortOrder.Top = lblSortOrder.Top - 3;
                chkReverseOrder.Top = lblSortOrder.Top;
                lstCollections.Height = grpCollections.Height - rdioAllCollections.Height - rdioSelectedResults.Height - buttonMargin * 3;

                grpboxExecution.Top = ExecutionGrpBoxTop;
                lstViewModifiers.Height = ExecutionGrpBoxTop - lstViewModifiers.Top;
            }
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

            // let scriptLogger know what the form's logTarget is
            scriptLogger.loggingFormLogTarget = logTarget;

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

                string itemCountStatement = scheduledItemsCount.ToString("#,0") + " items";
                if (scheduledItemsCount == 1) itemCountStatement = "1 item";

                bool copyAttachments = false;
                if (!testOnly) copyAttachments = Convert.ToBoolean(chkCopyToStaging.Checked);

                if (testOnly || MessageBox.Show(this, "This will update " + itemCountStatement + " in EQUELLA at " +
                    txtInstitutionUrl.Text + ".\n\nContinue?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    int logFileCount = 0;
                    string startLogFilename = "";
                    string currentLogFilename = "";

                    // clear log on update if specified
                    if (chkClearLogOnUpdate.Checked)
                    {
                        richTextLog.Clear();
                    }

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

                    // empty "vars" item in scriptEngine.VsaEngine
                    scriptVariables.Clear();

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
                                oldItemXMLstring = equellaClient.editItem(itemID, itemVersion, copyAttachments);
                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.LoadXml(oldItemXMLstring);

                                StringWriter sw = new StringWriter();
                                XmlTextWriter xw = new XmlTextWriter(sw);

                                // log input xml appropriately
                                if (cmbLogOutput.SelectedIndex != 2 || logTarget != logOutputTarget.displayOnly)
                                {
                                    // write either indented or unindented item xml to display log
                                    if (cmbLogOutput.SelectedIndex == 0 || logTarget != logOutputTarget.displayOnly)
                                    {
                                        string indentedItemXMLString;
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

                                if (testOnly)
                                {
                                    addLogEvent(" Running modifiers...", false, "Blue", logTarget);
                                }
                                else
                                {
                                    addLogEvent(" Running modifiers and resaving item...", false, "Blue", logTarget);
                                }

                                // update item XML metadata
                                bool cancelSave = updateItemXML(xmlDoc, itemsProcessed);

                                newItemXMLstring = xmlDoc.InnerXml;
                                
                                // log transformed xml appropriately
                                if (cmbLogOutput.SelectedIndex != 2 || logTarget != logOutputTarget.displayOnly)
                                {
                                    //XmlDocument xmlDoc = new XmlDocument();
                                    sw = new StringWriter();
                                    xw = new XmlTextWriter(sw);
                                    xw.Formatting = Formatting.Indented;
                                    xmlDoc.WriteTo(xw);
                                    string indentedItemXMLString = sw.ToString();

                                    // write either indented or unindented item xml to display log
                                    if (cmbLogOutput.SelectedIndex == 0 || logTarget != logOutputTarget.displayOnly)
                                    {

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

                                if ((testOnly) || (cancelSave))
                                {
                                    // cancel item editing
                                    equellaClient.cancelItemEdit(itemID, itemVersion);
                                    if (testOnly) addLogEvent(" Item valid for saving.", false, "Blue", logTarget);
                                    else if (cancelSave) addLogEvent(" Item not saved as per modifier instructions.", false, "Blue", logTarget);
                                }
                                else
                                {
                                    // save item to EQUELLA
                                    equellaClient.saveItem(newItemXMLstring, false);
                                    addLogEvent(" Item sucessfully saved.", false, "Blue", logTarget);
                                    
                                    // if required unselect result in resultsgrid
                                    if (chkUnselectProcessedResults.Checked)
                                    {
                                        dataGridResults.Rows[i].Cells[0].Value = false;
                                        toolStripSelectionCount.Text = Convert.ToString(countCheckedResultsRows()) + " selected";
                                    }
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
                                addLogEvent("ERROR: " + err.Message, false, "Red", logTarget);
                            }

                            itemsProcessed += 1;
                            toolStripStatusProgressText.Text = " Processing " + itemsProcessed.ToString() + "/" + scheduledItemsCount + " (" + (itemsProcessed * 100 / (scheduledItemsCount)).ToString() + "%)";
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
            // non-essential clean up
            try
            {
                // close eventLog file
                if (eventLog != null)
                {
                    eventLog.Close();
                }
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
        private bool updateItemXML(XmlDocument xmlDoc, int itemProcessingIndex)
        {

            // create instance of XMLWrapper "around" xmlDoc
            XMLWrapper xmlWrapper = new XMLWrapper((XmlElement)xmlDoc.SelectSingleNode("/*"));

            // reset modifersControl object
            modifiersControl.skipNext = false;
            modifiersControl.skipRemaining = false;
            modifiersControl.cancelSave = false;

            // iterate through modifiers
            for (int index = 0; index < modifierList.Count; index++)
            {
                // only run this modifier if skipNext = false
                if (!modifiersControl.skipNext)
                {
                    // cast correct modifier object and transform
                    switch (modifierList[index].GetType().Name)
                    {
                        case "ModifierUpdateText":
                            ModifierUpdateText updateTextModifier = (ModifierUpdateText)modifierList[index];
                            if (updateTextModifier.Enabled)
                            {
                                updateTextModifier.modifierPosition = index + 1;
                                updateTextModifier.setRoot((XmlElement)xmlDoc.SelectSingleNode("/*"));
                                updateTextModifier.transform(xmlDoc);

                            }
                            break;

                        case "ModifierAddXML":
                            ModifierAddXML addXMLModifier = (ModifierAddXML)modifierList[index];
                            if (addXMLModifier.Enabled)
                            {
                                addXMLModifier.modifierPosition = index + 1;
                                addXMLModifier.setRoot((XmlElement)xmlDoc.SelectSingleNode("/*"));
                                addXMLModifier.transform(xmlDoc);
                            }
                            break;
                        case "ModifierRenameNode":
                            ModifierRenameNode renameNodeModifier = (ModifierRenameNode)modifierList[index];
                            if (renameNodeModifier.Enabled)
                            {
                                renameNodeModifier.modifierPosition = index + 1;
                                renameNodeModifier.setRoot((XmlElement)xmlDoc.SelectSingleNode("/*"));
                                renameNodeModifier.transform(xmlDoc);
                            }
                            break;
                        case "ModifierRemoveNode":
                            ModifierRemoveNode removeNodeModifier = (ModifierRemoveNode)modifierList[index];
                            if (removeNodeModifier.Enabled)
                            {
                                removeNodeModifier.modifierPosition = index + 1;
                                removeNodeModifier.setRoot((XmlElement)xmlDoc.SelectSingleNode("/*"));
                                removeNodeModifier.transform(xmlDoc);
                            }
                            break;
                        case "ModifierCopyXML":
                            ModifierCopyXML copyXMLModifier = (ModifierCopyXML)modifierList[index];
                            if (copyXMLModifier.Enabled)
                            {
                                copyXMLModifier.modifierPosition = index + 1;
                                copyXMLModifier.setRoot((XmlElement)xmlDoc.SelectSingleNode("/*"));
                                copyXMLModifier.transform(xmlDoc);
                            }
                            break;
                        case "ModifierReplaceText":
                            ModifierReplaceText replaceTextModifier = (ModifierReplaceText)modifierList[index];
                            if (replaceTextModifier.Enabled)
                            {
                                replaceTextModifier.modifierPosition = index + 1;
                                replaceTextModifier.setRoot((XmlElement)xmlDoc.SelectSingleNode("/*"));
                                replaceTextModifier.transform(xmlDoc);
                            }
                            break;
                        case "ModifierXSLT":
                            ModifierXSLT XSLTModifier = (ModifierXSLT)modifierList[index];
                            if (XSLTModifier.Enabled)
                            {
                                XSLTModifier.setRoot((XmlElement)xmlDoc.SelectSingleNode("/*"));
                                XSLTModifier.validate();
                                XSLTModifier.transform(xmlDoc);
                            }
                            break;

                        case "ModifierScript":
                            ModifierScript scriptModifier = (ModifierScript)modifierList[index];
                            if (scriptModifier.Enabled)
                                if (!((scriptModifier.runOnlyOnce) && (itemProcessingIndex != 0)))
                                {
                                    scriptModifier.modifierPosition = index + 1;
                                    scriptModifier.setRoot((XmlElement)xmlDoc.SelectSingleNode("/*"));
                                    scriptModifier.addObject("xml", xmlWrapper);

                                    // transform
                                    scriptModifier.transform(xmlDoc);
                                }
                            break;

                        default:
                            throw new System.InvalidOperationException(modifierList[index].GetType().GetType().Name + " not a recognized modifier");
                    }
                }
                else
                {
                    // skipping this modifier only because skipNext = true
                    modifiersControl.skipNext = false;
                }

                // stop processing modifiers for this item if skipRemaining = true
                if (modifiersControl.skipRemaining)
                {
                    break;
                }
            }
            return modifiersControl.cancelSave;
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
            dirtyUI = true;
        }

        // method for script engine to access event log
        public void logEvent(string eventText, bool includeTime, string textColor, logOutputTarget outputTarget)
        {
            addLogEvent(eventText, includeTime, textColor, outputTarget);
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
                    DrawingControl.SuspendDrawing(richTextLog);

                    // if too long truncate the beginning of the log
                    if (richTextLog.Text.Length > maxLogBuffer)
                    {
                        richTextLog.SelectionStart = 0;
                        richTextLog.SelectionLength = fullEventText.Length - 1;
                        richTextLog.ReadOnly = false;
                        richTextLog.SelectedText = "";
                        richTextLog.ReadOnly = true;
                    }

                    // set color and append text
                    richTextLog.SelectionColor = System.Drawing.Color.FromName(textColor);
                    richTextLog.AppendText(fullEventText);

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

                        // renumber row headers

                        for (int i = 0; i < dataGridResults.Rows.Count; i++)
                        {
                            dataGridResults.Rows[i].HeaderCell.Value = (i + 1).ToString();
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

        private void loadScriptModifiers()
        {
            foreach (object modifier in modifierList)
            {
                if (modifier.GetType().Name == "ModifierScript")
                {
                    ModifierScript ScriptModifier = (ModifierScript)modifier;
                    ScriptModifier.setScriptEngine(new ScriptEngine());
                    ScriptModifier.createVsaEngine();
                    ScriptModifier.addObject("modifiers", modifiersControl);
                    ScriptModifier.addObject("logger", scriptLogger);
                    ScriptModifier.addObject("vars", scriptVariables);
                }
            }
        }

        // refresh modifier list view
        private void refreshModifierList()
        {
            lstViewModifiers.Items.Clear();
            string[] itemArray;
            ListViewItem lstViewItem;

            foreach(object modifier in modifierList)
            {
                switch (modifier.GetType().Name)
                {
                    case "ModifierUpdateText":
                        ModifierUpdateText updateTextModifier = (ModifierUpdateText)modifier;
                        itemArray = new string[4] { updateTextModifier.Enabled.ToString(), "Update Text", updateTextModifier.xpath, "'" + updateTextModifier.updateText + "'" };
                        lstViewItem = new ListViewItem(itemArray);
                        //set color to red if invalid
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
                        itemArray = new string[4] { renameNodeModifier.Enabled.ToString(), "Rename Node", renameNodeModifier.currentXpath, "-> " + renameNodeModifier.renamedNode };
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
                    case "ModifierCopyXML":
                        ModifierCopyXML copyXMLModifier = (ModifierCopyXML)modifier;
                        itemArray = new string[4] { copyXMLModifier.Enabled.ToString(), "Copy XML", copyXMLModifier.sourceNode, "-> " + copyXMLModifier.targetXpath };
                        lstViewItem = new ListViewItem(itemArray);
                        try
                        {
                            copyXMLModifier.validate();
                            lstViewItem.ForeColor = Color.Black;
                        }
                        catch
                        {
                            lstViewItem.ForeColor = Color.Red;
                        }
                        lstViewModifiers.Items.Add(lstViewItem);
                        break;
                    case "ModifierReplaceText":
                        ModifierReplaceText replaceTextModifier = (ModifierReplaceText)modifier;
                        itemArray = new string[4] { replaceTextModifier.Enabled.ToString(), "Replace Text", replaceTextModifier.xpath, "'" + replaceTextModifier.findText + "'" + " -> " + "'" + replaceTextModifier.replaceWithText + "'" };
                        lstViewItem = new ListViewItem(itemArray);
                        try
                        {
                            replaceTextModifier.validate();
                            lstViewItem.ForeColor = Color.Black;
                        }
                        catch
                        {
                            lstViewItem.ForeColor = Color.Red;
                        }
                        lstViewModifiers.Items.Add(lstViewItem);
                        break;
                    case "ModifierXSLT":
                        ModifierXSLT XSLTModifier = (ModifierXSLT)modifier;
                        itemArray = new string[4] { XSLTModifier.Enabled.ToString(), "XSLT", XSLTModifier.XSLTtext, "" };
                        lstViewItem = new ListViewItem(itemArray);
                        try
                        {
                            XSLTModifier.validate();
                            lstViewItem.ForeColor = Color.Black;
                        }
                        catch
                        {
                            lstViewItem.ForeColor = Color.Red;
                        }
                        lstViewModifiers.Items.Add(lstViewItem);
                        break;
                    case "ModifierScript":
                        ModifierScript ScriptModifier = (ModifierScript)modifier;

                        // get first non-blank line of script minus any "//"
                        string scriptFirstLine = ScriptModifier.ScriptText;
                        scriptFirstLine = scriptFirstLine.TrimStart(new char[] { ' ', '\n', '\r' });//.TrimStart('\n').TrimStart('\r');
                        if (scriptFirstLine.IndexOf('\n') != -1)
                        {
                            scriptFirstLine = scriptFirstLine.Substring(0, scriptFirstLine.IndexOf('\n'));
                        }
                        if (scriptFirstLine.StartsWith("//"))
                        {
                            scriptFirstLine = scriptFirstLine.Substring(2).Trim();
                        }

                        string runOnlyOnceString = "";
                        if (ScriptModifier.runOnlyOnce)
                        {
                            runOnlyOnceString = "Run only once";
                        }

                        itemArray = new string[4] { ScriptModifier.Enabled.ToString(), "Script", scriptFirstLine, runOnlyOnceString };
                        lstViewItem = new ListViewItem(itemArray);
                        if (ScriptModifier.valid)
                        {
                            lstViewItem.ForeColor = Color.Black;
                        }
                        else
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
                dirtyUI = true;
                refreshModifierList();
            }
            modifierUpdateTextForm.Close();
            this.BringToFront();

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
                dirtyUI = true;
                refreshModifierList();
            }
            modifierAddXMLForm.Close();
            this.BringToFront();
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
                dirtyUI = true;
                refreshModifierList();
            }
            modifierRenameNodeForm.Close();
            this.BringToFront();
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
                dirtyUI = true;
                refreshModifierList();
            }
            modifierRemoveNodeForm.Close();
            this.BringToFront();
        }

        // add Copy XML Modifier
        private void btnModifierCopyXml_Click(object sender, EventArgs e)
        {
            ModifierCopyXMLForm modifierCopyXMLForm = new ModifierCopyXMLForm();

            modifierCopyXMLForm.modifier = new ModifierCopyXML();

            modifierCopyXMLForm.ShowDialog(this);

            if (modifierCopyXMLForm.okPressed)
            {
                //add modifier to modifier list
                modifierList.Add(modifierCopyXMLForm.modifier);
                dirtyUI = true;
                refreshModifierList();
            }
            modifierCopyXMLForm.Close();
            this.BringToFront();
        }

        // add Replace Text Modifier
        private void btnModifierReplaceText_Click(object sender, EventArgs e)
        {
            ModifierReplaceTextForm modifierReplaceTextForm = new ModifierReplaceTextForm();

            modifierReplaceTextForm.modifier = new ModifierReplaceText();

            modifierReplaceTextForm.ShowDialog(this);

            if (modifierReplaceTextForm.okPressed)
            {
                //add modifier to modifier list
                modifierList.Add(modifierReplaceTextForm.modifier);
                dirtyUI = true;
                refreshModifierList();
            }
            modifierReplaceTextForm.Close();
            this.BringToFront();

        }

        // add XSLT Modifier
        private void btnModifierXSLT_Click(object sender, EventArgs e)
        {
            ModifierXSLTForm modifierXSLTForm = new ModifierXSLTForm();

            modifierXSLTForm.modifier = new ModifierXSLT();
            modifierXSLTForm.ShowDialog(this);

            if (modifierXSLTForm.okPressed)
            {
                //add modifier to modifier list
                modifierList.Add(modifierXSLTForm.modifier);
                dirtyUI = true;
                refreshModifierList();
            }
            modifierXSLTForm.Close();
            this.BringToFront();
        }

        // add Script Modifier
        private void btnModifierScript_Click(object sender, EventArgs e)
        {
            ModifierScriptForm modifierScriptForm = new ModifierScriptForm();

            ModifierScript newScriptModifier = new ModifierScript();
            newScriptModifier.setScriptEngine(new ScriptEngine());
            newScriptModifier.createVsaEngine();
            newScriptModifier.addObject("modifiers", modifiersControl);
            newScriptModifier.addObject("logger", scriptLogger);
            newScriptModifier.addObject("vars", scriptVariables);

            modifierScriptForm.modifier = newScriptModifier;
            modifierScriptForm.ShowDialog(this);

            if (modifierScriptForm.okPressed)
            {
                //add modifier to modifier list
                modifierList.Add(modifierScriptForm.modifier);
                dirtyUI = true;
                refreshModifierList();
            }

            modifierScriptForm.Close();
            this.BringToFront();
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
                            dirtyUI = true;
                            refreshModifierList();
                        }
                        modifierUpdateTextForm.Close();
                        this.BringToFront();

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
                            dirtyUI = true;
                            refreshModifierList();
                        }
                        modifierAddXMLForm.Close();
                        this.BringToFront();

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
                            dirtyUI = true;
                            refreshModifierList();
                        }
                        modifierRenameNodeForm.Close();
                        this.BringToFront();

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
                            dirtyUI = true;
                            refreshModifierList();
                        }
                        modifierRemoveNodeForm.Close();
                        this.BringToFront();

                        break;
                    case "ModifierCopyXML":
                        ModifierCopyXML copyXMLModifier = (ModifierCopyXML)modifierList[index];
                        ModifierCopyXMLForm copyXMLModifierForm = new ModifierCopyXMLForm();

                        copyXMLModifierForm.modifier = copyXMLModifier;
                        copyXMLModifierForm.ShowDialog(this);

                        if (copyXMLModifierForm.okPressed)
                        {
                            //update modifier in modifier list
                            modifierList[index] = copyXMLModifierForm.modifier;
                            dirtyUI = true;
                            refreshModifierList();
                        }
                        copyXMLModifierForm.Close();
                        this.BringToFront();

                        break;
                    case "ModifierReplaceText":
                        ModifierReplaceText replaceTextModifier = (ModifierReplaceText)modifierList[index];
                        ModifierReplaceTextForm replaceTextModifierForm = new ModifierReplaceTextForm();

                        replaceTextModifierForm.modifier = replaceTextModifier;
                        replaceTextModifierForm.ShowDialog(this);

                        if (replaceTextModifierForm.okPressed)
                        {
                            //update modifier in modifier list
                            modifierList[index] = replaceTextModifierForm.modifier;
                            dirtyUI = true;
                            refreshModifierList();
                        }
                        replaceTextModifierForm.Close();
                        this.BringToFront();

                        break;
                    case "ModifierXSLT":
                        ModifierXSLT XSLTModifier = (ModifierXSLT)modifierList[index];

                        ModifierXSLTForm XSLTModifierForm = new ModifierXSLTForm();

                        XSLTModifierForm.modifier = XSLTModifier;
                        XSLTModifierForm.ShowDialog(this);

                        if (XSLTModifierForm.okPressed)
                        {
                            //update modifier in modifier list
                            modifierList[index] = XSLTModifierForm.modifier;
                            dirtyUI = true;
                            refreshModifierList();
                        }
                        XSLTModifierForm.Close();
                        this.BringToFront();

                        break;
                    case "ModifierScript":
                        ModifierScript ScriptModifier = (ModifierScript)modifierList[index];
                        ModifierScriptForm ScriptModifierForm = new ModifierScriptForm();

                        ScriptModifierForm.modifier = ScriptModifier;
                        ScriptModifierForm.ShowDialog(this);

                        if (ScriptModifierForm.okPressed)
                        {
                            //update modifier in modifier list
                            dirtyUI = true;
                            refreshModifierList();
                        }
                        ScriptModifierForm.Close();
                        this.BringToFront();

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
                    dirtyUI = true;
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
                        dirtyUI = true;
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
                        dirtyUI = true;
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
                    MessageBox.Show("Export complete.", "Export Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                stream.Write("Selected,");
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
                for (int i = 0; i < dataGridResults.Columns.Count; i++)
                {
                    if (!selectionOnly || row.Cells[0].Value != null)
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

        private void btnBrowseCSV_Click(object sender, EventArgs e)
        {

            OpenFileDialog openProfileFileDialog = new OpenFileDialog();
            openProfileFileDialog.Title = "Load CSV";
            if (currentProfileFile != "")
            {
                openProfileFileDialog.InitialDirectory = Path.GetDirectoryName(currentProfileFile);
            }
            openProfileFileDialog.Filter = "CSV|*.csv|All files|*.*";
            if (openProfileFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtCSVPath.Text = openProfileFileDialog.FileName;
                ImportCSV(openProfileFileDialog.FileName, true);
            }
        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            string fileName = txtCSVPath.Text;
            if (currentProfileFile != "")
            {
                fileName = Path.Combine(Path.GetDirectoryName(currentProfileFile), txtCSVPath.Text);
            }
            ImportCSV(fileName, true);
        }

        // import first four column of a CSV into the first four columns of the datagrid
        private void ImportCSV(string filename, bool MessageError)
        {
            bool dataGridStartVisibility = dataGridResults.Visible;
            try
            {
                tabMain.SelectTab(2);
                this.Cursor = Cursors.WaitCursor;
                stopProcessing = false;

                // read CSV for count
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                CsvReader csv = new CsvReader(new StreamReader(fs), true);
                int csvCount = csv.Count();

                // read CSV
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                csv = new CsvReader(new StreamReader(fs), true);

                string[] headers = csv.GetFieldHeaders();

                // validate headers
                if (headers.Count() < 4)
                    throw new System.InvalidOperationException("At least 4 columns required, only " + headers.Count() + " found");
                else if (headers[0].ToUpper().Trim() != "SELECTED")
                    throw new System.InvalidOperationException("Invalid column '" + headers[0] + "' expecting 'Selected'");
                else if (headers[1].ToUpper().Trim() != "ITEM NAME")
                    throw new System.InvalidOperationException("Invalid column '" + headers[1] + "' expecting 'Item Name'");
                else if (headers[2].ToUpper().Trim() != "ITEM ID")
                    throw new System.InvalidOperationException("Invalid column '" + headers[2] + "' expecting 'Item ID'");
                else if (headers[3].ToUpper().Trim() != "VER")
                    throw new System.InvalidOperationException("Invalid column '" + headers[3] + "' expecting 'Ver'");

                stopToolStripButton.Enabled = true;
                dataGridResults.Visible = false;
                tabMain.Enabled = false;
                lblSearchPanelBackground.Text = "Loading CSV...";
                Application.DoEvents();

                // clear results from last query
                dataGridResults.Rows.Clear();

                toolStripProgressBar.Visible = true;
                toolStripStatusResultsCount.Text = "0 results";
                toolStripSelectionCount.Text = "0 selected";
                toolStripStatusProgressText.Text = "Retrieving 0" + "/" + csvCount;
                toolStripStatusProgressText.Visible = true;
                statusStrip1.Refresh();
                tabMain.Refresh();

                toolStripProgressBar.Step = 1;
                toolStripProgressBar.Maximum = csvCount/100;
                // TODO: reset CSV cursor
                long ip = csv.CurrentRecordIndex;

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
                {
                    dataGridResults.Columns[3].Visible = true;
                    dataGridResults.Columns[3].Width = 40;
                }
                else
                    dataGridResults.Columns[3].Visible = false;

                // get previous column widths
                Hashtable lastColumnWidths = new Hashtable();
                for (int i = 0; i < resultsXpaths.Count; i++)
                {
                    lastColumnWidths[resultsXpaths[i]] = dataGridResults.Columns[i + 1].Width;
                }

                // populate system xpaths
                resultsXpaths.Clear();
                resultsXpathsSelected.Clear();
                resultsXpaths.Add("/xml/item/name");
                resultsXpaths.Add("/xml/item/@id");
                resultsXpaths.Add("/xml/item/@version");

                dataGridResults.ColumnCount = systemColumns;

                int rowCount = 0;
                int remainder; 

                // iterate through CSV rows
                while (csv.ReadNextRecord())
                {
                    rowCount += 1;

                    // add row to grid
                    dataGridResults.Rows.Add();
                    dataGridResults.Rows[rowCount - 1].HeaderCell.Value = rowCount.ToString();
                    if (csv[0].ToUpper() == "TRUE")
                    {
                        dataGridResults.Rows[rowCount - 1].Cells[0].Value = true;
                    }
                    dataGridResults.Rows[rowCount - 1].Cells[1].Value = csv[1];
                    dataGridResults.Rows[rowCount - 1].Cells[2].Value = csv[2];
                    dataGridResults.Rows[rowCount - 1].Cells[3].Value = csv[3];

                    // update counts
                    Math.DivRem(rowCount, 100, out remainder);
                    if (remainder == 0)
                    {
                        toolStripProgressBar.Value = rowCount/100;
                        toolStripStatusResultsCount.Text = dataGridResults.Rows.Count.ToString() + " results";
                        toolStripSelectionCount.Text = Convert.ToString(countCheckedResultsRows()) + " selected";
                        toolStripStatusProgressText.Text = "Retrieving " + Convert.ToString(rowCount) + "/" + csvCount;
                        Application.DoEvents();
                        if (stopProcessing) break;
                    }
                }
                csv.Dispose();

                // finalize UI
                stopProcessing = false;
                toolStripProgressBar.Value = toolStripProgressBar.Maximum;
                toolStripStatusResultsCount.Text = dataGridResults.Rows.Count.ToString() + " results";
                toolStripSelectionCount.Text = Convert.ToString(countCheckedResultsRows()) + " selected";
                Application.DoEvents();
                toolStripProgressBar.Visible = false;
                toolStripStatusProgressText.Visible = false;
                toolStripProgressBar.Value = 0;
                dataGridResults.Columns[0].Width = 30;
                this.Cursor = Cursors.Arrow;
                tabMain.Enabled = true;
                dataGridResults.Visible = true;
                lblSearchPanelBackground.Text = "No results";

                clearToolStripButton.Text = "Clear unselected results";
                clearToolStripButton.Enabled = true;

                // workaround for scrollbar bug in data grid
                dataGridResults.Width += 1;
                dataGridResults.Width -= 1;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Arrow;
                tabMain.Enabled = true;
                dataGridResults.Visible = dataGridStartVisibility;
                toolStripProgressBar.Value = 0;
                toolStripStatusProgressText.Visible = false;
                lblSearchPanelBackground.Text = "No results";
                stopProcessing = false;
                if (MessageError)
                {
                    MessageBox.Show("An error occurred while trying to load the CSV.\n\n" + err.Message, "CSV Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // save profile
        private void saveProfile(string profileLocation)
        {
            string stsResultsText = toolStripStatusResultsCount.Text;
            string stsSelectionText = toolStripSelectionCount.Text;
            try
            {
                toolStripStatusResultsCount.Text = "Saving...";
                toolStripSelectionCount.Text = "";
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
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
                profile.useProxy = chkUseProxy.Checked;
                profile.proxyAddress = txtProxyAddress.Text;
                profile.proxyUsername = txtProxyUsername.Text;
                profile.proxyPassword = txtProxyPassword.Text;

                // query tab
                profile.whereStatement = txtWhereStatement.Text;
                profile.freetextQuery = txtFreeTextQuery.Text;
                profile.itemUuid = txtItemUUID.Text;
                profile.itemVersion = txtItemVersion.Text;
                profile.allCollections = rdioAllCollections.Checked;
                for (int i = 0; i < lstCollections.Items.Count; i++)
                {
                    profile.collectionsList.list.Add(lstCollections.Items[i]);
                }
                profile.columnsIncludeName = chkIncludeColumnsName.Checked;
                profile.columnsIncludeItemID = chkIncludeColumnsID.Checked;
                profile.columnsIncludeItemVersion = chkIncludeColumnsVersion.Checked;

                foreach (DataGridViewRow row in dataGridColumns.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        profile.columnsList.list.Add(row.Cells[0].Value);
                        profile.selectedColumnsList.list.Add(Convert.ToBoolean(row.Cells[1].Value));
                    }
                }

                profile.delimiter = txtDelimiter.Text;
                if (cmbSortOrder.SelectedItem != null)
                {
                    profile.sortOrder = cmbSortOrder.SelectedItem.ToString();
                }
                else
                {
                    profile.sortOrder = "0";
                }
                profile.reverseSort = chkReverseOrder.Checked;
                profile.includeNonLive = chkIncludeNonLive.Checked;
                profile.limit = txtLimit.Text;

                // results tab
                profile.csvFilename = txtCSVPath.Text;
                profile.loadCsvWithProfile = chkLoadCsvWithProfile.Checked;

                // update tab
                profile.modifierList.list = modifierList;
                profile.updateAllResults = rdioUpdateAllResults.Checked;
                profile.updateFirstFewOnly = chkOnlyUpdateFirstFew.Checked;
                profile.updateFirstFewNumber = Convert.ToInt16(txtFirstFewToUpdate.Text);
                profile.uncheckProcessedResults = chkUnselectProcessedResults.Checked;
                profile.logOutputSize = cmbLogOutput.SelectedIndex;
                profile.createLogFile = chkCreateLogFiles.Checked;
                profile.clearLogOnUpdate = chkClearLogOnUpdate.Checked;
                profile.copyItemFilesToStaging = chkCopyToStaging.Checked;

                XmlSerializer s = new XmlSerializer(typeof(Profile));
                TextWriter w = new StreamWriter(profileLocation);
                s.Serialize(w, profile);
                w.Close();
                currentProfileFile = profileLocation;
                dirtyUI = false;
                System.Threading.Thread.Sleep(250);
                toolStripStatusResultsCount.Text = stsResultsText;
                toolStripSelectionCount.Text = stsSelectionText;
                this.Cursor = Cursors.Arrow;

            }
            catch (Exception err)
            {
                toolStripStatusResultsCount.Text = stsResultsText;
                toolStripSelectionCount.Text = stsSelectionText;
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
                chkUseProxy.Checked = profile.useProxy;
                txtProxyAddress.Text = profile.proxyAddress;
                txtProxyUsername.Text = profile.proxyUsername;
                txtProxyPassword.Text = profile.proxyPassword;

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
                txtItemUUID.Text = profile.itemUuid;
                txtItemVersion.Text = profile.itemVersion;
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
                dataGridColumns.Rows.Clear();
                for (int i = 0; i < profile.columnsList.list.Count; i++)
                {
                    dataGridColumns.Rows.Add(profile.columnsList.list[i]);
                }
                for (int i = 0; i < profile.selectedColumnsList.list.Count; i++)
                {
                    dataGridColumns.Rows[i].Cells[1].Value = Convert.ToBoolean(profile.selectedColumnsList.list[i]);
                }


                txtDelimiter.Text = profile.delimiter;
                cmbSortOrder.SelectedItem = profile.sortOrder;
                chkReverseOrder.Checked = profile.reverseSort;
                chkIncludeNonLive.Checked = profile.includeNonLive;
                txtLimit.Text = profile.limit;

                // results tab
                txtCSVPath.Text = profile.csvFilename;
                chkLoadCsvWithProfile.Checked = profile.loadCsvWithProfile;

                // update tab
                if (profile.modifierList.list != null) modifierList = profile.modifierList.list;
                loadScriptModifiers();
                refreshModifierList();
                rdioUpdateAllResults.Checked = profile.updateAllResults;
                rdioSelectedResults.Checked = !rdioUpdateAllResults.Checked;
                chkOnlyUpdateFirstFew.Checked = profile.updateFirstFewOnly;
                txtFirstFewToUpdate.Text = profile.updateFirstFewNumber.ToString();
                chkUnselectProcessedResults.Checked = profile.uncheckProcessedResults; 
                cmbLogOutput.SelectedIndex = profile.logOutputSize;
                chkCreateLogFiles.Checked = profile.createLogFile;
                chkClearLogOnUpdate.Checked = profile.clearLogOnUpdate;
                chkCopyToStaging.Checked = profile.copyItemFilesToStaging;

                dirtyUI = false;

                this.Cursor = Cursors.Arrow;

                // load CSV if set
                if (mainFormShown && chkLoadCsvWithProfile.Checked)
                {
                    ImportCSV(Path.Combine(Path.GetDirectoryName(currentProfileFile), txtCSVPath.Text), true);
                }
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
            dataGridColumns.CommitEdit(DataGridViewDataErrorContexts.Commit);
            launchSaveDialog();
        }

        private bool launchSaveDialog()
        {
            bool profileSaved = false;
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
                profileSaved = true;
            }
            return profileSaved;
        }

        // load profile
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            bool cancelOpen = false;

            // check if dirty and ask if save
            if (dirtyUI)
            {
                DialogResult dialogResult = MessageBox.Show(this, "Do you want to save your current profile before opening another?", "EQUELLA Metadata Utility", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (dialogResult == DialogResult.Cancel)
                {
                    cancelOpen = true;
                }
                if (dialogResult == DialogResult.Yes)
                {
                    if (!launchSaveDialog())
                    {
                        cancelOpen = true;
                    }
                }
            }

            if (!cancelOpen)
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
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            bool cancelNew = false;

            // check if dirty and ask if save
            if (dirtyUI)
            {
                DialogResult dialogResult = MessageBox.Show(this, "Do you want to save your profile before clearing all settings?", "EQUELLA Metadata Utility", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (dialogResult == DialogResult.Cancel)
                {
                    cancelNew = true;
                }
                if (dialogResult == DialogResult.Yes)
                {
                    if (!launchSaveDialog())
                    {
                        cancelNew = true;
                    }
                }
            }

            if (!cancelNew)
            {
                try
                {
                    // connection tab
                    txtInstitutionUrl.Text = "";
                    rdioSharedSecret.Checked = false;
                    rdioEqUser.Checked = !rdioSharedSecret.Checked;
                    txtUsername.Text = "";
                    chkUseProxy.Checked = false;
                    txtProxyAddress.Text = "";
                    txtProxyUsername.Text = "";
                    txtProxyPassword.Text = "";
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
                    txtItemUUID.Text = "";
                    txtItemVersion.Text = "";
                    rdioAllCollections.Checked = true;
                    rdioSpecificCollections.Checked = !rdioAllCollections.Checked;
                    chkIncludeColumnsName.Checked = true;
                    chkIncludeColumnsID.Checked = true;
                    chkIncludeColumnsVersion.Checked = true;
                    dataGridColumns.Rows.Clear();
                    txtDelimiter.Text = "|";
                    cmbSortOrder.SelectedItem = "Rank";
                    chkReverseOrder.Checked = false;
                    chkIncludeNonLive.Checked = false;
                    txtLimit.Text = "";

                    // update tab
                    modifierList.Clear();
                    refreshModifierList();
                    rdioUpdateAllResults.Checked = false;
                    rdioSelectedResults.Checked = !rdioUpdateAllResults.Checked;
                    chkOnlyUpdateFirstFew.Checked = false;
                    txtFirstFewToUpdate.Text = "";
                    cmbLogOutput.SelectedIndex = 0;
                    chkCreateLogFiles.Checked = false;
                    chkClearLogOnUpdate.Checked = false;
                    chkCopyToStaging.Checked = false;

                    dirtyUI = false;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Clear Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        // export Modifiers
        private void btnExportModifiers_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveProfileFileDialog = new SaveFileDialog();
            saveProfileFileDialog.Title = "Export Modifiers";
            saveProfileFileDialog.OverwritePrompt = true;
            try
            {
                if (currentProfileFile != "")
                {
                    saveProfileFileDialog.InitialDirectory = Path.GetDirectoryName(currentProfileFile);
                    //saveProfileFileDialog.FileName = Path.GetFileName(currentProfileFile);
                }
            }
            finally {}
            saveProfileFileDialog.Filter = "EMU modifiers|*.xml";
            if (saveProfileFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    ModifiersExport modifiersExport = new ModifiersExport();
                    modifiersExport.modifierList.list = modifierList;
                    XmlSerializer s = new XmlSerializer(typeof(ModifiersExport));
                    TextWriter w = new StreamWriter(saveProfileFileDialog.FileName);
                    s.Serialize(w, modifiersExport);
                    w.Close();
                    currentProfileFile = saveProfileFileDialog.FileName;
                    this.Cursor = Cursors.Arrow;

                }
                catch (Exception err)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(err.Message, "Modifers Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // import modifiers
        private void btnImportModifiers_Click(object sender, EventArgs e)
        {
            OpenFileDialog openProfileFileDialog = new OpenFileDialog();
            openProfileFileDialog.Title = "Import Modifiers";
            if (currentProfileFile != "")
            {
                openProfileFileDialog.InitialDirectory = Path.GetDirectoryName(currentProfileFile);
            }
            openProfileFileDialog.Filter = "EMU modifiers|*.xml|All files|*.*";
            if (openProfileFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    XmlSerializer s = new XmlSerializer(typeof(ModifiersExport));
                    TextReader r = new StreamReader(openProfileFileDialog.FileName);
                    ModifiersExport modifiersExport;
                    modifiersExport = (ModifiersExport)s.Deserialize(r);
                    r.Close();
                    modifierList.AddRange(modifiersExport.modifierList.list);
                    loadScriptModifiers();
                    refreshModifierList();

                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception err)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show("Modifier import error:\n" + err.Message + "\n\nModifiers file may be corrupt.", "Modifiers Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
             if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.S) 
             {
                 if (currentProfileFile == "")
                 {
                     launchSaveDialog();
                 }
                 else
                 {
                     saveProfile(currentProfileFile);
                 }
             }

             if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.E)
             {
                 if (tabMain.SelectedTab == tabQuery)
                 {
                     Application.DoEvents();
                     cmdRunQuery_Click(sender, e);
                 }
                 else if (tabMain.SelectedTab == tabQueryResults)
                 {
                     tabMain.SelectedTab = tabQuery;
                 }
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
            String uneditedCellValue = "";

            try
            {

                string delimiter = txtDelimiter.Text.Replace("\\n", "\r\n");
                delimiter = delimiter.Replace("\\t", "\t");

                // get item ID and version from data grid
                string itemID = dataGridResults.Rows[dataGridResults.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                int itemVersion = Convert.ToInt16(dataGridResults.Rows[dataGridResults.SelectedCells[0].RowIndex].Cells[3].Value);


                // check if double clicked on non-header cell
                if (dataGridResults.SelectedCells[0].ColumnIndex > 0)
                {
                    uneditedCellValue = dataGridResults.SelectedCells[0].Value.ToString();

                    string xpath = resultsXpaths[dataGridResults.SelectedCells[0].ColumnIndex - 1].ToString();


                    // open form and load note value into it
                    ViewNodeXMLForm viewNodeXMLForm = new ViewNodeXMLForm();

                    viewNodeXMLForm.nodeText = uneditedCellValue;
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
                    if (!xpathInNonEditableNodes)
                    {
                        viewNodeXMLForm.editable = true;
                    }

                    // launch form
                    viewNodeXMLForm.ShowDialog(this);
                    this.BringToFront();
                    this.Cursor = Cursors.WaitCursor;

                    if (viewNodeXMLForm.nodeEdited)
                    {
                        dataGridResults.SelectedCells[0].Value = "Updating...";
                        Application.DoEvents();

                        // split string if not an attribute otherwise only need an array of one element
                        string[] values;
                        if (!viewNodeXMLForm.xpath.Contains('@'))
                        {
                            values = viewNodeXMLForm.newNodeText.Split(new String[] { delimiter }, StringSplitOptions.None);
                        }
                        else
                        {
                            values = new String[] { viewNodeXMLForm.newNodeText };
                        }

                        login();

                        // get item for editing
                        equellaClient.unlock(itemID, itemVersion);
                        string oldItemXMLstring = equellaClient.editItem(itemID, itemVersion, false);
                        string newItemXMLstring = oldItemXMLstring;
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(oldItemXMLstring);

                        XmlDocument newXmlDoc = new XmlDocument();
                        XmlElement rootElement;
                        XmlNodeList nodeList;

                        ModifierUpdateText modifierUpdateText = new ModifierUpdateText();
                        ModifierAddXML modifierAddXML = new ModifierAddXML();
                        Utils utils = new Utils();

                        if (values.Length > 0)
                        {
                            // create/update first value be it attribute or element
                            modifierUpdateText.xpath = xpath;
                            modifierUpdateText.createNode = true;
                            modifierUpdateText.updateText = values[0];
                            newItemXMLstring = modifierUpdateText.transform(oldItemXMLstring);
                            newXmlDoc.LoadXml(newItemXMLstring);

                            rootElement = (XmlElement)newXmlDoc.GetElementsByTagName("xml")[0];
                            nodeList = rootElement.SelectNodes(xpath);

                            // if more than one value and xpath is not an attribute
                            if (values.Length > 1 && !xpath.Contains("/@"))
                            {
                                    
                                // update the existing nodes
                                for (int i = 1; i < nodeList.Count; i++)
                                {
                                    if(i < values.Length)
                                    {
                                        XmlNode matchingElementTextNode = utils.GetTextNode((XmlElement)nodeList[i]);
                                        matchingElementTextNode.Value = values[i];
                                    }

                                }

                                // update/create remaining values
                                string lastNode = xpath.Substring(xpath.LastIndexOf("/") + 1);
                                XmlNode parentNode = nodeList[0].ParentNode;
                                for (int i = 1; i < values.Length; i++)
                                {
                                    if (i < nodeList.Count)
                                    {
                                        // update existing element
                                        utils.GetTextNode((XmlElement)nodeList[i]).Value = values[i];
                                    }
                                    else
                                    {
                                        // create new node
                                        XmlElement newElement = newXmlDoc.CreateElement(lastNode);
                                        newElement.AppendChild(newXmlDoc.CreateTextNode(values[i]));
                                        parentNode.AppendChild(newElement);
                                    }
                                }
                            }

                            // clear remaining nodes
                            for (int i = values.Length; i < nodeList.Count; i++)
                            {
                                if (xpath.Contains("/@"))
                                {
                                    nodeList[i].Value = "";
                                }
                                else
                                {
                                    XmlNode textNode = utils.GetTextNode((XmlElement)nodeList[i]);
                                    if (textNode != null) textNode.Value = "";
                                }
                            }

                            // get XML string
                            newItemXMLstring = newXmlDoc.InnerXml;

                        }

                        // save item
                        equellaClient.saveItem(newItemXMLstring, false);

                        // reload grid row
                        string refreshedXMLString = equellaClient.getItem(itemID, itemVersion, "");
                        xmlDoc.LoadXml(refreshedXMLString);
                        rowCells = new object[dataGridResults.ColumnCount];
                        populateDataGridResultsRow(dataGridResults.SelectedCells[0].RowIndex, xmlDoc.ChildNodes[0], delimiter);
                        toolStripSelectionCount.Text = Convert.ToString(countCheckedResultsRows()) + " selected";


                        equellaClient.logout();
                    }

                    viewNodeXMLForm.Close();

                }
                else
                {
                    // check if double clicked on row header cell
                    if (dataGridResults.SelectedCells[0].ColumnIndex == 0 && dataGridResults.SelectedCells.Count > 1)
                    {
                        //// open View Item XML Form and load equellaClient and item parameters into it
                        ViewItemXMLForm viewItemXMLForm = new ViewItemXMLForm();
                        viewItemXMLForm.mainForm = this;
                        viewItemXMLForm.copyToStaging = chkCopyToStaging.Checked;
                        login();
                        viewItemXMLForm.equellaClient = equellaClient;
                        viewItemXMLForm.itemID = itemID;
                        viewItemXMLForm.itemVersion = itemVersion;
                        viewItemXMLForm.Show();
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception err)
            {
                if (dataGridResults.SelectedCells[0].ColumnIndex > 0)
                {
                    dataGridResults.SelectedCells[0].Value = uneditedCellValue;
                }
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(err.Message, "View/Edit Node Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void chkUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseProxy.Checked)
            {
                txtProxyAddress.Enabled = true;
                txtProxyUsername.Enabled = true;
                txtProxyPassword.Enabled = true;
            }
            else
            {
                txtProxyAddress.Enabled = false;
                txtProxyUsername.Enabled = false;
                txtProxyPassword.Enabled = false;
            }
            dirtyUI = true;
        }

        // intercept close event
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // check if dirty and ask if save
            if (dirtyUI)
            {
                DialogResult dialogResult = MessageBox.Show(this, "Do you want to save your profile before closing?", "EQUELLA Metadata Utility", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                if (dialogResult == DialogResult.Yes)
                {
                    if (!launchSaveDialog())
                    {
                        e.Cancel = true;
                    }
                }

            }
        }
        
        private void txtInstitutionUrl_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;

            // Set application bar caption to include institution url
            if (txtInstitutionUrl.Text.StartsWith("http://"))
            {
                this.Text = txtInstitutionUrl.Text.Substring(7) + " - EMU";
            }
            else if (txtInstitutionUrl.Text.StartsWith("https://"))
            {
                this.Text = txtInstitutionUrl.Text.Substring(8) + " - EMU";
            }
            else
            {
                this.Text = "No Institution - EMU";
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtSSUsername_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtSharedSecretID_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtSharedSecret_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtLogFileLocation_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtProxyAddress_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtProxyUsername_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtProxyPassword_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtFreeTextQuery_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtWhereStatement_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void chkReverseOrder_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void chkIncludeNonLive_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void chkIncludeColumnsName_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void chkIncludeColumnsID_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void chkIncludeColumnsVersion_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtDelimiter_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void rdioSelectedResults_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtFirstFewToUpdate_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void cmbLogOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void chkCreateLogFiles_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }


        private void dataGridColumns_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dirtyUI = true;

        }

        private void chkClearLogOnUpdate_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void chkCopyToStaging_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtItemUUID_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtItemVersion_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtLimit_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void chkUnselectProcessedResults_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void txtCSVPath_TextChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void chkLoadCsvWithProfile_CheckedChanged(object sender, EventArgs e)
        {
            dirtyUI = true;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            mainFormShown = true;
            // load CSV if set
            if (chkLoadCsvWithProfile.Checked)
            {
                Application.DoEvents();
                ImportCSV(Path.Combine(Path.GetDirectoryName(currentProfileFile), txtCSVPath.Text), true);
            }
        }
    }
    //DrawingControl for controlling a Windows control's redraw behavior
    public class DrawingControl
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETREDRAW = 0x000B;

        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, (IntPtr) 0, IntPtr.Zero);
        }

        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, (IntPtr) 1, IntPtr.Zero);
            parent.Refresh();
        }
    } 
}
