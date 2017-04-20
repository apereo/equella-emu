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
    public partial class ModifierRenameNodeForm : Form
    {
        public ModifierRenameNode modifier;
        public bool okPressed = false;

        public ModifierRenameNodeForm()
        {
            InitializeComponent();
            ModifierRenameNodeForm_Resize();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(txtCurrentXpath.Text != "")
            {
                // set public members for MainForm to access and hide form (MainForm will close it)
                modifier.currentXpath = txtCurrentXpath.Text;
                modifier.renamedNode = txtRenamedXpath.Text;
                modifier.Enabled = chkEnabled.Checked;
                okPressed = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show("modifier Error: xpath cannot be left blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ModifierRenameNodeForm_Load(object sender, EventArgs e)
        {
            if (modifier != null)
            {
                txtCurrentXpath.Text = modifier.currentXpath;
                txtRenamedXpath.Text = modifier.renamedNode;
                chkEnabled.Checked = modifier.Enabled;
            }
        }

        private void ModifierRenameNodeForm_Resize(object sender, EventArgs e)
        {
            ModifierRenameNodeForm_Resize();
        }

        private void ModifierRenameNodeForm_Resize()
        {
            int buttonWidth = 16;

            txtCurrentXpath.Width = this.ClientSize.Width - buttonWidth;
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
                modifier.currentXpath = txtCurrentXpath.Text;
                modifier.renamedNode = txtRenamedXpath.Text;

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
