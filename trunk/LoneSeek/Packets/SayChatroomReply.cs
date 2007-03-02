using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Sent to us to notify us about a message.
    /// </summary>
    public class SayChatroomReply : Packet
    {
        public SayChatroomReply()
        {
            this.type = PacketType.SayChatRoom;
            this.direction = PacketDirection.ServerToClient;

            AddString();        // Room we are in.
            AddString();        // The user who has sent this message
            AddString();        // The message.
        }

        /// <summary>
        /// The room.
        /// </summary>
        public String Room
        {
            get { return data[0] as String; }
        }

        /// <summary>
        /// The user who has sent this message.
        /// </summary>
        public String User
        {
            get { return data[1] as String; }
        }

        /// <summary>
        /// The message.
        /// </summary>
        public String Message
        {
            get { return data[2] as String; }
        }
    }
}
