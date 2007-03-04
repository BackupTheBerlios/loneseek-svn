using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LoneChat
{
    public partial class Connections : Form
    {
        private TextBox editBox = new TextBox();

        public Connections()
        {
            InitializeComponent();
            lsConnections.DoubleClickActivation = true;
        }

        public void UpdateList()
        {
            lsConnections.Items.Clear();
            // Add new items.
            foreach (Connection connection in Configuration.Instance.Connections)
            {
                ListViewItem item = new ListViewItem(connection.Name);

                item.SubItems.Add(connection.Host);
                item.SubItems.Add(connection.Port.ToString());
                item.SubItems.Add(connection.Username);
                item.Tag = connection;
                // Edit 
                lsConnections.Items.Add(item);
            }
        }

        private void Connections_Load(object sender, EventArgs e)
        {
            editBox.Hide();
            editBox.Parent = this;
            UpdateList();
        }

        private void bnAdd_Click(object sender, EventArgs e)
        {
            Connection connection = new Connection();

            connection.Name = "New Connection";
            Configuration.Instance.Connections.Add(connection);
            UpdateList();
        }

        private void lsConnections_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            
        }

        private void lsConnections_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            try
            {
                Connection con = e.Item.Tag as Connection;

                if (e.SubItem == 0)
                {
                    con.Name = e.DisplayText;
                }
                if (e.SubItem == 1)
                {
                    con.Host = e.DisplayText;
                }
                else if (e.SubItem == 2)
                {
                    con.Port = Int32.Parse(e.DisplayText);
                }
                else if (e.SubItem == 3)
                {
                    con.Username = e.DisplayText;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The information you provided is not valid. Notice: Port only accept numbers.");
                e.Cancel = true;
            }
        }

        private void bnRem_Click(object sender, EventArgs e)
        {
            int selected = 0;

            if (lsConnections.SelectedIndices.Count > 0)
            {
                selected = lsConnections.SelectedIndices[0];
                Configuration.Instance.Connections.RemoveAt(selected);
                UpdateList();
            }
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            // Save configuration.
            Configuration.Save();
            Close();
        }

        private void lsConnections_SubItemClicked(object sender, SubItemEventArgs e)
        {            
            // Start editing.
            lsConnections.StartEditing(editBox, e.Item, e.SubItem);
        }
    }
}