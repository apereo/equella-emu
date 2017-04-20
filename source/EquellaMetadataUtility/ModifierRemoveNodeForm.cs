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
    public partial class ModifierRemoveNodeForm : Form
    {
        public ModifierRemoveNode modifier;
        public bool okPressed = false;

        public ModifierRemoveNodeForm()
        {
            InitializeComponent();
            ModifierRemoveNodeForm_Resize();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtXpath.Text != "")
            {
                // set public members for MainForm to access and hide form (MainForm will close it)
                modifier.xpath = txtXpath.Text;
                modifier.Enabled = chkEnabled.Checked;
                okPressed = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show("modifier Error: xpath cannot lbe left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ModifierRemoveNodeForm_Load(object sender, EventArgs e)
        {
            if (modifier != null)
            {
                txtXpath.Text = modifier.xpath;
                chkEnabled.Checked = modifier.Enabled;
            }
        }

        private void ModifierRemoveNodeForm_Resize(object sender, EventArgs e)
        {
            ModifierRemoveNodeForm_Resize();
        }

        private void ModifierRemoveNodeForm_Resize()
        {
            int buttonWidth = 16;

            txtXpath.Width = this.ClientSize.Width - buttonWidth;
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
                modifier.xpath = txtXpath.Text;

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
