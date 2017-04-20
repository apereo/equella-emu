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
    public partial class ModifierCopyXMLForm : Form
    {
        public ModifierCopyXML modifier;
        public bool okPressed = false;

        public ModifierCopyXMLForm()
        {
            InitializeComponent();
            ModifierCopyXMLForm_Resize();
        }

        private void ModifierCopyXMLForm_Resize(object sender, EventArgs e)
        {
            ModifierCopyXMLForm_Resize();
        }

        private void ModifierCopyXMLForm_Resize()
        {
            int buttonWidth = 16;

            txtSourceNode.Width = this.ClientSize.Width - buttonWidth;
            txtTargetXpath.Width = this.ClientSize.Width - buttonWidth;

            btnOk.Top = this.ClientSize.Height - btnOk.Height - buttonWidth / 2;
            btnCancel.Top = btnOk.Top;
            btnCancel.Left = this.ClientSize.Width - btnCancel.Width - buttonWidth / 2;
            btnOk.Left = btnCancel.Left - btnOk.Width - buttonWidth / 2;
            btnValidate.Top = btnOk.Top;
            chkEnabled.Top = btnOk.Top + buttonWidth / 4;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtSourceNode.Text != "" && txtTargetXpath.Text != "")
            {
                // set public members for MainForm to access and hide form (MainForm will close it)
                modifier.sourceNode = txtSourceNode.Text;
                modifier.targetXpath = txtTargetXpath.Text;
                modifier.createNode = chkCreateNode.Checked;
                modifier.Enabled = chkEnabled.Checked;
                okPressed = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show("modifier Error: Source Node and Target XPath cannot be left blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ModifierCopyXMLForm_Load(object sender, EventArgs e)
        {
            if (modifier != null)
            {
                txtSourceNode.Text = modifier.sourceNode;
                txtTargetXpath.Text = modifier.targetXpath;
                chkCreateNode.Checked = modifier.createNode;
                chkEnabled.Checked = modifier.Enabled;
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                modifier.sourceNode = txtSourceNode.Text;
                modifier.targetXpath = txtTargetXpath.Text;
                modifier.createNode = chkCreateNode.Checked;

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

