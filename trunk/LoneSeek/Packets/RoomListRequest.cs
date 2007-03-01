using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Used to query the user list.
    /// </summary>
    public class RoomListRequest : Packet
    {
        public RoomListRequest()
        {
            this.type = PacketType.RoomList;
            this.Direction = PacketDirection.ClientToServer;
            // That's it folks.
        }
    }
}
