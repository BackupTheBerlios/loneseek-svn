using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Send when we leave a room.
    /// </summary>
    public class LeaveRoomRequest : Packet
    {
        public LeaveRoomRequest()
        {
            this.type = PacketType.LeaveRoom;
            this.direction = PacketDirection.ClientToServer;

            AddString();        // Room we wish to leave.
        }

        /// <summary>
        /// Retrieves or sets the room we wish to leave.
        /// </summary>
        public String Room
        {
            get { return data[0] as String; }
            set { data[0] = value; }
        }
    }
}
