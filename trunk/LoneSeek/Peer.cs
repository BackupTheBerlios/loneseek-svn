using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace LoneSeek
{
    /// <summary>
    /// Represents a peer which is connected to us.
    /// </summary>
    public class Peer
    {
        private Socket socket = null;

        /// <summary>
        /// Constructs a new empty Peer object.
        /// </summary>
        public Peer()
        {
        }

        /// <summary>
        /// Constructs a new peer object which can be contacted
        /// over the given Socket
        /// </summary>
        /// <param name="socket">Socket of the peer</param>
        public Peer(Socket socket)
        {
            this.socket = socket;
        }

        /// <summary>
        /// Retrieves the socket of the peer.
        /// </summary>
        public Socket Socket
        {
            get { return socket; }
        }

        /// <summary>
        /// Safely closes the collection to the peer.
        /// </summary>
        public void Close()
        {
            socket.Close();
            socket = null;
        }
    }
}
