using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Sent to join a specified room.
    /// </summary>
    class JoinRoomRequest : Packet
    {
        public JoinRoomRequest()
        {
            this.type = PacketType.JoinRoom;
            this.direction = PacketDirection.ClientToServer;

            AddString();        // Room to join
        }

        /// <summary>
        /// Sets or retrieves the room to join.
        /// </summary>
        public String Room
        {
            get { return data[0] as String; }
            set { data[0] = value; }
        }
    }
}
