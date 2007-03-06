using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// 
    /// </summary>
    public class UserLeftReply: Packet
    {
        public UserLeftReply()
        {
            this.type = PacketType.UserLeftRoom;
            this.direction = PacketDirection.ServerToClient;

            AddString();        // Room 
            AddString();        // User
        }

        /// <summary>
        /// Retrieves the room the user joined.
        /// </summary>
        public String Room
        {
            get { return data[0] as String; }
        }

        /// <summary>
        /// Retrieves the user who has joined the room.
        /// </summary>
        public String User
        {
            get { return data[1] as String; }
        }
    }
}
