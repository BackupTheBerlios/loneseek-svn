using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Sent to us when a user joined the room.
    /// </summary>
    public class UserJoinedReply : Packet
    {
        public UserJoinedReply()
        {
            this.type = PacketType.UserJoinedRoom;
            this.direction = PacketDirection.ServerToClient;

            AddString();    // Room name
            AddString();    // User who has joined.
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
