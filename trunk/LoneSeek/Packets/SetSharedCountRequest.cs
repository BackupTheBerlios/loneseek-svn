using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Used to tell the server how many files and folders we share.
    /// </summary>
    public class SetSharedCountRequest : Packet
    {
        public SetSharedCountRequest()
        {
            this.type = PacketType.SharedFoldersFiles;
            this.direction = PacketDirection.ClientToServer;

            AddInt();       // Shared folders
            AddInt();       // Shared files.
        }

        /// <summary>
        /// Sets or retrieves the number of shared folders.
        /// </summary>
        public Int32 SharedFolders
        {
            get { return (Int32)data[0]; }
            set { data[0] = value; }
        }

        /// <summary>
        /// Sets or retrieves the number of shared folders.
        /// </summary>
        public Int32 SharedFiles
        {
            get { return (Int32)data[1]; }
            set { data[1] = value; }
        }
    }
}
