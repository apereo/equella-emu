namespace EquellaMetadataUtility
{
    partial class Collections
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Collections));
            this.lstCollections = new System.Windows.Forms.ListBox();
            this.btOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstCollections
            // 
            this.lstCollections.FormattingEnabled = true;
            this.lstCollections.Location = new System.Drawing.Point(0, 2);
            this.lstCollections.Name = "lstCollections";
            this.lstCollections.ScrollAlwaysVisible = true;
            this.lstCollections.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstCollections.Size = new System.Drawing.Size(475, 212);
            this.lstCollections.Sorted = true;
            this.lstCollections.TabIndex = 0;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(297, 228);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(84, 23);
            this.btOk.TabIndex = 1;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(384, 228);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Collections
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(476, 252);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.lstCollections);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Collections";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Collections";
            this.Load += new System.EventHandler(this.Collections_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstCollections;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btnCancel;
    }
}