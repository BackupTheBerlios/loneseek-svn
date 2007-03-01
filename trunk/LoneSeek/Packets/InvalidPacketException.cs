using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Thrown the the system encounters an invalid packet.
    /// </summary>
    public class InvalidPacketException : ApplicationException
    {
        public InvalidPacketException()
        {
        }

        public InvalidPacketException(string message)
            : base(message)
        {
        }
    }
}
