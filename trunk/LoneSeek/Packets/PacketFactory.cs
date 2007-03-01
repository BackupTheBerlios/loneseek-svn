using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Crafts a packet from the given type. Static class.
    /// </summary>
    public static class PacketFactory
    {
        private static Dictionary<PacketType, Dictionary<PacketDirection, Type>> types = new Dictionary<PacketType, Dictionary<PacketDirection, Type>>();
        private static bool initialized = false;

        /// <summary>
        /// Returns true if the PacketFactory() has been initialized.
        /// </summary>
        public static bool Initialized
        {
            get { return initialized; }
        }

        /// <summary>
        /// Register all packets.
        /// </summary>
        public static void Initialize()
        {
            // Register login packet
            Register(PacketType.Login, PacketDirection.ClientToServer, typeof(LoginRequest));
            Register(PacketType.Login, PacketDirection.ServerToClient, typeof(LoginReply));
            // Set port packet
            Register(PacketType.SetWaitPort, PacketDirection.ClientToServer, typeof(SetWaitPortRequest));
            // Shared files and folders
            Register(PacketType.SharedFoldersFiles, PacketDirection.ClientToServer, typeof(SetSharedCountRequest));
            // Get parent list
            Register(PacketType.GetParentList, PacketDirection.ClientToServer, typeof(GetParentListRequest));
            // Room list
            Register(PacketType.RoomList, PacketDirection.ClientToServer, typeof(RoomListRequest));
            Register(PacketType.RoomList, PacketDirection.ServerToClient, typeof(RoomListReply));
            // Join a room
            Register(PacketType.JoinRoom, PacketDirection.ClientToServer, typeof(JoinRoomRequest));
            Register(PacketType.JoinRoom, PacketDirection.ServerToClient, typeof(JoinRoomReply));

            initialized = true;
        }

        /// <summary>
        /// Register a packet.
        /// </summary>
        /// <param name="type">Type to register.</param>
        /// <param name="packet">Packet corresponding to the type.</param>
        /// <param name="direction">Direction of the packet.</param>
        public static void Register(PacketType type, PacketDirection direction, Type packetType)
        {
            if (packetType.IsSubclassOf(typeof(Packet)))
            {
                if (!types.ContainsKey(type))
                { // Register type.
                    types[type] = new Dictionary<PacketDirection, Type>();
                }
                types[type][direction] = packetType;
            }
        }

        /// <summary>
        /// Creates a new packet from the given type.
        /// </summary>
        /// <param name="type">Type of the packet to craft.</param>
        /// <param name="direction">Direction of the packet.</param>
        /// <returns>An instance of this packet or null.</returns>
        public static Packet Create(PacketType type, PacketDirection direction)
        {
            if (!types.ContainsKey(type))
            { // Nothing here.
                return null;
            }
            if (!types[type].ContainsKey(direction))
            { // Nothing here with the requested direction.
                return null;
            }
            // Get a dolly.
            return Activator.CreateInstance(types[type][direction]) as Packet;
        }


        /// <summary>
        /// Read a packet from the given stream.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        public static Packet FromStream(Stream stream)
        {
            try
            {
                Packet packet = null;
                BinaryReader reader = new BinaryReader(stream);
                byte[] therest = null;
                int length = 0, type = 0;
                int read = 0, rest = 0;

                // Read an Int32
                length = reader.ReadInt32();
                // Read type.
                type = reader.ReadInt32();
                // Craft a packet.
                packet = Create((PacketType)type, PacketDirection.ServerToClient);
                // Let the packet do the rest, with the data we have read.
                // So let's read the packet to the end.
                rest = length - 4;
                // The reading though is important to keep the stream clear.
                // Read the additional information
                while (read < rest)
                { // Everything, please
                    therest = reader.ReadBytes(rest);
                    read += therest.Length;
                }
                if (packet != null)
                { // If it is not null parse it.
                    packet.Parse(therest);
                }
                else
                { // Throw error
                    String error = String.Format("Packet of type {0} not yet supported.", type);
                    throw new ApplicationException(error);
                }

                return packet;
            }
            catch (Exception e)
            {
                throw new InvalidPacketException(e.Message);
            }
        }
    }
}
