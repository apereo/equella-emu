namespace EquellaMetadataUtility
{
    partial class ModifierAddXMLForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifierAddXMLForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXpath = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.fctbAddXML = new FastColoredTextBoxNS.FastColoredTextBox();
            this.chkCreateNode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(456, 283);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 28);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(549, 283);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "XML Fragment:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "xpath:";
            // 
            // txtXpath
            // 
            this.txtXpath.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtXpath.Location = new System.Drawing.Point(41, 0);
            this.txtXpath.Name = "txtXpath";
            this.txtXpath.Size = new System.Drawing.Size(602, 20);
            this.txtXpath.TabIndex = 1;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(4, 286);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(84, 24);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Check";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Location = new System.Drawing.Point(109, 291);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 5;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // fctbAddXML
            // 
            this.fctbAddXML.AllowDrop = true;
            this.fctbAddXML.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.fctbAddXML.BackBrush = null;
            this.fctbAddXML.CommentPrefix = null;
            this.fctbAddXML.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctbAddXML.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctbAddXML.IsReplaceMode = false;
            this.fctbAddXML.Language = FastColoredTextBoxNS.Language.HTML;
            this.fctbAddXML.LeftBracket = '<';
            this.fctbAddXML.LeftBracket2 = '(';
            this.fctbAddXML.Location = new System.Drawing.Point(2, 42);
            this.fctbAddXML.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fctbAddXML.Name = "fctbAddXML";
            this.fctbAddXML.Paddings = new System.Windows.Forms.Padding(0);
            this.fctbAddXML.RightBracket = '>';
            this.fctbAddXML.RightBracket2 = ')';
            this.fctbAddXML.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctbAddXML.Size = new System.Drawing.Size(112, 122);
            this.fctbAddXML.TabIndex = 8;
            this.fctbAddXML.Text = "fastColoredTextBox1";
            // 
            // chkCreateNode
            // 
            this.chkCreateNode.AutoSize = true;
            this.chkCreateNode.Location = new System.Drawing.Point(109, 25);
            this.chkCreateNode.Name = "chkCreateNode";
            this.chkCreateNode.Size = new System.Drawing.Size(168, 17);
            this.chkCreateNode.TabIndex = 9;
            this.chkCreateNode.Text = "Create node if it does not exist";
            this.chkCreateNode.UseVisualStyleBackColor = true;
            // 
            // ModifierAddXMLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(646, 311);
            this.Controls.Add(this.chkCreateNode);
            this.Controls.Add(this.fctbAddXML);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtXpath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModifierAddXMLForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add XML Modifier";
            this.Load += new System.EventHandler(this.ModifierAddXMLForm_Load);
            this.Resize += new System.EventHandler(this.ModifierAddXMLForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtXpath;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.CheckBox chkEnabled;
        private FastColoredTextBoxNS.FastColoredTextBox fctbAddXML;
        private System.Windows.Forms.CheckBox chkCreateNode;
    }
}