using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Sent to us to read the roomlist.
    /// </summary>
    public class RoomListReply : Packet
    {
        public RoomListReply()
        {
            this.type = PacketType.RoomList;
            this.direction = PacketDirection.ServerToClient;

            AddStringArray();
            AddIntArray();
        }

        /// <summary>
        /// Returns a list of room names.
        /// </summary>
        public String[] Rooms
        {
            get { return data[0] as String[]; }
        }

        /// <summary>
        /// Returns a list of user counts.
        /// </summary>
        public Int32[] UserCounts
        {
            get { return data[1] as Int32[]; }
        }
    }
}
