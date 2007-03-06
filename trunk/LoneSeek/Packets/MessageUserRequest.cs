using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Whenever we wish to send a private message to a user.
    /// </summary>
    public class MessageUserRequest : Packet
    {
        public MessageUserRequest()
        {
            this.type = PacketType.MessageUser;
            this.direction = PacketDirection.ClientToServer;

            AddString();        // Username.
            AddString();        // Message.
        }

        /// <summary>
        /// Sets or retrieves the user to send the message to.
        /// </summary>
        public String User
        {
            get { return data[0] as String; }
            set { data[0] = value; }
        }

        /// <summary>
        /// Sets or retrieves the message to send.
        /// </summary>
        public String Message
        {
            get { return data[0] as String; }
            set { data[0] = value; }
        }
    }
}
