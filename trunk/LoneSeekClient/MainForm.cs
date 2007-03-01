using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LoneSeek;
using LoneSeek.Packets;

namespace LoneSeekGUI
{
    public partial class MainForm : Form
    {
        private delegate void OnEvent();
        private LoneSeekClient client = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void bnConnect_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                client = new LoneSeekClient();
                client.OnLogin += new LoneSeekEvent(client_OnLogin);
                client.OnRoomListUpdated += new LoneSeekEvent(client_OnRoomListUpdated);
                // Only share my avi files.
                client.FileIndexer.AllowedExtensions.Add("avi");
                // Share my southpark folder
                client.FileIndexer.Add("d:\\southpark");
                // Specify incoming port
                client.IncomingPort = 14446;
                // Connect to the soulseek stuff
                client.Connect("sk6.slsknet.org", 2240);
                // Login as loneseek.
                client.Username = "loneseek";
                client.Password = "loneseek";
                // Login now
                client.Login();
            }
        }

        void OnRoomListUpdated()
        {
            lvRooms.Items.Clear();

            foreach (ChatRoom room in client.ChatRooms)
            {
                ListViewItem item = new ListViewItem(room.Name);
                item.SubItems.Add(room.UserCount.ToString());
                // Add item
                lvRooms.Items.Add(item);
            }
        }

        void client_OnRoomListUpdated(object sender)
        {
            this.Invoke(new OnEvent(OnRoomListUpdated));   
        }

        void OnLogin()
        {
            txText.AppendText("Login successful: " + client.LoggedIn);
            if (client.LoggedIn)
            {
                txText.AppendText(client.WelcomeMessage);
                txText.AppendText("\r\nYour IP: " + client.PublicAddress);
            }
        }

        void client_OnLogin(object sender)
        {
            txText.Invoke(new OnEvent(OnLogin));
        }

        private void bnDisconnect_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.Disconnect();
                client = null;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}