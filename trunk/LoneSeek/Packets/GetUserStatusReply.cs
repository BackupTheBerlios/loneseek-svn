using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Reply of the GetUserStatus request packet.
    /// </summary>
    class GetUserStatusReply : Packet
    {
        public GetUserStatusReply()
        {
            this.type = PacketType.GetUserStatus;
            this.direction = PacketDirection.ServerToClient;

            AddString();        // Username this status belongs to.
            AddInt();           // Userstatus
        }

        /// <summary>
        /// Retrieves the username this status belongs to.
        /// </summary>
        public String User
        {
            get { return data[0] as String; }
        }

        /// <summary>
        /// Retrieves the status of the user.
        /// </summary>
        public UserStatus Status
        {
            get { return (UserStatus)data[1]; }
        }
    }
}
