using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Resembles a login packet.
    /// </summary>
    public class LoginRequest : Packet
    {
        public LoginRequest()
        {
            AddString(); // Username
            AddString(); // Password
            AddInt();    // Version
            AddString(); // Hash value
            AddInt();    // ?

            data[2] = (Int32)SoulSeekVersion.Latest; 
            data[4] = 1;    // Setting the unkown thingee to 1
                            // is a good starting point.
            // Set the type
            type = PacketType.Login;
            // Direction.
            direction = PacketDirection.ClientToServer;
        }

        /// <summary>
        /// Updates the hash. Should be called when Username and/or
        /// Password changed.
        /// </summary>
        private void UpdateHash()
        {
            data[3] = Hash.CalculateHash(Username + Password);
        }

        /// <summary>
        /// Sets or retrieves the username to login.
        /// </summary>
        public string Username
        {
            get { return data[0] as String; }
            set 
            {   
                data[0] = value;
                UpdateHash();
            }
        }

        /// <summary>
        /// Password to use when to login.
        /// </summary>
        public string Password
        {
            get { return data[1] as String; }
            set 
            { 
                data[1] = value;
                UpdateHash();
            }
        }
    }
}
