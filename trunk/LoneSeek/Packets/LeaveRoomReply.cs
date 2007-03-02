using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Retrieved then we are out of a room.
    /// </summary>
    public class LeaveRoomReply : Packet
    {
        public LeaveRoomReply()
        {
            this.type = PacketType.LeaveRoom;
            this.direction = PacketDirection.ServerToClient;

            AddString();    // Room we have left.
        }

        /// <summary>
        /// Retrieves the room we have left.
        /// </summary>
        public String Room
        {
            get { return data[0] as String; }
        }
    }
}
