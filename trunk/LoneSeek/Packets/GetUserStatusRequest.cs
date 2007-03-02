using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Send when we which to request the status of a user.
    /// </summary>
    class GetUserStatusRequest : Packet
    {
        public GetUserStatusRequest()
        {
            this.type = PacketType.GetUserStatus;
            this.direction = PacketDirection.ClientToServer;

            AddString();        // The user we are requesting the status of.
        }

        /// <summary>
        /// Retrieves or sets the username we query.
        /// </summary>
        public String User
        {
            get { return data[0] as String; }
            set { data[0] = value; }
        }
    }
}
