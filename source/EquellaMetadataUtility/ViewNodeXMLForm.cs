using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquellaMetadataUtility
{
    public partial class ViewNodeXMLForm : Form
    {
        public bool editable = false;
        public bool editModeEnabled = false;
        public string nodeText;
        public string newNodeText;
        public bool nodeEdited = false;
        public string xpath;

        public ViewNodeXMLForm()
        {
            InitializeComponent();
            ViewNodeXMLForm_Resize();
        }

        private void ViewNodeXMLForm_Resize(object sender, EventArgs e)
        {
            ViewNodeXMLForm_Resize();
        }

        private void ViewNodeXMLForm_Resize()
        {
            int buttonMargin = 16;

            txtNodeXML.Width = this.ClientSize.Width;
            txtNodeXML.Height = this.ClientSize.Height - btnCloseCancel.Height - buttonMargin;

            btnCloseCancel.Left = this.ClientSize.Width - btnCloseCancel.Width - buttonMargin;
            btnCloseCancel.Top = this.ClientSize.Height - btnCloseCancel.Height - buttonMargin / 2;
            btnEditSave.Top = btnCloseCancel.Top;
            btnEditSave.Left = this.Width - btnEditSave.Width - btnCloseCancel.Width - buttonMargin * 2;
        }

        private void ViewNodeXMLForm_Load(object sender, EventArgs e)
        {
            txtNodeXML.Text = nodeText;
            txtNodeXML.SelectionStart = 0;

            if (editable)
            {
                this.Text = "View Node: " + xpath + " (editable)";
                btnEditSave.Visible = true;
            }
            else
            {
                this.Text = "View Node: " + xpath + " (read-only)";
                btnEditSave.Visible = false;
                btnCloseCancel.Text = "Cancel";
            }
            btnCloseCancel.Focus();
        }

        private void btnCloseCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            if (!editModeEnabled)
            {
                txtNodeXML.BackColor = System.Drawing.SystemColors.ControlLightLight;
                txtNodeXML.ReadOnly = false;
                btnEditSave.Text = "Save";
                btnCloseCancel.Text = "Cancel";
                editModeEnabled = true;
                if (txtNodeXML.Text == "<null>")
                {
                    txtNodeXML.Text = "";
                }
                txtNodeXML.SelectionStart = 0;
                txtNodeXML.Focus();
            }
            else
            {
                nodeEdited = true;
                newNodeText = txtNodeXML.Text;
                this.Hide();
            }
        }
    }
}
