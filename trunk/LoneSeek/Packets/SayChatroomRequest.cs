using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Used to send a message to the others.
    /// </summary>
    public class SayChatroomRequest : Packet
    {
        public SayChatroomRequest()
        {
            this.type = PacketType.SayChatRoom;
            this.direction = PacketDirection.ClientToServer;

            AddString();        // Chatroom we are in
            AddString();        // Message we would like to send.
        }

        /// <summary>
        /// Sets or retrieves the room we are sending the message to.
        /// </summary>
        public String Room
        {
            get { return data[0] as String; }
            set { data[0] = value; }
        }

        /// <summary>
        /// Sets or retrieves the message to send.
        /// </summary>
        public String Message
        {
            get { return data[1] as String; }
            set { data[1] = value; }
        }
    }
}
