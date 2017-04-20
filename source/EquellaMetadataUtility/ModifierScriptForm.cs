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
    public partial class ModifierScriptForm : Form
    {
        public bool okPressed = false;
        public ModifierScript modifier;

        private bool initialLoad;

        public ModifierScriptForm()
        {
            InitializeComponent();
            ModifierScriptForm_Resize();
        }

        private void ModifierScriptForm_Resize(object sender, EventArgs e)
        {
            ModifierScriptForm_Resize();
        }

        private void ModifierScriptForm_Resize()
        {
            int buttonWidth = 16;

            fctbScript.Height = this.ClientSize.Height - fctbScript.Top - btnOk.Height - buttonWidth;
            fctbScript.Width = this.ClientSize.Width;
            btnOk.Top = this.ClientSize.Height - btnOk.Height - buttonWidth / 2;
            btnCancel.Top = btnOk.Top;
            btnCancel.Left = this.ClientSize.Width - btnCancel.Width - buttonWidth / 2;
            btnOk.Left = btnCancel.Left - btnOk.Width - buttonWidth / 2;
            btnValidate.Top = btnOk.Top;
            chkEnabled.Top = btnOk.Top + buttonWidth / 4;
            chkRunOnlyOnce.Top = chkEnabled.Top;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // set public members for MainForm to access and hide form (MainForm will close it)
            modifier.ScriptText = fctbScript.Text;
            modifier.runOnlyOnce = chkRunOnlyOnce.Checked;
            modifier.Enabled = chkEnabled.Checked;
            if (!modifier.getCompiledState())
            {
                try
                {
                    modifier.validate();
                    modifier.setCompiledState(true);
                    modifier.valid = true;
                }
                catch (Exception err)
                {
                    modifier.setCompiledState(false);
                    modifier.valid = false;
                }
            }
            okPressed = true;
            this.Hide();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            string uncommittedScriptText = modifier.ScriptText;
            try
            {
                // validate modifier
                
                modifier.ScriptText = fctbScript.Text;
                modifier.validate();
                modifier.setCompiledState(true);
                modifier.valid = true;
                modifier.ScriptText = uncommittedScriptText;
                MessageBox.Show("Script compiles successfully.", "Syntax Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                modifier.ScriptText = uncommittedScriptText;
                modifier.setCompiledState(false);
                modifier.valid = false;
                string innerMessage = "";
                if (err.InnerException != null)
                {
                    innerMessage = err.InnerException.Message;
                }
                MessageBox.Show("Modifier Error: " + err.Message + "\n\n" + innerMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModifierScriptForm_Load(object sender, EventArgs e)
        {
            if (modifier != null)
            {
                initialLoad = true;
                fctbScript.Text = modifier.ScriptText.Replace("\r\n", "\n").Replace("\n", "\r\n");
                initialLoad = false;
                chkRunOnlyOnce.Checked = modifier.runOnlyOnce;
                chkEnabled.Checked = modifier.Enabled;
            }
        }

        private void btnFindReplace_Click(object sender, EventArgs e)
        {
            fctbScript.ShowReplaceDialog();
        }

        private void fctbScript_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!initialLoad)
            {
                modifier.setCompiledState(false);
                modifier.valid = false;
            }
        }
    }
}
