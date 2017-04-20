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
    public partial class ModifierAddXMLForm : Form
    {
        public bool okPressed = false;
        public ModifierAddXML modifier;

        public ModifierAddXMLForm()
        {
            InitializeComponent();
            ModifierAddXMLForm_Resize();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // set public members for MainForm to access and hide form (MainForm will close it)
            if (txtXpath.Text != "")
            {
                modifier.xpath = txtXpath.Text;
                modifier.addXML = fctbAddXML.Text;
                modifier.createNode = chkCreateNode.Checked;
                modifier.Enabled = chkEnabled.Checked;
                okPressed = true;
                this.Hide();
            }
            else
                MessageBox.Show("Modifier Error: Cannot leave xpath empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ModifierAddXMLForm_Load(object sender, EventArgs e)
        {
            if (modifier != null)
            {
                txtXpath.Text = modifier.xpath;
                chkCreateNode.Checked = modifier.createNode;
                fctbAddXML.Text = modifier.addXML.Replace("\r\n", "\n").Replace("\n", "\r\n");
                chkEnabled.Checked = modifier.Enabled;
            }
        }

        private void ModifierAddXMLForm_Resize(object sender, EventArgs e)
        {
            ModifierAddXMLForm_Resize();
        }

        private void ModifierAddXMLForm_Resize()
        {
            int buttonWidth = 16;

            fctbAddXML.Height = this.ClientSize.Height - fctbAddXML.Top - btnOk.Height - buttonWidth;
            fctbAddXML.Width = this.ClientSize.Width;
            txtXpath.Width = this.Width - 60;
            btnOk.Top = this.ClientSize.Height - btnOk.Height - buttonWidth / 2;
            btnCancel.Top = btnOk.Top;
            btnCancel.Left = this.ClientSize.Width - btnCancel.Width - buttonWidth / 2;
            btnOk.Left = btnCancel.Left - btnOk.Width - buttonWidth / 2;
            btnTest.Top = btnOk.Top;
            chkEnabled.Top = btnOk.Top + buttonWidth / 4;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                modifier.xpath = txtXpath.Text;
                modifier.createNode = chkCreateNode.Checked;
                modifier.addXML = fctbAddXML.Text;

                // validate modifier
                string message = modifier.validate();
                MessageBox.Show(message, "Syntax Check", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception err)
            {
                MessageBox.Show("Modifier Error: " + err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
