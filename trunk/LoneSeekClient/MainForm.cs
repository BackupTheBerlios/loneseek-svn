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
        private ChatRoom joined = null;

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
                client.OnRoomJoined += new LoneSeekRoomEvent(client_OnRoomJoined);
                client.OnChatMessage += new LoneSeekChatMessageEvent(client_OnChatMessage);
                client.OnRoomLeft += new LoneSeekRoomEvent(client_OnRoomLeft);
                // Only share my avi files.
                client.FileIndexer.AllowedExtensions.Add("avi");
                // Share my southpark folder
                client.FileIndexer.Add("d:\\media\\");
                // Specify incoming port
                client.IncomingPort = 14447;
                // Connect to the soulseek stuff
                client.Connect("sk6.slsknet.org", 2240);
                // Login as loneseek.
                client.Username = "loneseek";
                client.Password = "loneseek";
                // Login now
                client.Login();
            }
        }

        void OnRoomLeft(object sender, ChatRoom room)
        {
            txText.AppendText("Left room: " + room.Name + "\r\n");
            txText.ScrollToCaret();
        }

        void client_OnRoomLeft(object sender, ChatRoom room)
        {
            this.Invoke(new LoneSeekRoomEvent(OnRoomLeft), sender, room);
        }

        void OnChatMessage(object sender, ChatMessage message, ChatRoom room)
        {
            txText.AppendText("<" + message.Sender + "@" + message.Room + "> " + message.Message + "\r\n");
            txText.ScrollToCaret();
        }

        void client_OnChatMessage(object sender, ChatMessage message, ChatRoom room)
        {
            this.Invoke(new LoneSeekChatMessageEvent(OnChatMessage), sender, message, room);
        }

        void OnRoomJoined(object sender, ChatRoom room)
        {
            lsUsers.Items.Clear();

            foreach (User user in room.Users)
            {
                lsUsers.Items.Add(user.Name);
            }
            txText.AppendText("Joined room " + room.Name + "\r\n");
            txText.AppendText("Users in the room " + room.UserCount.ToString() + "\r\n");
            txText.ScrollToCaret();
        }

        void client_OnRoomJoined(object sender, ChatRoom room)
        {
            this.Invoke(new LoneSeekRoomEvent(OnRoomJoined), sender, room);   
        }

        void OnRoomListUpdated()
        {
            lvRooms.Items.Clear();

            foreach (ChatRoom room in client.ChatRooms)
            {
                ListViewItem item = new ListViewItem(room.Name);
                item.SubItems.Add(room.UserCount.ToString());
                item.Tag = room;
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
            txText.AppendText("Login successful: " + client.LoggedIn + "\r\n");
            if (client.LoggedIn)
            {
                txText.AppendText(client.WelcomeMessage);
                txText.AppendText("Your IP: " + client.PublicAddress + "\r\n");
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

        private void lvRooms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selected = 0;

            if (lvRooms.SelectedIndices.Count > 0)
            { // Join this room.
                selected = lvRooms.SelectedIndices[0];
                joined = lvRooms.Items[selected].Tag as ChatRoom;
                // Join room.
                joined.Join();
            }
        }

        private void lvRooms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bnLeave_Click(object sender, EventArgs e)
        {
            if (joined != null)
            { // Leave joined chat room.
                joined.Leave();
            }
        }

    }
}