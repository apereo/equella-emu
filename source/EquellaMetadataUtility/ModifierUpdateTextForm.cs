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
    public partial class ModifierUpdateTextForm : Form
    {
        public bool okPressed = false;
        public ModifierUpdateText modifier;

        public ModifierUpdateTextForm()
        {
            InitializeComponent();
            ModifierUpdateTextForm_Resize();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtXpath.Text != "")
            {
                // set public members for MainForm to access and hide form (MainForm will close it)
                modifier.xpath = txtXpath.Text;
                modifier.createNode = chkCreateNode.Checked;
                modifier.updateText = txtUpdateText.Text;
                modifier.Enabled = chkEnabled.Checked;
                okPressed = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Modifier Error: xpath cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ModifierUpdateTextForm_Load(object sender, EventArgs e)
        {
            if (modifier != null)
            {
                txtXpath.Text = modifier.xpath;
                chkCreateNode.Checked = modifier.createNode;
                txtUpdateText.Text = modifier.updateText.Replace("\r\n", "\n").Replace("\n", "\r\n");
                chkEnabled.Checked = modifier.Enabled;
            }
        }

        private void ModifierUpdateTextForm_Resize(object sender, EventArgs e)
        {
            ModifierUpdateTextForm_Resize();
        }

        private void ModifierUpdateTextForm_Resize()
        {
            int buttonWidth = 16;

            txtUpdateText.Height = this.ClientSize.Height - txtUpdateText.Top - btnOk.Height - buttonWidth;
            txtUpdateText.Width = this.ClientSize.Width;
            txtXpath.Width = this.ClientSize.Width - txtXpath.Left - buttonWidth / 2;

            btnOk.Top = this.ClientSize.Height - btnOk.Height - buttonWidth / 2;
            btnCancel.Top = btnOk.Top;
            btnCancel.Left = this.ClientSize.Width - btnCancel.Width - buttonWidth / 2;
            btnOk.Left = btnCancel.Left - btnOk.Width - buttonWidth / 2;
            btnValidate.Top = btnOk.Top;
            chkEnabled.Top = btnOk.Top + buttonWidth / 4;
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                // validate modifier
                modifier.xpath = txtXpath.Text;
                modifier.createNode = chkCreateNode.Checked;
                modifier.updateText = txtUpdateText.Text;
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
