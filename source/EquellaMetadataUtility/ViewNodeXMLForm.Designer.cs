namespace EquellaMetadataUtility
{
    partial class ViewNodeXMLForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewNodeXMLForm));
            this.txtNodeXML = new System.Windows.Forms.TextBox();
            this.btnCloseCancel = new System.Windows.Forms.Button();
            this.btnEditSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNodeXML
            // 
            this.txtNodeXML.AcceptsReturn = true;
            this.txtNodeXML.AcceptsTab = true;
            this.txtNodeXML.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtNodeXML.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNodeXML.Location = new System.Drawing.Point(1, 1);
            this.txtNodeXML.Multiline = true;
            this.txtNodeXML.Name = "txtNodeXML";
            this.txtNodeXML.ReadOnly = true;
            this.txtNodeXML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNodeXML.Size = new System.Drawing.Size(708, 360);
            this.txtNodeXML.TabIndex = 1;
            // 
            // btnCloseCancel
            // 
            this.btnCloseCancel.Location = new System.Drawing.Point(615, 367);
            this.btnCloseCancel.Name = "btnCloseCancel";
            this.btnCloseCancel.Size = new System.Drawing.Size(88, 28);
            this.btnCloseCancel.TabIndex = 2;
            this.btnCloseCancel.Text = "Close";
            this.btnCloseCancel.UseVisualStyleBackColor = true;
            this.btnCloseCancel.Click += new System.EventHandler(this.btnCloseCancel_Click);
            // 
            // btnEditSave
            // 
            this.btnEditSave.Location = new System.Drawing.Point(526, 367);
            this.btnEditSave.Name = "btnEditSave";
            this.btnEditSave.Size = new System.Drawing.Size(88, 28);
            this.btnEditSave.TabIndex = 3;
            this.btnEditSave.Text = "Edit";
            this.btnEditSave.UseVisualStyleBackColor = true;
            this.btnEditSave.Click += new System.EventHandler(this.btnEditSave_Click);
            // 
            // ViewNodeXMLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 398);
            this.Controls.Add(this.btnEditSave);
            this.Controls.Add(this.btnCloseCancel);
            this.Controls.Add(this.txtNodeXML);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewNodeXMLForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ViewNodeXMLForm";
            this.Load += new System.EventHandler(this.ViewNodeXMLForm_Load);
            this.Resize += new System.EventHandler(this.ViewNodeXMLForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNodeXML;
        private System.Windows.Forms.Button btnCloseCancel;
        private System.Windows.Forms.Button btnEditSave;
    }
}