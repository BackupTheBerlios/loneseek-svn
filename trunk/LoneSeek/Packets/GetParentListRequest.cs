using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Request the parent list. Whatever that may be.
    /// </summary>
    public class GetParentListRequest : Packet
    {
        public GetParentListRequest()
        {
            this.type = PacketType.GetParentList;
            this.direction = PacketDirection.ClientToServer;

            AddByte();          // Unkown.
            // Unkown what to do with that byte.
            data[0] = (Byte)1;
        }

        /// <summary>
        /// Subject to change.
        /// </summary>
        [NotImplemented]
        public Byte Unkown
        {
            get { return (Byte)data[0]; }
            set { data[0] = value; }
        }
    }
}
