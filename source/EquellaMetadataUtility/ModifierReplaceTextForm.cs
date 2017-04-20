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
    public partial class ModifierReplaceTextForm : Form
    {
        public bool okPressed = false;
        public ModifierReplaceText modifier;

        public ModifierReplaceTextForm()
        {
            InitializeComponent();
            ModifierReplaceTextForm_Resize();
        }

        private void ModifierReplaceTextForm_Resize(object sender, EventArgs e)
        {
            ModifierReplaceTextForm_Resize();
        }

        private void ModifierReplaceTextForm_Resize()
        {
            int buttonWidth = 16;

            txtXpath.Width = this.ClientSize.Width - txtXpath.Left - buttonWidth / 2;
            txtFind.Width = txtXpath.Width;
            txtReplaceWith.Width = txtXpath.Width;

            txtFind.Height = (this.ClientSize.Height - txtXpath.Height - btnOk.Height) / 2 - buttonWidth * 2;
            txtReplaceWith.Top = this.ClientSize.Height / 2;
            label3.Top = txtReplaceWith.Top + 3;
            txtReplaceWith.Height = txtFind.Height;

            btnOk.Top = this.ClientSize.Height - btnOk.Height - buttonWidth / 2;
            btnCancel.Top = btnOk.Top;
            btnCancel.Left = this.ClientSize.Width - btnCancel.Width - buttonWidth / 2;
            btnOk.Left = btnCancel.Left - btnOk.Width - buttonWidth / 2;
            btnValidate.Top = btnOk.Top;
            chkEnabled.Top = btnOk.Top + buttonWidth / 4;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtXpath.Text != "")
            {
                // set public members for MainForm to access and hide form (MainForm will close it)
                modifier.xpath = txtXpath.Text;
                modifier.findText = txtFind.Text;
                modifier.replaceWithText = txtReplaceWith.Text;
                modifier.Enabled = chkEnabled.Checked;
                okPressed = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show("modifier Error: XPath cannot be left blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ModifierReplaceTextForm_Load(object sender, EventArgs e)
        {
            if (modifier != null)
            {
                txtXpath.Text = modifier.xpath;
                txtFind.Text = modifier.findText;
                txtReplaceWith.Text = modifier.replaceWithText;
                chkEnabled.Checked = modifier.Enabled;
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                modifier.xpath = txtXpath.Text;
                modifier.findText = txtFind.Text;
                modifier.replaceWithText = txtReplaceWith.Text;

                // validate modifier
                string message = modifier.validate();
                MessageBox.Show(message, "Syntax Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBox.Show("modifier Error: " + err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
