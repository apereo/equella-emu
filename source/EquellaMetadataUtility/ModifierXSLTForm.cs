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
    public partial class ModifierXSLTForm : Form
    {
        public bool okPressed = false;
        public ModifierXSLT modifier;

        public ModifierXSLTForm()
        {
            InitializeComponent();
            ModifierXSLTForm_Resize();
        }

        private void ModifierXSLTForm_Resize(object sender, EventArgs e)
        {
            ModifierXSLTForm_Resize();
        }

        private void ModifierXSLTForm_Resize()
        {
            int buttonWidth = 16;

            fctbXSLT.Height = this.ClientSize.Height - fctbXSLT.Top - btnOk.Height - buttonWidth;
            fctbXSLT.Width = this.ClientSize.Width;

            btnOk.Top = this.ClientSize.Height - btnOk.Height - buttonWidth / 2;
            btnCancel.Top = btnOk.Top;
            btnCancel.Left = this.ClientSize.Width - btnCancel.Width - buttonWidth / 2;
            btnOk.Left = btnCancel.Left - btnOk.Width - buttonWidth / 2;
            btnValidate.Top = btnOk.Top;
            chkEnabled.Top = btnOk.Top + buttonWidth / 4;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (fctbXSLT.Text.Trim() != "")
            {
                // set public members for MainForm to access and hide form (MainForm will close it)
                modifier.XSLTtext = fctbXSLT.Text;
                modifier.Enabled = chkEnabled.Checked;
                okPressed = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Modifier Error: xpath cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                // validate modifier
                ModifierXSLT testModifier = new ModifierXSLT(fctbXSLT.Text);
                testModifier.validate();
                MessageBox.Show("Modifier is correctly formatted.", "Syntax Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                string innerMessage = "";
                if (err.InnerException != null)
                {
                    innerMessage = err.InnerException.Message;
                }
                MessageBox.Show("Modifier Error: " + err.Message + "\n\n" + innerMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModifierXSLTForm_Load(object sender, EventArgs e)
        {
            if (modifier != null)
            {
                fctbXSLT.Text = modifier.XSLTtext.Replace("\r\n", "\n").Replace("\n", "\r\n");
                fctbXSLT.SelectionStart = 0;
                chkEnabled.Checked = modifier.Enabled;
            }
        }

        private void btnFindReplace_Click(object sender, EventArgs e)
        {
            fctbXSLT.ShowReplaceDialog();
        }
    }
}
