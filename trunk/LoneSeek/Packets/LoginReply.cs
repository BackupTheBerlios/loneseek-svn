using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Sent by the server when we have successfuly logged in.
    /// </summary>
    public class LoginReply : Packet
    {
        private IPAddress address = null;

        public LoginReply()
        {
            this.type = PacketType.Login;
            this.direction = PacketDirection.ServerToClient;

            AddByte();   // Login successful?.
            AddString(); // Welcome message or error message?
            AddInt();    // My public IP
            AddString(); // Hash
        }

        /// <summary>
        /// Returns true if the login was successful or not.
        /// </summary>
        public Boolean Successful
        {
            get { return Convert.ToBoolean(data[0]); }
        }

        /// <summary>
        /// Returns the welcome message.
        /// </summary>
        public String WelcomeMessage
        {
            get { return data[1] as String; }
        }

        /// <summary>
        /// Returns the public IP address of ourself.
        /// </summary>
        public IPAddress IPAddress
        {
            get
            {
                if (address == null)
                { // Get a new IP address: It is sent in little endian.
                    UInt32 addr = Convert.ToUInt32(data[2]);
                    addr = Helper.ToBigEndian(addr);
                    address = new IPAddress(addr);
                }
                return address;
            }
        }
    }
}
