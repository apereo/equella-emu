namespace EquellaMetadataUtility
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabEQSettings = new System.Windows.Forms.TabPage();
            this.chkUseProxy = new System.Windows.Forms.CheckBox();
            this.txtLogFileLocation = new System.Windows.Forms.TextBox();
            this.txtProxyAddress = new System.Windows.Forms.TextBox();
            this.btnSetLogFileLocation = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.rdioSharedSecret = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.rdioEqUser = new System.Windows.Forms.RadioButton();
            this.txtProxyUsername = new System.Windows.Forms.TextBox();
            this.txtProxyPassword = new System.Windows.Forms.TextBox();
            this.grpSharedSecret = new System.Windows.Forms.GroupBox();
            this.txtSharedSecretID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSharedSecret = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSSUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpEQUser = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInstitutionUrl = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabQuery = new System.Windows.Forms.TabPage();
            this.lblLimit = new System.Windows.Forms.Label();
            this.txtLimit = new System.Windows.Forms.TextBox();
            this.txtItemVersion = new System.Windows.Forms.TextBox();
            this.txtItemUUID = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dataGridColumns = new System.Windows.Forms.DataGridView();
            this.xpathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtDelimiter = new System.Windows.Forms.TextBox();
            this.lblDelimiter = new System.Windows.Forms.Label();
            this.chkIncludeColumnsVersion = new System.Windows.Forms.CheckBox();
            this.chkIncludeColumnsID = new System.Windows.Forms.CheckBox();
            this.chkIncludeColumnsName = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.grpCollections = new System.Windows.Forms.GroupBox();
            this.rdioSpecificCollections = new System.Windows.Forms.RadioButton();
            this.rdioAllCollections = new System.Windows.Forms.RadioButton();
            this.lstCollections = new System.Windows.Forms.ListBox();
            this.btnRemoveCollection = new System.Windows.Forms.Button();
            this.btnAddCollection = new System.Windows.Forms.Button();
            this.cmbSortOrder = new System.Windows.Forms.ComboBox();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.lblSortOrder = new System.Windows.Forms.Label();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.chkReverseOrder = new System.Windows.Forms.CheckBox();
            this.cmdRunQuery = new System.Windows.Forms.Button();
            this.chkIncludeNonLive = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFreeTextQuery = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWhereStatement = new System.Windows.Forms.TextBox();
            this.tabQueryResults = new System.Windows.Forms.TabPage();
            this.btnBrowseCSV = new System.Windows.Forms.Button();
            this.chkLoadCsvWithProfile = new System.Windows.Forms.CheckBox();
            this.txtCSVPath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnLoadCSV = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnInvertSelection = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnExportSelection = new System.Windows.Forms.Button();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.panelResultsBackground = new System.Windows.Forms.Panel();
            this.dataGridResults = new System.Windows.Forms.DataGridView();
            this.Selection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSearchPanelBackground = new System.Windows.Forms.Label();
            this.tabBulkUpdate = new System.Windows.Forms.TabPage();
            this.btnModifierScript = new System.Windows.Forms.Button();
            this.btnModifierXSLT = new System.Windows.Forms.Button();
            this.btnExportModifiers = new System.Windows.Forms.Button();
            this.btnImportModifiers = new System.Windows.Forms.Button();
            this.btnModifierReplaceText = new System.Windows.Forms.Button();
            this.btnModifierCopyXml = new System.Windows.Forms.Button();
            this.lstViewModifiers = new System.Windows.Forms.ListView();
            this.enabled = new System.Windows.Forms.ColumnHeader();
            this.type = new System.Windows.Forms.ColumnHeader();
            this.xpath = new System.Windows.Forms.ColumnHeader();
            this.property = new System.Windows.Forms.ColumnHeader();
            this.label9 = new System.Windows.Forms.Label();
            this.btnModifierRenameNode = new System.Windows.Forms.Button();
            this.btnEditModifier = new System.Windows.Forms.Button();
            this.grpboxExecution = new System.Windows.Forms.GroupBox();
            this.chkUnselectProcessedResults = new System.Windows.Forms.CheckBox();
            this.chkCopyToStaging = new System.Windows.Forms.CheckBox();
            this.chkClearLogOnUpdate = new System.Windows.Forms.CheckBox();
            this.chkCreateLogFiles = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbLogOutput = new System.Windows.Forms.ComboBox();
            this.txtFirstFewToUpdate = new System.Windows.Forms.TextBox();
            this.chkOnlyUpdateFirstFew = new System.Windows.Forms.CheckBox();
            this.rdioUpdateAllResults = new System.Windows.Forms.RadioButton();
            this.rdioSelectedResults = new System.Windows.Forms.RadioButton();
            this.btnTestBulkUpdate = new System.Windows.Forms.Button();
            this.btnRunBulkUpdate = new System.Windows.Forms.Button();
            this.btnModifierUpdateText = new System.Windows.Forms.Button();
            this.btnModifierRemoveXml = new System.Windows.Forms.Button();
            this.btnModifierAddXml = new System.Windows.Forms.Button();
            this.btnMoveModifierDown = new System.Windows.Forms.Button();
            this.btnMoveModifierUp = new System.Windows.Forms.Button();
            this.btnRemoveModifier = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.richTextLog = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusResultsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSelectionCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusProgressText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMainMenu = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.clearToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.folderBrowserLogFileLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.modifierToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabMain.SuspendLayout();
            this.tabEQSettings.SuspendLayout();
            this.grpSharedSecret.SuspendLayout();
            this.grpEQUser.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridColumns)).BeginInit();
            this.grpCollections.SuspendLayout();
            this.tabQueryResults.SuspendLayout();
            this.panelResultsBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResults)).BeginInit();
            this.tabBulkUpdate.SuspendLayout();
            this.grpboxExecution.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(0, 100);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(94, 72);
            this.btnTestConnection.TabIndex = 5;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabEQSettings);
            this.tabMain.Controls.Add(this.tabQuery);
            this.tabMain.Controls.Add(this.tabQueryResults);
            this.tabMain.Controls.Add(this.tabBulkUpdate);
            this.tabMain.Controls.Add(this.tabLog);
            this.tabMain.Location = new System.Drawing.Point(0, 28);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(897, 455);
            this.tabMain.TabIndex = 1;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabEQSettings
            // 
            this.tabEQSettings.Controls.Add(this.chkUseProxy);
            this.tabEQSettings.Controls.Add(this.txtLogFileLocation);
            this.tabEQSettings.Controls.Add(this.txtProxyAddress);
            this.tabEQSettings.Controls.Add(this.btnSetLogFileLocation);
            this.tabEQSettings.Controls.Add(this.label17);
            this.tabEQSettings.Controls.Add(this.label15);
            this.tabEQSettings.Controls.Add(this.label19);
            this.tabEQSettings.Controls.Add(this.rdioSharedSecret);
            this.tabEQSettings.Controls.Add(this.label18);
            this.tabEQSettings.Controls.Add(this.rdioEqUser);
            this.tabEQSettings.Controls.Add(this.txtProxyUsername);
            this.tabEQSettings.Controls.Add(this.txtProxyPassword);
            this.tabEQSettings.Controls.Add(this.grpSharedSecret);
            this.tabEQSettings.Controls.Add(this.label3);
            this.tabEQSettings.Controls.Add(this.grpEQUser);
            this.tabEQSettings.Controls.Add(this.txtInstitutionUrl);
            this.tabEQSettings.Controls.Add(this.panel1);
            this.tabEQSettings.Location = new System.Drawing.Point(4, 22);
            this.tabEQSettings.Name = "tabEQSettings";
            this.tabEQSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabEQSettings.Size = new System.Drawing.Size(889, 429);
            this.tabEQSettings.TabIndex = 0;
            this.tabEQSettings.Text = "Connection";
            this.tabEQSettings.UseVisualStyleBackColor = true;
            // 
            // chkUseProxy
            // 
            this.chkUseProxy.AutoSize = true;
            this.chkUseProxy.Location = new System.Drawing.Point(469, 45);
            this.chkUseProxy.Name = "chkUseProxy";
            this.chkUseProxy.Size = new System.Drawing.Size(74, 17);
            this.chkUseProxy.TabIndex = 9;
            this.chkUseProxy.Text = "Use Proxy";
            this.chkUseProxy.UseVisualStyleBackColor = true;
            this.chkUseProxy.CheckedChanged += new System.EventHandler(this.chkUseProxy_CheckedChanged);
            // 
            // txtLogFileLocation
            // 
            this.txtLogFileLocation.Location = new System.Drawing.Point(9, 276);
            this.txtLogFileLocation.Name = "txtLogFileLocation";
            this.txtLogFileLocation.ReadOnly = true;
            this.txtLogFileLocation.Size = new System.Drawing.Size(297, 20);
            this.txtLogFileLocation.TabIndex = 7;
            this.txtLogFileLocation.TextChanged += new System.EventHandler(this.txtLogFileLocation_TextChanged);
            // 
            // txtProxyAddress
            // 
            this.txtProxyAddress.Enabled = false;
            this.txtProxyAddress.Location = new System.Drawing.Point(562, 67);
            this.txtProxyAddress.Name = "txtProxyAddress";
            this.txtProxyAddress.Size = new System.Drawing.Size(322, 20);
            this.txtProxyAddress.TabIndex = 11;
            this.txtProxyAddress.TextChanged += new System.EventHandler(this.txtProxyAddress_TextChanged);
            // 
            // btnSetLogFileLocation
            // 
            this.btnSetLogFileLocation.Location = new System.Drawing.Point(309, 276);
            this.btnSetLogFileLocation.Name = "btnSetLogFileLocation";
            this.btnSetLogFileLocation.Size = new System.Drawing.Size(39, 18);
            this.btnSetLogFileLocation.TabIndex = 8;
            this.btnSetLogFileLocation.Text = "...";
            this.btnSetLogFileLocation.UseVisualStyleBackColor = true;
            this.btnSetLogFileLocation.Click += new System.EventHandler(this.btnSetLogFileLocation_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(472, 121);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 13);
            this.label17.TabIndex = 14;
            this.label17.Text = "Proxy Password";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 260);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(84, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Log Files Folder:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(472, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 13);
            this.label19.TabIndex = 10;
            this.label19.Text = "Proxy Address";
            // 
            // rdioSharedSecret
            // 
            this.rdioSharedSecret.AutoSize = true;
            this.rdioSharedSecret.BackColor = System.Drawing.Color.White;
            this.rdioSharedSecret.Location = new System.Drawing.Point(10, 137);
            this.rdioSharedSecret.Name = "rdioSharedSecret";
            this.rdioSharedSecret.Size = new System.Drawing.Size(93, 17);
            this.rdioSharedSecret.TabIndex = 3;
            this.rdioSharedSecret.TabStop = true;
            this.rdioSharedSecret.Text = "Shared Secret";
            this.rdioSharedSecret.UseVisualStyleBackColor = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(472, 95);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(84, 13);
            this.label18.TabIndex = 12;
            this.label18.Text = "Proxy Username";
            // 
            // rdioEqUser
            // 
            this.rdioEqUser.AutoSize = true;
            this.rdioEqUser.BackColor = System.Drawing.Color.Transparent;
            this.rdioEqUser.Location = new System.Drawing.Point(10, 38);
            this.rdioEqUser.Name = "rdioEqUser";
            this.rdioEqUser.Size = new System.Drawing.Size(85, 17);
            this.rdioEqUser.TabIndex = 1;
            this.rdioEqUser.TabStop = true;
            this.rdioEqUser.Text = "Equella User";
            this.rdioEqUser.UseVisualStyleBackColor = false;
            this.rdioEqUser.CheckedChanged += new System.EventHandler(this.rdioEqUser_CheckedChanged);
            // 
            // txtProxyUsername
            // 
            this.txtProxyUsername.Enabled = false;
            this.txtProxyUsername.Location = new System.Drawing.Point(562, 92);
            this.txtProxyUsername.Name = "txtProxyUsername";
            this.txtProxyUsername.Size = new System.Drawing.Size(195, 20);
            this.txtProxyUsername.TabIndex = 13;
            this.txtProxyUsername.TextChanged += new System.EventHandler(this.txtProxyUsername_TextChanged);
            // 
            // txtProxyPassword
            // 
            this.txtProxyPassword.Enabled = false;
            this.txtProxyPassword.Location = new System.Drawing.Point(562, 120);
            this.txtProxyPassword.Name = "txtProxyPassword";
            this.txtProxyPassword.PasswordChar = '*';
            this.txtProxyPassword.Size = new System.Drawing.Size(194, 20);
            this.txtProxyPassword.TabIndex = 15;
            this.txtProxyPassword.TextChanged += new System.EventHandler(this.txtProxyPassword_TextChanged);
            // 
            // grpSharedSecret
            // 
            this.grpSharedSecret.Controls.Add(this.txtSharedSecretID);
            this.grpSharedSecret.Controls.Add(this.label6);
            this.grpSharedSecret.Controls.Add(this.txtSharedSecret);
            this.grpSharedSecret.Controls.Add(this.label4);
            this.grpSharedSecret.Controls.Add(this.label5);
            this.grpSharedSecret.Controls.Add(this.txtSSUsername);
            this.grpSharedSecret.Location = new System.Drawing.Point(10, 151);
            this.grpSharedSecret.Name = "grpSharedSecret";
            this.grpSharedSecret.Size = new System.Drawing.Size(445, 100);
            this.grpSharedSecret.TabIndex = 4;
            this.grpSharedSecret.TabStop = false;
            // 
            // txtSharedSecretID
            // 
            this.txtSharedSecretID.Location = new System.Drawing.Point(112, 45);
            this.txtSharedSecretID.Name = "txtSharedSecretID";
            this.txtSharedSecretID.Size = new System.Drawing.Size(279, 20);
            this.txtSharedSecretID.TabIndex = 1;
            this.txtSharedSecretID.TextChanged += new System.EventHandler(this.txtSharedSecretID_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Shared Secret";
            // 
            // txtSharedSecret
            // 
            this.txtSharedSecret.Location = new System.Drawing.Point(112, 70);
            this.txtSharedSecret.Name = "txtSharedSecret";
            this.txtSharedSecret.PasswordChar = '*';
            this.txtSharedSecret.Size = new System.Drawing.Size(279, 20);
            this.txtSharedSecret.TabIndex = 2;
            this.txtSharedSecret.TextChanged += new System.EventHandler(this.txtSharedSecret_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Shared Secret ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Username";
            // 
            // txtSSUsername
            // 
            this.txtSSUsername.Location = new System.Drawing.Point(112, 19);
            this.txtSSUsername.Name = "txtSSUsername";
            this.txtSSUsername.Size = new System.Drawing.Size(279, 20);
            this.txtSSUsername.TabIndex = 0;
            this.txtSSUsername.TextChanged += new System.EventHandler(this.txtSSUsername_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Institution URL";
            // 
            // grpEQUser
            // 
            this.grpEQUser.Controls.Add(this.txtPassword);
            this.grpEQUser.Controls.Add(this.label2);
            this.grpEQUser.Controls.Add(this.txtUsername);
            this.grpEQUser.Controls.Add(this.label1);
            this.grpEQUser.Location = new System.Drawing.Point(10, 52);
            this.grpEQUser.Name = "grpEQUser";
            this.grpEQUser.Size = new System.Drawing.Size(445, 78);
            this.grpEQUser.TabIndex = 2;
            this.grpEQUser.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(111, 42);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(280, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(111, 16);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(280, 20);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Username";
            // 
            // txtInstitutionUrl
            // 
            this.txtInstitutionUrl.Location = new System.Drawing.Point(90, 14);
            this.txtInstitutionUrl.Name = "txtInstitutionUrl";
            this.txtInstitutionUrl.Size = new System.Drawing.Size(533, 20);
            this.txtInstitutionUrl.TabIndex = 0;
            this.txtInstitutionUrl.TextChanged += new System.EventHandler(this.txtInstitutionUrl_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnTestConnection);
            this.panel1.Location = new System.Drawing.Point(475, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 626);
            this.panel1.TabIndex = 9;
            // 
            // tabQuery
            // 
            this.tabQuery.Controls.Add(this.lblLimit);
            this.tabQuery.Controls.Add(this.txtLimit);
            this.tabQuery.Controls.Add(this.txtItemVersion);
            this.tabQuery.Controls.Add(this.txtItemUUID);
            this.tabQuery.Controls.Add(this.label16);
            this.tabQuery.Controls.Add(this.dataGridColumns);
            this.tabQuery.Controls.Add(this.label21);
            this.tabQuery.Controls.Add(this.label20);
            this.tabQuery.Controls.Add(this.txtDelimiter);
            this.tabQuery.Controls.Add(this.lblDelimiter);
            this.tabQuery.Controls.Add(this.chkIncludeColumnsVersion);
            this.tabQuery.Controls.Add(this.chkIncludeColumnsID);
            this.tabQuery.Controls.Add(this.chkIncludeColumnsName);
            this.tabQuery.Controls.Add(this.label12);
            this.tabQuery.Controls.Add(this.grpCollections);
            this.tabQuery.Controls.Add(this.cmbSortOrder);
            this.tabQuery.Controls.Add(this.btnMoveDown);
            this.tabQuery.Controls.Add(this.lblSortOrder);
            this.tabQuery.Controls.Add(this.btnMoveUp);
            this.tabQuery.Controls.Add(this.chkReverseOrder);
            this.tabQuery.Controls.Add(this.cmdRunQuery);
            this.tabQuery.Controls.Add(this.chkIncludeNonLive);
            this.tabQuery.Controls.Add(this.label8);
            this.tabQuery.Controls.Add(this.txtFreeTextQuery);
            this.tabQuery.Controls.Add(this.label7);
            this.tabQuery.Controls.Add(this.txtWhereStatement);
            this.tabQuery.Location = new System.Drawing.Point(4, 22);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuery.Size = new System.Drawing.Size(889, 429);
            this.tabQuery.TabIndex = 2;
            this.tabQuery.Text = "Search";
            this.tabQuery.UseVisualStyleBackColor = true;
            // 
            // lblLimit
            // 
            this.lblLimit.AutoSize = true;
            this.lblLimit.Location = new System.Drawing.Point(771, 145);
            this.lblLimit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLimit.Name = "lblLimit";
            this.lblLimit.Size = new System.Drawing.Size(31, 13);
            this.lblLimit.TabIndex = 39;
            this.lblLimit.Text = "Limit:";
            // 
            // txtLimit
            // 
            this.txtLimit.Location = new System.Drawing.Point(804, 145);
            this.txtLimit.Margin = new System.Windows.Forms.Padding(2);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(76, 20);
            this.txtLimit.TabIndex = 38;
            this.txtLimit.TextChanged += new System.EventHandler(this.txtLimit_TextChanged);
            // 
            // txtItemVersion
            // 
            this.txtItemVersion.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemVersion.Location = new System.Drawing.Point(740, 21);
            this.txtItemVersion.Name = "txtItemVersion";
            this.txtItemVersion.Size = new System.Drawing.Size(23, 20);
            this.txtItemVersion.TabIndex = 35;
            this.txtItemVersion.TextChanged += new System.EventHandler(this.txtItemVersion_TextChanged);
            // 
            // txtItemUUID
            // 
            this.txtItemUUID.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemUUID.Location = new System.Drawing.Point(468, 21);
            this.txtItemUUID.Name = "txtItemUUID";
            this.txtItemUUID.Size = new System.Drawing.Size(264, 20);
            this.txtItemUUID.TabIndex = 32;
            this.txtItemUUID.TextChanged += new System.EventHandler(this.txtItemUUID_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(731, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(12, 13);
            this.label16.TabIndex = 37;
            this.label16.Text = "/";
            // 
            // dataGridColumns
            // 
            this.dataGridColumns.AllowUserToResizeRows = false;
            this.dataGridColumns.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridColumns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xpathColumn,
            this.selectedColumn});
            this.dataGridColumns.Location = new System.Drawing.Point(376, 254);
            this.dataGridColumns.Name = "dataGridColumns";
            this.dataGridColumns.RowTemplate.Height = 24;
            this.dataGridColumns.Size = new System.Drawing.Size(439, 141);
            this.dataGridColumns.TabIndex = 36;
            this.dataGridColumns.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridColumns_CellEndEdit);
            // 
            // xpathColumn
            // 
            this.xpathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.xpathColumn.HeaderText = "Column";
            this.xpathColumn.Name = "xpathColumn";
            this.xpathColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.xpathColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // selectedColumn
            // 
            this.selectedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.selectedColumn.HeaderText = "Select";
            this.selectedColumn.MinimumWidth = 40;
            this.selectedColumn.Name = "selectedColumn";
            this.selectedColumn.Width = 50;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(739, 6);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(26, 13);
            this.label21.TabIndex = 34;
            this.label21.Text = "Ver:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(469, 6);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 13);
            this.label20.TabIndex = 33;
            this.label20.Text = "Item UUID:";
            // 
            // txtDelimiter
            // 
            this.txtDelimiter.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDelimiter.Location = new System.Drawing.Point(539, 405);
            this.txtDelimiter.Name = "txtDelimiter";
            this.txtDelimiter.Size = new System.Drawing.Size(54, 20);
            this.txtDelimiter.TabIndex = 19;
            this.txtDelimiter.Text = "|";
            this.txtDelimiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDelimiter.TextChanged += new System.EventHandler(this.txtDelimiter_TextChanged);
            // 
            // lblDelimiter
            // 
            this.lblDelimiter.AutoSize = true;
            this.lblDelimiter.Location = new System.Drawing.Point(376, 408);
            this.lblDelimiter.Name = "lblDelimiter";
            this.lblDelimiter.Size = new System.Drawing.Size(160, 13);
            this.lblDelimiter.TabIndex = 18;
            this.lblDelimiter.Text = "Delimiter for multi-value columns:";
            // 
            // chkIncludeColumnsVersion
            // 
            this.chkIncludeColumnsVersion.AutoSize = true;
            this.chkIncludeColumnsVersion.Checked = true;
            this.chkIncludeColumnsVersion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeColumnsVersion.Location = new System.Drawing.Point(759, 232);
            this.chkIncludeColumnsVersion.Name = "chkIncludeColumnsVersion";
            this.chkIncludeColumnsVersion.Size = new System.Drawing.Size(61, 17);
            this.chkIncludeColumnsVersion.TabIndex = 11;
            this.chkIncludeColumnsVersion.Text = "Version";
            this.chkIncludeColumnsVersion.UseVisualStyleBackColor = true;
            this.chkIncludeColumnsVersion.CheckedChanged += new System.EventHandler(this.chkIncludeColumnsVersion_CheckedChanged);
            // 
            // chkIncludeColumnsID
            // 
            this.chkIncludeColumnsID.AutoSize = true;
            this.chkIncludeColumnsID.Checked = true;
            this.chkIncludeColumnsID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeColumnsID.Location = new System.Drawing.Point(715, 232);
            this.chkIncludeColumnsID.Name = "chkIncludeColumnsID";
            this.chkIncludeColumnsID.Size = new System.Drawing.Size(37, 17);
            this.chkIncludeColumnsID.TabIndex = 10;
            this.chkIncludeColumnsID.Text = "ID";
            this.chkIncludeColumnsID.UseVisualStyleBackColor = true;
            this.chkIncludeColumnsID.CheckedChanged += new System.EventHandler(this.chkIncludeColumnsID_CheckedChanged);
            // 
            // chkIncludeColumnsName
            // 
            this.chkIncludeColumnsName.AutoSize = true;
            this.chkIncludeColumnsName.Checked = true;
            this.chkIncludeColumnsName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeColumnsName.Location = new System.Drawing.Point(655, 232);
            this.chkIncludeColumnsName.Name = "chkIncludeColumnsName";
            this.chkIncludeColumnsName.Size = new System.Drawing.Size(54, 17);
            this.chkIncludeColumnsName.TabIndex = 9;
            this.chkIncludeColumnsName.Text = "Name";
            this.chkIncludeColumnsName.UseVisualStyleBackColor = true;
            this.chkIncludeColumnsName.CheckedChanged += new System.EventHandler(this.chkIncludeColumnsName_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(373, 233);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(277, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Columns to include in search results: e.g. /xml/item/name";
            // 
            // grpCollections
            // 
            this.grpCollections.Controls.Add(this.rdioSpecificCollections);
            this.grpCollections.Controls.Add(this.rdioAllCollections);
            this.grpCollections.Controls.Add(this.lstCollections);
            this.grpCollections.Controls.Add(this.btnRemoveCollection);
            this.grpCollections.Controls.Add(this.btnAddCollection);
            this.grpCollections.Location = new System.Drawing.Point(11, 227);
            this.grpCollections.Name = "grpCollections";
            this.grpCollections.Size = new System.Drawing.Size(326, 172);
            this.grpCollections.TabIndex = 4;
            this.grpCollections.TabStop = false;
            this.grpCollections.Text = "Collections to query";
            // 
            // rdioSpecificCollections
            // 
            this.rdioSpecificCollections.AutoSize = true;
            this.rdioSpecificCollections.Location = new System.Drawing.Point(6, 36);
            this.rdioSpecificCollections.Name = "rdioSpecificCollections";
            this.rdioSpecificCollections.Size = new System.Drawing.Size(129, 17);
            this.rdioSpecificCollections.TabIndex = 1;
            this.rdioSpecificCollections.Text = "Just these collections:";
            this.rdioSpecificCollections.UseVisualStyleBackColor = true;
            // 
            // rdioAllCollections
            // 
            this.rdioAllCollections.AutoSize = true;
            this.rdioAllCollections.Location = new System.Drawing.Point(6, 16);
            this.rdioAllCollections.Name = "rdioAllCollections";
            this.rdioAllCollections.Size = new System.Drawing.Size(89, 17);
            this.rdioAllCollections.TabIndex = 0;
            this.rdioAllCollections.Text = "All collections";
            this.rdioAllCollections.UseVisualStyleBackColor = true;
            this.rdioAllCollections.CheckedChanged += new System.EventHandler(this.rdioAllCollections_CheckedChanged);
            // 
            // lstCollections
            // 
            this.lstCollections.FormattingEnabled = true;
            this.lstCollections.Location = new System.Drawing.Point(67, 59);
            this.lstCollections.Name = "lstCollections";
            this.lstCollections.ScrollAlwaysVisible = true;
            this.lstCollections.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstCollections.Size = new System.Drawing.Size(251, 108);
            this.lstCollections.Sorted = true;
            this.lstCollections.TabIndex = 4;
            // 
            // btnRemoveCollection
            // 
            this.btnRemoveCollection.Location = new System.Drawing.Point(5, 84);
            this.btnRemoveCollection.Name = "btnRemoveCollection";
            this.btnRemoveCollection.Size = new System.Drawing.Size(57, 23);
            this.btnRemoveCollection.TabIndex = 3;
            this.btnRemoveCollection.Text = "Remove";
            this.btnRemoveCollection.UseVisualStyleBackColor = true;
            this.btnRemoveCollection.Click += new System.EventHandler(this.btnRemoveCollection_Click);
            // 
            // btnAddCollection
            // 
            this.btnAddCollection.Location = new System.Drawing.Point(5, 59);
            this.btnAddCollection.Name = "btnAddCollection";
            this.btnAddCollection.Size = new System.Drawing.Size(57, 23);
            this.btnAddCollection.TabIndex = 2;
            this.btnAddCollection.Text = "Add";
            this.btnAddCollection.UseVisualStyleBackColor = true;
            this.btnAddCollection.Click += new System.EventHandler(this.btnAddCollection_Click);
            // 
            // cmbSortOrder
            // 
            this.cmbSortOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortOrder.FormattingEnabled = true;
            this.cmbSortOrder.Items.AddRange(new object[] {
            "Rank",
            "Date Modified",
            "Name"});
            this.cmbSortOrder.Location = new System.Drawing.Point(54, 405);
            this.cmbSortOrder.Name = "cmbSortOrder";
            this.cmbSortOrder.Size = new System.Drawing.Size(92, 21);
            this.cmbSortOrder.TabIndex = 6;
            this.cmbSortOrder.SelectedIndexChanged += new System.EventHandler(this.cmbSortOrder_SelectedIndexChanged);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(818, 336);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(63, 23);
            this.btnMoveDown.TabIndex = 16;
            this.btnMoveDown.Text = "↓";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // lblSortOrder
            // 
            this.lblSortOrder.AutoSize = true;
            this.lblSortOrder.Location = new System.Drawing.Point(15, 407);
            this.lblSortOrder.Name = "lblSortOrder";
            this.lblSortOrder.Size = new System.Drawing.Size(36, 13);
            this.lblSortOrder.TabIndex = 5;
            this.lblSortOrder.Text = "Order:";
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveUp.Location = new System.Drawing.Point(818, 306);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(63, 23);
            this.btnMoveUp.TabIndex = 15;
            this.btnMoveUp.Text = "↑";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // chkReverseOrder
            // 
            this.chkReverseOrder.AutoSize = true;
            this.chkReverseOrder.Location = new System.Drawing.Point(153, 406);
            this.chkReverseOrder.Name = "chkReverseOrder";
            this.chkReverseOrder.Size = new System.Drawing.Size(95, 17);
            this.chkReverseOrder.TabIndex = 7;
            this.chkReverseOrder.Text = "Reverse Order";
            this.chkReverseOrder.UseVisualStyleBackColor = true;
            this.chkReverseOrder.CheckedChanged += new System.EventHandler(this.chkReverseOrder_CheckedChanged);
            // 
            // cmdRunQuery
            // 
            this.cmdRunQuery.Location = new System.Drawing.Point(775, 41);
            this.cmdRunQuery.Name = "cmdRunQuery";
            this.cmdRunQuery.Size = new System.Drawing.Size(90, 71);
            this.cmdRunQuery.TabIndex = 20;
            this.cmdRunQuery.Text = "Search\r\n(Ctrl+E)";
            this.cmdRunQuery.UseVisualStyleBackColor = true;
            this.cmdRunQuery.Click += new System.EventHandler(this.cmdRunQuery_Click);
            // 
            // chkIncludeNonLive
            // 
            this.chkIncludeNonLive.AutoSize = true;
            this.chkIncludeNonLive.Location = new System.Drawing.Point(771, 121);
            this.chkIncludeNonLive.Name = "chkIncludeNonLive";
            this.chkIncludeNonLive.Size = new System.Drawing.Size(122, 17);
            this.chkIncludeNonLive.TabIndex = 21;
            this.chkIncludeNonLive.Text = "Inclue non-live items";
            this.chkIncludeNonLive.UseVisualStyleBackColor = true;
            this.chkIncludeNonLive.CheckedChanged += new System.EventHandler(this.chkIncludeNonLive_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = " Free-text Query:";
            // 
            // txtFreeTextQuery
            // 
            this.txtFreeTextQuery.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFreeTextQuery.Location = new System.Drawing.Point(16, 21);
            this.txtFreeTextQuery.Name = "txtFreeTextQuery";
            this.txtFreeTextQuery.Size = new System.Drawing.Size(446, 20);
            this.txtFreeTextQuery.TabIndex = 1;
            this.txtFreeTextQuery.TextChanged += new System.EventHandler(this.txtFreeTextQuery_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(265, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "XML Query: (e.g. WHERE /xml/item/name = \'My Item\')";
            // 
            // txtWhereStatement
            // 
            this.txtWhereStatement.AcceptsReturn = true;
            this.txtWhereStatement.AcceptsTab = true;
            this.txtWhereStatement.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWhereStatement.Location = new System.Drawing.Point(16, 64);
            this.txtWhereStatement.Multiline = true;
            this.txtWhereStatement.Name = "txtWhereStatement";
            this.txtWhereStatement.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtWhereStatement.Size = new System.Drawing.Size(736, 157);
            this.txtWhereStatement.TabIndex = 3;
            this.txtWhereStatement.WordWrap = false;
            this.txtWhereStatement.TextChanged += new System.EventHandler(this.txtWhereStatement_TextChanged);
            // 
            // tabQueryResults
            // 
            this.tabQueryResults.Controls.Add(this.btnBrowseCSV);
            this.tabQueryResults.Controls.Add(this.chkLoadCsvWithProfile);
            this.tabQueryResults.Controls.Add(this.txtCSVPath);
            this.tabQueryResults.Controls.Add(this.label10);
            this.tabQueryResults.Controls.Add(this.btnLoadCSV);
            this.tabQueryResults.Controls.Add(this.btnUnselectAll);
            this.tabQueryResults.Controls.Add(this.btnInvertSelection);
            this.tabQueryResults.Controls.Add(this.btnSelectAll);
            this.tabQueryResults.Controls.Add(this.btnExportSelection);
            this.tabQueryResults.Controls.Add(this.btnExportAll);
            this.tabQueryResults.Controls.Add(this.panelResultsBackground);
            this.tabQueryResults.Location = new System.Drawing.Point(4, 22);
            this.tabQueryResults.Name = "tabQueryResults";
            this.tabQueryResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabQueryResults.Size = new System.Drawing.Size(889, 429);
            this.tabQueryResults.TabIndex = 1;
            this.tabQueryResults.Text = "Search Results";
            this.tabQueryResults.UseVisualStyleBackColor = true;
            this.tabQueryResults.Click += new System.EventHandler(this.tabQueryResults_Click);
            // 
            // btnBrowseCSV
            // 
            this.btnBrowseCSV.Location = new System.Drawing.Point(474, 4);
            this.btnBrowseCSV.Name = "btnBrowseCSV";
            this.btnBrowseCSV.Size = new System.Drawing.Size(55, 23);
            this.btnBrowseCSV.TabIndex = 8;
            this.btnBrowseCSV.Text = "Browse";
            this.btnBrowseCSV.UseVisualStyleBackColor = true;
            this.btnBrowseCSV.Click += new System.EventHandler(this.btnBrowseCSV_Click);
            // 
            // chkLoadCsvWithProfile
            // 
            this.chkLoadCsvWithProfile.AutoSize = true;
            this.chkLoadCsvWithProfile.Location = new System.Drawing.Point(590, 8);
            this.chkLoadCsvWithProfile.Name = "chkLoadCsvWithProfile";
            this.chkLoadCsvWithProfile.Size = new System.Drawing.Size(103, 17);
            this.chkLoadCsvWithProfile.TabIndex = 3;
            this.chkLoadCsvWithProfile.Text = "Load with profile";
            this.chkLoadCsvWithProfile.UseVisualStyleBackColor = true;
            this.chkLoadCsvWithProfile.CheckedChanged += new System.EventHandler(this.chkLoadCsvWithProfile_CheckedChanged);
            // 
            // txtCSVPath
            // 
            this.txtCSVPath.Location = new System.Drawing.Point(290, 6);
            this.txtCSVPath.Name = "txtCSVPath";
            this.txtCSVPath.Size = new System.Drawing.Size(178, 20);
            this.txtCSVPath.TabIndex = 9;
            this.txtCSVPath.TextChanged += new System.EventHandler(this.txtCSVPath_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(261, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "CSV:";
            // 
            // btnLoadCSV
            // 
            this.btnLoadCSV.Location = new System.Drawing.Point(530, 4);
            this.btnLoadCSV.Name = "btnLoadCSV";
            this.btnLoadCSV.Size = new System.Drawing.Size(55, 23);
            this.btnLoadCSV.TabIndex = 7;
            this.btnLoadCSV.Text = "Reload";
            this.btnLoadCSV.UseVisualStyleBackColor = true;
            this.btnLoadCSV.Click += new System.EventHandler(this.btnLoadCSV_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Location = new System.Drawing.Point(79, 4);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(73, 23);
            this.btnUnselectAll.TabIndex = 1;
            this.btnUnselectAll.Text = "Unselect All";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // btnInvertSelection
            // 
            this.btnInvertSelection.Location = new System.Drawing.Point(156, 4);
            this.btnInvertSelection.Name = "btnInvertSelection";
            this.btnInvertSelection.Size = new System.Drawing.Size(89, 23);
            this.btnInvertSelection.TabIndex = 2;
            this.btnInvertSelection.Text = "Invert Selection";
            this.btnInvertSelection.UseVisualStyleBackColor = true;
            this.btnInvertSelection.Click += new System.EventHandler(this.btnInvertSelection_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(3, 4);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(73, 23);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnExportSelection
            // 
            this.btnExportSelection.Location = new System.Drawing.Point(710, 4);
            this.btnExportSelection.Name = "btnExportSelection";
            this.btnExportSelection.Size = new System.Drawing.Size(92, 23);
            this.btnExportSelection.TabIndex = 3;
            this.btnExportSelection.Text = "Export Selection";
            this.btnExportSelection.UseVisualStyleBackColor = true;
            this.btnExportSelection.Click += new System.EventHandler(this.btnExportSelection_Click);
            // 
            // btnExportAll
            // 
            this.btnExportAll.Location = new System.Drawing.Point(803, 4);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(83, 23);
            this.btnExportAll.TabIndex = 4;
            this.btnExportAll.Text = "Export All";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // panelResultsBackground
            // 
            this.panelResultsBackground.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelResultsBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelResultsBackground.Controls.Add(this.dataGridResults);
            this.panelResultsBackground.Controls.Add(this.lblSearchPanelBackground);
            this.panelResultsBackground.Location = new System.Drawing.Point(1, 32);
            this.panelResultsBackground.Name = "panelResultsBackground";
            this.panelResultsBackground.Size = new System.Drawing.Size(887, 355);
            this.panelResultsBackground.TabIndex = 6;
            // 
            // dataGridResults
            // 
            this.dataGridResults.AllowUserToAddRows = false;
            this.dataGridResults.AllowUserToDeleteRows = false;
            this.dataGridResults.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridResults.CausesValidation = false;
            this.dataGridResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selection,
            this.ItemName,
            this.ItemID,
            this.ItemVersion});
            this.dataGridResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridResults.Location = new System.Drawing.Point(-5, -2);
            this.dataGridResults.MultiSelect = false;
            this.dataGridResults.Name = "dataGridResults";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridResults.RowHeadersWidth = 80;
            this.dataGridResults.RowTemplate.Height = 24;
            this.dataGridResults.ShowCellErrors = false;
            this.dataGridResults.ShowCellToolTips = false;
            this.dataGridResults.ShowEditingIcon = false;
            this.dataGridResults.ShowRowErrors = false;
            this.dataGridResults.Size = new System.Drawing.Size(890, 345);
            this.dataGridResults.TabIndex = 0;
            this.dataGridResults.Visible = false;
            this.dataGridResults.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridResults_CellMouseUp);
            this.dataGridResults.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridResults_CellMouseDoubleClick);
            // 
            // Selection
            // 
            this.Selection.HeaderText = "";
            this.Selection.Name = "Selection";
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Visible = false;
            // 
            // ItemID
            // 
            this.ItemID.HeaderText = "Item ID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.Visible = false;
            // 
            // ItemVersion
            // 
            this.ItemVersion.HeaderText = "Ver";
            this.ItemVersion.Name = "ItemVersion";
            this.ItemVersion.ReadOnly = true;
            this.ItemVersion.Visible = false;
            // 
            // lblSearchPanelBackground
            // 
            this.lblSearchPanelBackground.AutoSize = true;
            this.lblSearchPanelBackground.Location = new System.Drawing.Point(0, 0);
            this.lblSearchPanelBackground.Name = "lblSearchPanelBackground";
            this.lblSearchPanelBackground.Size = new System.Drawing.Size(57, 13);
            this.lblSearchPanelBackground.TabIndex = 1;
            this.lblSearchPanelBackground.Text = "No results.";
            // 
            // tabBulkUpdate
            // 
            this.tabBulkUpdate.Controls.Add(this.btnModifierScript);
            this.tabBulkUpdate.Controls.Add(this.btnModifierXSLT);
            this.tabBulkUpdate.Controls.Add(this.btnExportModifiers);
            this.tabBulkUpdate.Controls.Add(this.btnImportModifiers);
            this.tabBulkUpdate.Controls.Add(this.btnModifierReplaceText);
            this.tabBulkUpdate.Controls.Add(this.btnModifierCopyXml);
            this.tabBulkUpdate.Controls.Add(this.lstViewModifiers);
            this.tabBulkUpdate.Controls.Add(this.label9);
            this.tabBulkUpdate.Controls.Add(this.btnModifierRenameNode);
            this.tabBulkUpdate.Controls.Add(this.btnEditModifier);
            this.tabBulkUpdate.Controls.Add(this.grpboxExecution);
            this.tabBulkUpdate.Controls.Add(this.btnModifierUpdateText);
            this.tabBulkUpdate.Controls.Add(this.btnModifierRemoveXml);
            this.tabBulkUpdate.Controls.Add(this.btnModifierAddXml);
            this.tabBulkUpdate.Controls.Add(this.btnMoveModifierDown);
            this.tabBulkUpdate.Controls.Add(this.btnMoveModifierUp);
            this.tabBulkUpdate.Controls.Add(this.btnRemoveModifier);
            this.tabBulkUpdate.Controls.Add(this.label11);
            this.tabBulkUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabBulkUpdate.Name = "tabBulkUpdate";
            this.tabBulkUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tabBulkUpdate.Size = new System.Drawing.Size(889, 429);
            this.tabBulkUpdate.TabIndex = 3;
            this.tabBulkUpdate.Text = "Bulk Update";
            this.tabBulkUpdate.UseVisualStyleBackColor = true;
            // 
            // btnModifierScript
            // 
            this.btnModifierScript.Location = new System.Drawing.Point(6, 256);
            this.btnModifierScript.Name = "btnModifierScript";
            this.btnModifierScript.Size = new System.Drawing.Size(101, 26);
            this.btnModifierScript.TabIndex = 7;
            this.btnModifierScript.Text = "Script ->";
            this.btnModifierScript.UseVisualStyleBackColor = true;
            this.btnModifierScript.Click += new System.EventHandler(this.btnModifierScript_Click);
            // 
            // btnModifierXSLT
            // 
            this.btnModifierXSLT.Location = new System.Drawing.Point(6, 226);
            this.btnModifierXSLT.Name = "btnModifierXSLT";
            this.btnModifierXSLT.Size = new System.Drawing.Size(101, 26);
            this.btnModifierXSLT.TabIndex = 6;
            this.btnModifierXSLT.Text = "XSLT ->";
            this.btnModifierXSLT.UseVisualStyleBackColor = true;
            this.btnModifierXSLT.Click += new System.EventHandler(this.btnModifierXSLT_Click);
            // 
            // btnExportModifiers
            // 
            this.btnExportModifiers.Location = new System.Drawing.Point(625, 6);
            this.btnExportModifiers.Name = "btnExportModifiers";
            this.btnExportModifiers.Size = new System.Drawing.Size(91, 23);
            this.btnExportModifiers.TabIndex = 13;
            this.btnExportModifiers.Text = "Export Modifiers";
            this.btnExportModifiers.UseVisualStyleBackColor = true;
            this.btnExportModifiers.Click += new System.EventHandler(this.btnExportModifiers_Click);
            // 
            // btnImportModifiers
            // 
            this.btnImportModifiers.Location = new System.Drawing.Point(718, 5);
            this.btnImportModifiers.Name = "btnImportModifiers";
            this.btnImportModifiers.Size = new System.Drawing.Size(95, 24);
            this.btnImportModifiers.TabIndex = 14;
            this.btnImportModifiers.Text = "Import Modifiers";
            this.btnImportModifiers.UseVisualStyleBackColor = true;
            this.btnImportModifiers.Click += new System.EventHandler(this.btnImportModifiers_Click);
            // 
            // btnModifierReplaceText
            // 
            this.btnModifierReplaceText.Location = new System.Drawing.Point(6, 197);
            this.btnModifierReplaceText.Name = "btnModifierReplaceText";
            this.btnModifierReplaceText.Size = new System.Drawing.Size(101, 26);
            this.btnModifierReplaceText.TabIndex = 5;
            this.btnModifierReplaceText.Text = "Replace Text ->";
            this.btnModifierReplaceText.UseVisualStyleBackColor = true;
            this.btnModifierReplaceText.Click += new System.EventHandler(this.btnModifierReplaceText_Click);
            // 
            // btnModifierCopyXml
            // 
            this.btnModifierCopyXml.Location = new System.Drawing.Point(6, 168);
            this.btnModifierCopyXml.Name = "btnModifierCopyXml";
            this.btnModifierCopyXml.Size = new System.Drawing.Size(101, 26);
            this.btnModifierCopyXml.TabIndex = 4;
            this.btnModifierCopyXml.Text = "Copy XML ->";
            this.btnModifierCopyXml.UseVisualStyleBackColor = true;
            this.btnModifierCopyXml.Click += new System.EventHandler(this.btnModifierCopyXml_Click);
            // 
            // lstViewModifiers
            // 
            this.lstViewModifiers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.enabled,
            this.type,
            this.xpath,
            this.property});
            this.lstViewModifiers.FullRowSelect = true;
            this.lstViewModifiers.GridLines = true;
            this.lstViewModifiers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstViewModifiers.HideSelection = false;
            this.lstViewModifiers.Location = new System.Drawing.Point(114, 32);
            this.lstViewModifiers.Name = "lstViewModifiers";
            this.lstViewModifiers.Size = new System.Drawing.Size(699, 258);
            this.lstViewModifiers.TabIndex = 8;
            this.lstViewModifiers.UseCompatibleStateImageBehavior = false;
            this.lstViewModifiers.View = System.Windows.Forms.View.Details;
            this.lstViewModifiers.DoubleClick += new System.EventHandler(this.lstViewModifiers_DoubleClick);
            // 
            // enabled
            // 
            this.enabled.Text = "Enabled";
            this.enabled.Width = 70;
            // 
            // type
            // 
            this.type.Text = "Modifier Type";
            this.type.Width = 100;
            // 
            // xpath
            // 
            this.xpath.Text = "Attribute 1";
            this.xpath.Width = 200;
            // 
            // property
            // 
            this.property.Text = "Attribute 2";
            this.property.Width = 300;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 26);
            this.label9.TabIndex = 30;
            this.label9.Text = "Available\r\nModifiers";
            // 
            // btnModifierRenameNode
            // 
            this.btnModifierRenameNode.Location = new System.Drawing.Point(6, 110);
            this.btnModifierRenameNode.Name = "btnModifierRenameNode";
            this.btnModifierRenameNode.Size = new System.Drawing.Size(101, 26);
            this.btnModifierRenameNode.TabIndex = 2;
            this.btnModifierRenameNode.Text = "Rename Node ->";
            this.btnModifierRenameNode.UseVisualStyleBackColor = true;
            this.btnModifierRenameNode.Click += new System.EventHandler(this.btnModifierRenameNode_Click);
            // 
            // btnEditModifier
            // 
            this.btnEditModifier.Location = new System.Drawing.Point(820, 82);
            this.btnEditModifier.Name = "btnEditModifier";
            this.btnEditModifier.Size = new System.Drawing.Size(63, 24);
            this.btnEditModifier.TabIndex = 9;
            this.btnEditModifier.Text = "Edit";
            this.btnEditModifier.UseVisualStyleBackColor = true;
            this.btnEditModifier.Click += new System.EventHandler(this.btnEditModifier_Click);
            // 
            // grpboxExecution
            // 
            this.grpboxExecution.Controls.Add(this.chkUnselectProcessedResults);
            this.grpboxExecution.Controls.Add(this.chkCopyToStaging);
            this.grpboxExecution.Controls.Add(this.chkClearLogOnUpdate);
            this.grpboxExecution.Controls.Add(this.chkCreateLogFiles);
            this.grpboxExecution.Controls.Add(this.label14);
            this.grpboxExecution.Controls.Add(this.cmbLogOutput);
            this.grpboxExecution.Controls.Add(this.txtFirstFewToUpdate);
            this.grpboxExecution.Controls.Add(this.chkOnlyUpdateFirstFew);
            this.grpboxExecution.Controls.Add(this.rdioUpdateAllResults);
            this.grpboxExecution.Controls.Add(this.rdioSelectedResults);
            this.grpboxExecution.Controls.Add(this.btnTestBulkUpdate);
            this.grpboxExecution.Controls.Add(this.btnRunBulkUpdate);
            this.grpboxExecution.Location = new System.Drawing.Point(8, 290);
            this.grpboxExecution.Name = "grpboxExecution";
            this.grpboxExecution.Size = new System.Drawing.Size(867, 88);
            this.grpboxExecution.TabIndex = 13;
            this.grpboxExecution.TabStop = false;
            this.grpboxExecution.Text = "Execution";
            // 
            // chkUnselectProcessedResults
            // 
            this.chkUnselectProcessedResults.AutoSize = true;
            this.chkUnselectProcessedResults.Location = new System.Drawing.Point(136, 21);
            this.chkUnselectProcessedResults.Name = "chkUnselectProcessedResults";
            this.chkUnselectProcessedResults.Size = new System.Drawing.Size(208, 17);
            this.chkUnselectProcessedResults.TabIndex = 37;
            this.chkUnselectProcessedResults.Text = "Unselect results as they are processed";
            this.chkUnselectProcessedResults.UseVisualStyleBackColor = true;
            this.chkUnselectProcessedResults.CheckedChanged += new System.EventHandler(this.chkUnselectProcessedResults_CheckedChanged);
            // 
            // chkCopyToStaging
            // 
            this.chkCopyToStaging.AutoSize = true;
            this.chkCopyToStaging.Location = new System.Drawing.Point(334, 64);
            this.chkCopyToStaging.Name = "chkCopyToStaging";
            this.chkCopyToStaging.Size = new System.Drawing.Size(142, 17);
            this.chkCopyToStaging.TabIndex = 36;
            this.chkCopyToStaging.Text = "Copy item files to staging";
            this.chkCopyToStaging.UseVisualStyleBackColor = true;
            this.chkCopyToStaging.CheckedChanged += new System.EventHandler(this.chkCopyToStaging_CheckedChanged);
            // 
            // chkClearLogOnUpdate
            // 
            this.chkClearLogOnUpdate.AutoSize = true;
            this.chkClearLogOnUpdate.Location = new System.Drawing.Point(504, 64);
            this.chkClearLogOnUpdate.Name = "chkClearLogOnUpdate";
            this.chkClearLogOnUpdate.Size = new System.Drawing.Size(136, 17);
            this.chkClearLogOnUpdate.TabIndex = 35;
            this.chkClearLogOnUpdate.Text = "Clear log tab on update";
            this.chkClearLogOnUpdate.UseVisualStyleBackColor = true;
            this.chkClearLogOnUpdate.CheckedChanged += new System.EventHandler(this.chkClearLogOnUpdate_CheckedChanged);
            // 
            // chkCreateLogFiles
            // 
            this.chkCreateLogFiles.AutoSize = true;
            this.chkCreateLogFiles.Location = new System.Drawing.Point(504, 41);
            this.chkCreateLogFiles.Name = "chkCreateLogFiles";
            this.chkCreateLogFiles.Size = new System.Drawing.Size(95, 17);
            this.chkCreateLogFiles.TabIndex = 5;
            this.chkCreateLogFiles.Text = "Create log files";
            this.chkCreateLogFiles.UseVisualStyleBackColor = true;
            this.chkCreateLogFiles.CheckedChanged += new System.EventHandler(this.chkCreateLogFiles_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(428, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Output format:";
            // 
            // cmbLogOutput
            // 
            this.cmbLogOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogOutput.FormattingEnabled = true;
            this.cmbLogOutput.Items.AddRange(new object[] {
            "Indented XML (Large)",
            "Unindented XML (Medium)",
            "No XML (Compact)"});
            this.cmbLogOutput.Location = new System.Drawing.Point(504, 12);
            this.cmbLogOutput.Name = "cmbLogOutput";
            this.cmbLogOutput.Size = new System.Drawing.Size(156, 21);
            this.cmbLogOutput.TabIndex = 4;
            this.cmbLogOutput.SelectedIndexChanged += new System.EventHandler(this.cmbLogOutput_SelectedIndexChanged);
            // 
            // txtFirstFewToUpdate
            // 
            this.txtFirstFewToUpdate.Enabled = false;
            this.txtFirstFewToUpdate.Location = new System.Drawing.Point(250, 62);
            this.txtFirstFewToUpdate.Name = "txtFirstFewToUpdate";
            this.txtFirstFewToUpdate.Size = new System.Drawing.Size(43, 20);
            this.txtFirstFewToUpdate.TabIndex = 3;
            this.txtFirstFewToUpdate.Text = "1";
            this.txtFirstFewToUpdate.TextChanged += new System.EventHandler(this.txtFirstFewToUpdate_TextChanged);
            // 
            // chkOnlyUpdateFirstFew
            // 
            this.chkOnlyUpdateFirstFew.AutoSize = true;
            this.chkOnlyUpdateFirstFew.Location = new System.Drawing.Point(31, 64);
            this.chkOnlyUpdateFirstFew.Name = "chkOnlyUpdateFirstFew";
            this.chkOnlyUpdateFirstFew.Size = new System.Drawing.Size(220, 17);
            this.chkOnlyUpdateFirstFew.TabIndex = 2;
            this.chkOnlyUpdateFirstFew.Text = "Of the above options update only the first";
            this.chkOnlyUpdateFirstFew.UseVisualStyleBackColor = true;
            this.chkOnlyUpdateFirstFew.CheckedChanged += new System.EventHandler(this.chkOnlyUpdateFirstFew_CheckedChanged);
            // 
            // rdioUpdateAllResults
            // 
            this.rdioUpdateAllResults.AutoSize = true;
            this.rdioUpdateAllResults.Location = new System.Drawing.Point(15, 39);
            this.rdioUpdateAllResults.Name = "rdioUpdateAllResults";
            this.rdioUpdateAllResults.Size = new System.Drawing.Size(74, 17);
            this.rdioUpdateAllResults.TabIndex = 1;
            this.rdioUpdateAllResults.Text = "All Results";
            this.rdioUpdateAllResults.UseVisualStyleBackColor = true;
            // 
            // rdioSelectedResults
            // 
            this.rdioSelectedResults.AutoSize = true;
            this.rdioSelectedResults.Checked = true;
            this.rdioSelectedResults.Location = new System.Drawing.Point(15, 20);
            this.rdioSelectedResults.Name = "rdioSelectedResults";
            this.rdioSelectedResults.Size = new System.Drawing.Size(105, 17);
            this.rdioSelectedResults.TabIndex = 0;
            this.rdioSelectedResults.TabStop = true;
            this.rdioSelectedResults.Text = "Selected Results";
            this.rdioSelectedResults.UseVisualStyleBackColor = true;
            this.rdioSelectedResults.CheckedChanged += new System.EventHandler(this.rdioSelectedResults_CheckedChanged);
            // 
            // btnTestBulkUpdate
            // 
            this.btnTestBulkUpdate.Location = new System.Drawing.Point(673, 15);
            this.btnTestBulkUpdate.Name = "btnTestBulkUpdate";
            this.btnTestBulkUpdate.Size = new System.Drawing.Size(91, 66);
            this.btnTestBulkUpdate.TabIndex = 6;
            this.btnTestBulkUpdate.Text = "Test Bulk Update";
            this.btnTestBulkUpdate.UseVisualStyleBackColor = true;
            this.btnTestBulkUpdate.Click += new System.EventHandler(this.btnTestBulkUpdate_Click);
            // 
            // btnRunBulkUpdate
            // 
            this.btnRunBulkUpdate.Location = new System.Drawing.Point(770, 15);
            this.btnRunBulkUpdate.Name = "btnRunBulkUpdate";
            this.btnRunBulkUpdate.Size = new System.Drawing.Size(91, 66);
            this.btnRunBulkUpdate.TabIndex = 7;
            this.btnRunBulkUpdate.Text = "Run Bulk Update";
            this.btnRunBulkUpdate.UseVisualStyleBackColor = true;
            this.btnRunBulkUpdate.Click += new System.EventHandler(this.btnRunBulkUpdate_Click);
            // 
            // btnModifierUpdateText
            // 
            this.btnModifierUpdateText.Location = new System.Drawing.Point(6, 54);
            this.btnModifierUpdateText.Name = "btnModifierUpdateText";
            this.btnModifierUpdateText.Size = new System.Drawing.Size(101, 26);
            this.btnModifierUpdateText.TabIndex = 0;
            this.btnModifierUpdateText.Text = "Update Text ->";
            this.btnModifierUpdateText.UseVisualStyleBackColor = true;
            this.btnModifierUpdateText.Click += new System.EventHandler(this.btnModifierUpdateText_Click);
            // 
            // btnModifierRemoveXml
            // 
            this.btnModifierRemoveXml.Location = new System.Drawing.Point(6, 139);
            this.btnModifierRemoveXml.Name = "btnModifierRemoveXml";
            this.btnModifierRemoveXml.Size = new System.Drawing.Size(101, 26);
            this.btnModifierRemoveXml.TabIndex = 3;
            this.btnModifierRemoveXml.Text = "Remove XML ->";
            this.btnModifierRemoveXml.UseVisualStyleBackColor = true;
            this.btnModifierRemoveXml.Click += new System.EventHandler(this.btnModifierRemoveXml_Click);
            // 
            // btnModifierAddXml
            // 
            this.btnModifierAddXml.Location = new System.Drawing.Point(6, 83);
            this.btnModifierAddXml.Name = "btnModifierAddXml";
            this.btnModifierAddXml.Size = new System.Drawing.Size(101, 24);
            this.btnModifierAddXml.TabIndex = 1;
            this.btnModifierAddXml.Text = "Add XML ->";
            this.btnModifierAddXml.UseVisualStyleBackColor = true;
            this.btnModifierAddXml.Click += new System.EventHandler(this.btnModifierAddXml_Click);
            // 
            // btnMoveModifierDown
            // 
            this.btnMoveModifierDown.Location = new System.Drawing.Point(820, 141);
            this.btnMoveModifierDown.Name = "btnMoveModifierDown";
            this.btnMoveModifierDown.Size = new System.Drawing.Size(63, 23);
            this.btnMoveModifierDown.TabIndex = 11;
            this.btnMoveModifierDown.Text = "↓";
            this.btnMoveModifierDown.UseVisualStyleBackColor = true;
            this.btnMoveModifierDown.Click += new System.EventHandler(this.btnMoveModifierDown_Click);
            // 
            // btnMoveModifierUp
            // 
            this.btnMoveModifierUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveModifierUp.Location = new System.Drawing.Point(820, 112);
            this.btnMoveModifierUp.Name = "btnMoveModifierUp";
            this.btnMoveModifierUp.Size = new System.Drawing.Size(63, 23);
            this.btnMoveModifierUp.TabIndex = 10;
            this.btnMoveModifierUp.Text = "↑";
            this.btnMoveModifierUp.UseVisualStyleBackColor = true;
            this.btnMoveModifierUp.Click += new System.EventHandler(this.btnMoveModifierUp_Click);
            // 
            // btnRemoveModifier
            // 
            this.btnRemoveModifier.Location = new System.Drawing.Point(820, 170);
            this.btnRemoveModifier.Name = "btnRemoveModifier";
            this.btnRemoveModifier.Size = new System.Drawing.Size(63, 24);
            this.btnRemoveModifier.TabIndex = 12;
            this.btnRemoveModifier.Text = "Remove";
            this.btnRemoveModifier.UseVisualStyleBackColor = true;
            this.btnRemoveModifier.Click += new System.EventHandler(this.btnRemoveModifier_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(110, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Selected Modifiers:";
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.richTextLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(889, 429);
            this.tabLog.TabIndex = 4;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // richTextLog
            // 
            this.richTextLog.BackColor = System.Drawing.SystemColors.Window;
            this.richTextLog.DetectUrls = false;
            this.richTextLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextLog.Location = new System.Drawing.Point(0, 0);
            this.richTextLog.Name = "richTextLog";
            this.richTextLog.ReadOnly = true;
            this.richTextLog.Size = new System.Drawing.Size(889, 378);
            this.richTextLog.TabIndex = 0;
            this.richTextLog.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusResultsCount,
            this.toolStripSelectionCount,
            this.toolStripProgressBar,
            this.toolStripStatusProgressText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 484);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(897, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusResultsCount
            // 
            this.toolStripStatusResultsCount.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusResultsCount.Name = "toolStripStatusResultsCount";
            this.toolStripStatusResultsCount.Size = new System.Drawing.Size(50, 17);
            this.toolStripStatusResultsCount.Text = "0 results";
            // 
            // toolStripSelectionCount
            // 
            this.toolStripSelectionCount.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripSelectionCount.Name = "toolStripSelectionCount";
            this.toolStripSelectionCount.Size = new System.Drawing.Size(59, 17);
            this.toolStripSelectionCount.Text = "0 selected";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar.Visible = false;
            // 
            // toolStripStatusProgressText
            // 
            this.toolStripStatusProgressText.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusProgressText.Name = "toolStripStatusProgressText";
            this.toolStripStatusProgressText.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusProgressText.Text = "Processing 0/0";
            this.toolStripStatusProgressText.Visible = false;
            // 
            // toolStripMainMenu
            // 
            this.toolStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.stopToolStripButton,
            this.clearToolStripButton,
            this.helpToolStripButton});
            this.toolStripMainMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMainMenu.Name = "toolStripMainMenu";
            this.toolStripMainMenu.Size = new System.Drawing.Size(897, 25);
            this.toolStripMainMenu.TabIndex = 2;
            this.toolStripMainMenu.TabStop = true;
            this.toolStripMainMenu.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "Clear Settings";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // stopToolStripButton
            // 
            this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopToolStripButton.Enabled = false;
            this.stopToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripButton.Image")));
            this.stopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopToolStripButton.Name = "stopToolStripButton";
            this.stopToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.stopToolStripButton.Text = "&Stop Processing";
            this.stopToolStripButton.Click += new System.EventHandler(this.stopToolStripButton_Click);
            // 
            // clearToolStripButton
            // 
            this.clearToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearToolStripButton.Enabled = false;
            this.clearToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("clearToolStripButton.Image")));
            this.clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearToolStripButton.Name = "clearToolStripButton";
            this.clearToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.clearToolStripButton.Text = "Clear";
            this.clearToolStripButton.Click += new System.EventHandler(this.clearToolStripButton_Click);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "About";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnTestConnection;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(897, 506);
            this.Controls.Add(this.toolStripMainMenu);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.tabMain.ResumeLayout(false);
            this.tabEQSettings.ResumeLayout(false);
            this.tabEQSettings.PerformLayout();
            this.grpSharedSecret.ResumeLayout(false);
            this.grpSharedSecret.PerformLayout();
            this.grpEQUser.ResumeLayout(false);
            this.grpEQUser.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabQuery.ResumeLayout(false);
            this.tabQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridColumns)).EndInit();
            this.grpCollections.ResumeLayout(false);
            this.grpCollections.PerformLayout();
            this.tabQueryResults.ResumeLayout(false);
            this.tabQueryResults.PerformLayout();
            this.panelResultsBackground.ResumeLayout(false);
            this.panelResultsBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResults)).EndInit();
            this.tabBulkUpdate.ResumeLayout(false);
            this.tabBulkUpdate.PerformLayout();
            this.grpboxExecution.ResumeLayout(false);
            this.grpboxExecution.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripMainMenu.ResumeLayout(false);
            this.toolStripMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabEQSettings;
        private System.Windows.Forms.TabPage tabQueryResults;
        private System.Windows.Forms.TextBox txtInstitutionUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabQuery;
        private System.Windows.Forms.TabPage tabBulkUpdate;
        private System.Windows.Forms.TextBox txtWhereStatement;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFreeTextQuery;
        private System.Windows.Forms.ListBox lstCollections;
        private System.Windows.Forms.GroupBox grpCollections;
        private System.Windows.Forms.Button btnRemoveCollection;
        private System.Windows.Forms.Button btnAddCollection;
        private System.Windows.Forms.RadioButton rdioAllCollections;
        private System.Windows.Forms.RadioButton rdioSpecificCollections;
        private System.Windows.Forms.ComboBox cmbSortOrder;
        private System.Windows.Forms.Label lblSortOrder;
        private System.Windows.Forms.CheckBox chkIncludeNonLive;
        private System.Windows.Forms.CheckBox chkReverseOrder;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Button cmdRunQuery;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveModifierDown;
        private System.Windows.Forms.Button btnMoveModifierUp;
        private System.Windows.Forms.Button btnRemoveModifier;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnModifierUpdateText;
        private System.Windows.Forms.Button btnModifierRemoveXml;
        private System.Windows.Forms.Button btnModifierAddXml;
        private System.Windows.Forms.GroupBox grpboxExecution;
        private System.Windows.Forms.Button btnTestBulkUpdate;
        private System.Windows.Forms.Button btnRunBulkUpdate;
        private System.Windows.Forms.CheckBox chkOnlyUpdateFirstFew;
        private System.Windows.Forms.RadioButton rdioUpdateAllResults;
        private System.Windows.Forms.RadioButton rdioSelectedResults;
        private System.Windows.Forms.TextBox txtFirstFewToUpdate;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusResultsCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSelectionCount;
        private System.Windows.Forms.Button btnInvertSelection;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnExportSelection;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox richTextLog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusProgressText;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnEditModifier;
        private System.Windows.Forms.Button btnModifierRenameNode;
        private System.Windows.Forms.Panel panelResultsBackground;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rdioSharedSecret;
        private System.Windows.Forms.RadioButton rdioEqUser;
        private System.Windows.Forms.GroupBox grpSharedSecret;
        private System.Windows.Forms.TextBox txtSharedSecretID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox txtSharedSecret;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSSUsername;
        private System.Windows.Forms.GroupBox grpEQUser;
        private System.Windows.Forms.MaskedTextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridResults;
        private System.Windows.Forms.Label lblSearchPanelBackground;
        private System.Windows.Forms.CheckBox chkIncludeColumnsVersion;
        private System.Windows.Forms.CheckBox chkIncludeColumnsID;
        private System.Windows.Forms.CheckBox chkIncludeColumnsName;
        private System.Windows.Forms.ToolStrip toolStripMainMenu;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripButton stopToolStripButton;
        private System.Windows.Forms.ToolStripButton clearToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView lstViewModifiers;
        private System.Windows.Forms.ColumnHeader xpath;
        private System.Windows.Forms.ColumnHeader property;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader enabled;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbLogOutput;
        private System.Windows.Forms.CheckBox chkCreateLogFiles;
        private System.Windows.Forms.Button btnSetLogFileLocation;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtLogFileLocation;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserLogFileLocation;
        private System.Windows.Forms.Label lblDelimiter;
        private System.Windows.Forms.TextBox txtDelimiter;
        private System.Windows.Forms.TextBox txtProxyAddress;
        private System.Windows.Forms.CheckBox chkUseProxy;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtProxyUsername;
        private System.Windows.Forms.TextBox txtProxyPassword;
        private System.Windows.Forms.Button btnModifierCopyXml;
        private System.Windows.Forms.Button btnModifierReplaceText;
        private System.Windows.Forms.Button btnExportModifiers;
        private System.Windows.Forms.Button btnImportModifiers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selection;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemVersion;
        private System.Windows.Forms.Button btnModifierXSLT;
        private System.Windows.Forms.ToolTip modifierToolTip;
        private System.Windows.Forms.Button btnModifierScript;
        private System.Windows.Forms.TextBox txtItemUUID;
        private System.Windows.Forms.TextBox txtItemVersion;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DataGridView dataGridColumns;
        private System.Windows.Forms.CheckBox chkClearLogOnUpdate;
        private System.Windows.Forms.CheckBox chkCopyToStaging;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLimit;
        private System.Windows.Forms.TextBox txtLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn xpathColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectedColumn;
        private System.Windows.Forms.CheckBox chkUnselectProcessedResults;
        private System.Windows.Forms.Button btnLoadCSV;
        private System.Windows.Forms.Button btnBrowseCSV;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCSVPath;
        private System.Windows.Forms.CheckBox chkLoadCsvWithProfile;
    }
}

