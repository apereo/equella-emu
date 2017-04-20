namespace EquellaMetadataUtility
{
    partial class ViewItemXMLForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewItemXMLForm));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.fctbItemXml = new FastColoredTextBoxNS.FastColoredTextBox();
            this.toolStripViewXml = new System.Windows.Forms.ToolStrip();
            this.viewXmlFind = new System.Windows.Forms.ToolStripButton();
            this.btnEditSave = new System.Windows.Forms.Button();
            this.chkHideSystemMetadata = new System.Windows.Forms.CheckBox();
            this.btnGoUp = new System.Windows.Forms.Button();
            this.btnGoDown = new System.Windows.Forms.Button();
            this.toolStripViewXml.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(823, 454);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 34);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(9, 453);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 37);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh (F5)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // fctbItemXml
            // 
            this.fctbItemXml.AcceptsTab = false;
            this.fctbItemXml.AllowDrop = true;
            this.fctbItemXml.AutoScrollMinSize = new System.Drawing.Size(0, 18);
            this.fctbItemXml.BackBrush = null;
            this.fctbItemXml.CommentPrefix = null;
            this.fctbItemXml.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctbItemXml.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctbItemXml.IsReplaceMode = false;
            this.fctbItemXml.Language = FastColoredTextBoxNS.Language.HTML;
            this.fctbItemXml.LeftBracket = '<';
            this.fctbItemXml.LeftBracket2 = '(';
            this.fctbItemXml.Location = new System.Drawing.Point(0, 27);
            this.fctbItemXml.Name = "fctbItemXml";
            this.fctbItemXml.Paddings = new System.Windows.Forms.Padding(0);
            this.fctbItemXml.ReadOnly = true;
            this.fctbItemXml.RightBracket = '>';
            this.fctbItemXml.RightBracket2 = ')';
            this.fctbItemXml.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctbItemXml.Size = new System.Drawing.Size(683, 382);
            this.fctbItemXml.TabIndex = 0;
            this.fctbItemXml.Text = "fastColoredTextBox1";
            this.fctbItemXml.WordWrap = true;
            this.fctbItemXml.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fctbItemXml_TextChanged);
            // 
            // toolStripViewXml
            // 
            this.toolStripViewXml.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewXmlFind});
            this.toolStripViewXml.Location = new System.Drawing.Point(0, 0);
            this.toolStripViewXml.Name = "toolStripViewXml";
            this.toolStripViewXml.Size = new System.Drawing.Size(948, 27);
            this.toolStripViewXml.TabIndex = 6;
            this.toolStripViewXml.Text = "toolStrip1";
            // 
            // viewXmlFind
            // 
            this.viewXmlFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.viewXmlFind.Image = ((System.Drawing.Image)(resources.GetObject("viewXmlFind.Image")));
            this.viewXmlFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewXmlFind.Name = "viewXmlFind";
            this.viewXmlFind.Size = new System.Drawing.Size(41, 24);
            this.viewXmlFind.Text = "Find";
            this.viewXmlFind.Click += new System.EventHandler(this.viewXmlFind_Click);
            // 
            // btnEditSave
            // 
            this.btnEditSave.Location = new System.Drawing.Point(698, 454);
            this.btnEditSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditSave.Name = "btnEditSave";
            this.btnEditSave.Size = new System.Drawing.Size(117, 34);
            this.btnEditSave.TabIndex = 4;
            this.btnEditSave.Text = "Edit";
            this.btnEditSave.UseVisualStyleBackColor = true;
            this.btnEditSave.Click += new System.EventHandler(this.btnEditSave_Click);
            // 
            // chkHideSystemMetadata
            // 
            this.chkHideSystemMetadata.AutoSize = true;
            this.chkHideSystemMetadata.Location = new System.Drawing.Point(193, 3);
            this.chkHideSystemMetadata.Name = "chkHideSystemMetadata";
            this.chkHideSystemMetadata.Size = new System.Drawing.Size(172, 21);
            this.chkHideSystemMetadata.TabIndex = 7;
            this.chkHideSystemMetadata.Text = "Hide System Metadata";
            this.chkHideSystemMetadata.UseVisualStyleBackColor = true;
            this.chkHideSystemMetadata.CheckedChanged += new System.EventHandler(this.chkHideSystemMetadata_CheckedChanged);
            // 
            // btnGoUp
            // 
            this.btnGoUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoUp.Location = new System.Drawing.Point(326, 456);
            this.btnGoUp.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoUp.Name = "btnGoUp";
            this.btnGoUp.Size = new System.Drawing.Size(84, 28);
            this.btnGoUp.TabIndex = 2;
            this.btnGoUp.Text = "↑";
            this.btnGoUp.UseVisualStyleBackColor = true;
            this.btnGoUp.Click += new System.EventHandler(this.btnGoUp_Click);
            // 
            // btnGoDown
            // 
            this.btnGoDown.Location = new System.Drawing.Point(418, 456);
            this.btnGoDown.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoDown.Name = "btnGoDown";
            this.btnGoDown.Size = new System.Drawing.Size(84, 28);
            this.btnGoDown.TabIndex = 3;
            this.btnGoDown.Text = "↓";
            this.btnGoDown.UseVisualStyleBackColor = true;
            this.btnGoDown.Click += new System.EventHandler(this.btnGoDown_Click);
            // 
            // ViewItemXMLForm
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(948, 491);
            this.Controls.Add(this.btnGoDown);
            this.Controls.Add(this.btnGoUp);
            this.Controls.Add(this.chkHideSystemMetadata);
            this.Controls.Add(this.btnEditSave);
            this.Controls.Add(this.toolStripViewXml);
            this.Controls.Add(this.fctbItemXml);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewItemXMLForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item XML Metadata";
            this.Load += new System.EventHandler(this.ViewItemXMLForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewItemXMLForm_FormClosing);
            this.Resize += new System.EventHandler(this.ViewItemXMLForm_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewItemXMLForm_KeyDown);
            this.toolStripViewXml.ResumeLayout(false);
            this.toolStripViewXml.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private FastColoredTextBoxNS.FastColoredTextBox fctbItemXml;
        private System.Windows.Forms.ToolStrip toolStripViewXml;
        private System.Windows.Forms.ToolStripButton viewXmlFind;
        private System.Windows.Forms.Button btnEditSave;
        private System.Windows.Forms.CheckBox chkHideSystemMetadata;
        private System.Windows.Forms.Button btnGoUp;
        private System.Windows.Forms.Button btnGoDown;
    }
}