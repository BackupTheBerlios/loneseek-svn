using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LoneChat
{
    public partial class LoneChat : Form
    {
        private Dictionary<String, TabPage> conTabs = new Dictionary<String, TabPage>();

        public LoneChat()
        {
            InitializeComponent();
        }

        private void mnuNewConnection_Click(object sender, EventArgs e)
        {
            Connections connections = new Connections();
            // Show modal dialog.
            connections.ShowDialog(this);
        }

        private void mnuFile_DropDownOpening(object sender, EventArgs e)
        {
            // Clear old items.
            mnuConnectTo.DropDownItems.Clear();
            foreach (Connection connection in Configuration.Instance.Connections)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(connection.Name);

                item.Click += new EventHandler(item_Click);
                item.Tag = connection;
                mnuConnectTo.DropDownItems.Add(item);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Connection connection = item.Tag as Connection;
            // Connect to it.
            if (conTabs.ContainsKey(connection.Name))
            {
                TabPage page = conTabs[connection.Name];
                // Select this tab page.
                page.Select();
            }
            else
            {
                TabPage page = new TabPage("Server: " +connection.Name);
                ServerConnection control = new ServerConnection();

                control.Dock = DockStyle.Fill;
                page.Controls.Add(control);
                // Add a new ServerConnection control to it.
                tbTabs.TabPages.Add(page);
            }
        }

        private void mnuQuit_Click(object sender, EventArgs e)
        {
            try
            {
                // Save configuration.
                Configuration.Save();
            }
            catch ( Exception )
            {
            }
            Application.Exit();
        }
    }
}