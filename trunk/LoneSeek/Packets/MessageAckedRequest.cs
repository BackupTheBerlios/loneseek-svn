using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Send when we a ack a private message.
    /// </summary>
    public class MessageAckedRequest : Packet
    {
        public MessageAckedRequest()
        {
            this.type = PacketType.MessageAck;
            this.direction = PacketDirection.ClientToServer;

            AddInt();       // Message id
        }

        /// <summary>
        /// Sets or retrieves the message id to ack.
        /// </summary>
        public Int32 MessageId
        {
            get { return (Int32)data[0]; }
            set { data[0] = value; }
        }
    }
}
