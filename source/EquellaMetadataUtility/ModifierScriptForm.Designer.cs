namespace EquellaMetadataUtility
{
    partial class ModifierScriptForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifierScriptForm));
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkRunOnlyOnce = new System.Windows.Forms.CheckBox();
            this.fctbScript = new FastColoredTextBoxNS.FastColoredTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFindReplace = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Location = new System.Drawing.Point(101, 375);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 3;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(1, 370);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(85, 25);
            this.btnValidate.TabIndex = 2;
            this.btnValidate.Text = "Check";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(546, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 28);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(452, 368);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 28);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkRunOnlyOnce
            // 
            this.chkRunOnlyOnce.AutoSize = true;
            this.chkRunOnlyOnce.Location = new System.Drawing.Point(238, 375);
            this.chkRunOnlyOnce.Name = "chkRunOnlyOnce";
            this.chkRunOnlyOnce.Size = new System.Drawing.Size(99, 17);
            this.chkRunOnlyOnce.TabIndex = 7;
            this.chkRunOnlyOnce.Text = "Run Only Once";
            this.chkRunOnlyOnce.UseVisualStyleBackColor = true;
            // 
            // fctbScript
            // 
            this.fctbScript.AllowDrop = true;
            this.fctbScript.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.fctbScript.BackBrush = null;
            this.fctbScript.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctbScript.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctbScript.IsReplaceMode = false;
            this.fctbScript.Language = FastColoredTextBoxNS.Language.JS;
            this.fctbScript.LeftBracket = '(';
            this.fctbScript.Location = new System.Drawing.Point(1, 24);
            this.fctbScript.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fctbScript.Name = "fctbScript";
            this.fctbScript.Paddings = new System.Windows.Forms.Padding(0);
            this.fctbScript.RightBracket = ')';
            this.fctbScript.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctbScript.Size = new System.Drawing.Size(112, 122);
            this.fctbScript.TabIndex = 8;
            this.fctbScript.Text = "fastColoredTextBox1";
            this.fctbScript.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fctbScript_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFindReplace});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(645, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnFindReplace
            // 
            this.btnFindReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFindReplace.Image = ((System.Drawing.Image)(resources.GetObject("btnFindReplace.Image")));
            this.btnFindReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindReplace.Name = "btnFindReplace";
            this.btnFindReplace.Size = new System.Drawing.Size(34, 22);
            this.btnFindReplace.Text = "Find";
            this.btnFindReplace.Click += new System.EventHandler(this.btnFindReplace_Click);
            // 
            // ModifierScriptForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(645, 398);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.fctbScript);
            this.Controls.Add(this.chkRunOnlyOnce);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModifierScriptForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Script Modifier";
            this.Load += new System.EventHandler(this.ModifierScriptForm_Load);
            this.Resize += new System.EventHandler(this.ModifierScriptForm_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkRunOnlyOnce;
        private FastColoredTextBoxNS.FastColoredTextBox fctbScript;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFindReplace;
    }
}