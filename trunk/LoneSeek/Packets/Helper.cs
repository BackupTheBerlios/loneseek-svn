using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    public class Helper
    {
        /// <summary>
        /// Converts the given unsigned int to big endian. If the host machine is big endian no convertion is being performed.
        /// <summary>
        /// <param name="source">The unsigned int to convert.</param>
        /// <returns>The converted unsigned int.</returns>
        public static uint ToBigEndian(uint source)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (uint)(source >> 24) |
                             ((source << 8) & 0x00FF0000) |
                             ((source >> 8) & 0x0000FF00) |
                              (source << 24);
            }
            return source;
        }
    }
}
