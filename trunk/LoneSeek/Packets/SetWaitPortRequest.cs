using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Send whenever the client wants to tell the server his port.
    /// </summary>
    public class SetWaitPortRequest : Packet
    {
        /// <summary>
        /// Constructs a new empty SetWaitPortPacket()
        /// </summary>
        public SetWaitPortRequest()
        {
            this.type = PacketType.SetWaitPort;
            this.direction = PacketDirection.ClientToServer;

            AddInt();   // Port
        }

        /// <summary>
        /// Retrieves the or sets the port to set.
        /// </summary>
        public Int32 Port
        {
            get { return (Int32)data[0]; }
            set { data[0] = value; }
        }
    }
}
