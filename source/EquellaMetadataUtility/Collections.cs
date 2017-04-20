using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace EquellaMetadataUtility
{
    public partial class Collections : Form
    {
        public Collections()
        {
            InitializeComponent();
        }

        public EquellaClient equellaClient;
        public bool okPressed = false;
        public ArrayList selectedCollections = new ArrayList();

        private void Collections_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XmlDocument xmlCollections = new XmlDocument();

                // get list of contributable collections
                xmlCollections.LoadXml(equellaClient.getContributableCollections());

                int contributableCollectionsCount = xmlCollections.ChildNodes[0].ChildNodes.Count;
                for (int i = 0; i < contributableCollectionsCount; i++)
                {
                    // CollectionItem collectionItem = new CollectionItem(xmlCollections.ChildNodes[0].ChildNodes[i].SelectSingleNode("id").InnerXml, xmlCollections.ChildNodes[0].ChildNodes[i].SelectSingleNode("name").InnerXml);
                    lstCollections.Items.Add(xmlCollections.ChildNodes[0].ChildNodes[i].SelectSingleNode("name").InnerXml);
                }
                equellaClient.logout();
                this.Cursor = Cursors.Arrow;
            }
            catch(Exception err)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btOk_Click(object sender, EventArgs e)
        {
            okPressed = true;
            // place selected list box in a public member for access by main form
            for (int i = 0; i < lstCollections.SelectedItems.Count; i++)
            {
                selectedCollections.Add(lstCollections.SelectedItems[i]);
            }
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

}
