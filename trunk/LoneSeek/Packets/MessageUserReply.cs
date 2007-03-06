using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Retrieved whenever someone sent us a private message.
    /// </summary>
    public class MessageUserReply : Packet
    {
        public MessageUserReply()
        {
            this.type = PacketType.MessageUser;
            this.direction = PacketDirection.ServerToClient;

            AddInt();       // Message ID
            AddInt();       // Time stamp
            AddString();    // Username
            AddString();    // Message
        }

        /// <summary>
        /// Retrieves the id of the message.
        /// </summary>
        public Int32 MessageId
        {
            get { return (Int32)data[0]; }
        }

        /// <summary>
        /// Retrieves the time the message was sent.
        /// </summary>
        public DateTime Time
        {
            get
            {
                DateTime time = DateTime.FromFileTime((Int32)data[1]);
                return time;
            }
        }

        /// <summary>
        /// Sets or retrieves the user to send the message to.
        /// </summary>
        public String User
        {
            get { return data[2] as String; }
        }

        /// <summary>
        /// Sets or retrieves the message to send.
        /// </summary>
        public String Message
        {
            get { return data[3] as String; }
        }
    }
}
