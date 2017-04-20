namespace EquellaMetadataUtility
{
    partial class ModifierXSLTForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifierXSLTForm));
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.fctbXSLT = new FastColoredTextBoxNS.FastColoredTextBox();
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
            this.chkEnabled.Location = new System.Drawing.Point(135, 462);
            this.chkEnabled.Margin = new System.Windows.Forms.Padding(4);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(82, 21);
            this.chkEnabled.TabIndex = 3;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(1, 455);
            this.btnValidate.Margin = new System.Windows.Forms.Padding(4);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(113, 31);
            this.btnValidate.TabIndex = 2;
            this.btnValidate.Text = "Check";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(728, 453);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(117, 34);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(603, 453);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(117, 34);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // fctbXSLT
            // 
            this.fctbXSLT.AllowDrop = true;
            this.fctbXSLT.AutoScrollMinSize = new System.Drawing.Size(221, 18);
            this.fctbXSLT.BackBrush = null;
            this.fctbXSLT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctbXSLT.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctbXSLT.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fctbXSLT.IsReplaceMode = false;
            this.fctbXSLT.Language = FastColoredTextBoxNS.Language.HTML;
            this.fctbXSLT.Location = new System.Drawing.Point(1, 28);
            this.fctbXSLT.Name = "fctbXSLT";
            this.fctbXSLT.Paddings = new System.Windows.Forms.Padding(0);
            this.fctbXSLT.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctbXSLT.Size = new System.Drawing.Size(150, 150);
            this.fctbXSLT.TabIndex = 7;
            this.fctbXSLT.Text = "fastColoredTextBox1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFindReplace});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(860, 27);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnFindReplace
            // 
            this.btnFindReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFindReplace.Image = ((System.Drawing.Image)(resources.GetObject("btnFindReplace.Image")));
            this.btnFindReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindReplace.Name = "btnFindReplace";
            this.btnFindReplace.Size = new System.Drawing.Size(41, 24);
            this.btnFindReplace.Text = "Find";
            this.btnFindReplace.Click += new System.EventHandler(this.btnFindReplace_Click);
            // 
            // ModifierXSLTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(860, 490);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.fctbXSLT);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ModifierXSLTForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XSLT Modifier";
            this.Load += new System.EventHandler(this.ModifierXSLTForm_Load);
            this.Resize += new System.EventHandler(this.ModifierXSLTForm_Resize);
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
        private FastColoredTextBoxNS.FastColoredTextBox fctbXSLT;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFindReplace;
    }
}