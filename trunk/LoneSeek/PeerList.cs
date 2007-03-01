using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek
{
    /// <summary>
    /// Represents a list of peers.
    /// </summary>
    public class PeerList : List<Peer>
    {
        /// <summary>
        /// Constructs a new empty peer list.
        /// </summary>
        public PeerList()
        {
        }

        /// <summary>
        /// Closes all sockets and then clears the list.
        /// </summary>
        public new void Clear()
        {
            while (Count > 0 )
            { // All peers should be closed and deleted
                Peer p = this[0];
                // Close peer
                p.Close();
                // Remove first item
                RemoveAt(0);
            }
        }
    }
}
