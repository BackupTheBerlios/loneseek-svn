using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Represents a packet sent to the server/client.
    /// </summary>
    public partial class Packet
    {
        protected List<Object> data = new List<Object>();
        private List<Type> dataFormat = new List<Type>();
        protected PacketType type = PacketType.Unknown;
        protected PacketDirection direction = PacketDirection.ClientToServer;
        private Peer peer = null;

        /// <summary>
        /// Call this to add an Int to the packet format.
        /// </summary>
        protected void AddInt()
        {
            dataFormat.Add(typeof(Int32));
            data.Add(new Int32());
        }

        /// <summary>
        /// Call this to add a byte to the packet format.
        /// </summary>
        protected void AddByte()
        {
            dataFormat.Add(typeof(Byte));
            data.Add(new Byte());
        }

        /// <summary>
        /// Call this to add a string to the packet format.
        /// </summary>
        protected void AddString()
        {
            dataFormat.Add(typeof(String));
            data.Add("");
        }

        protected void AddStringArray()
        {
            dataFormat.Add(typeof(String[]));
            data.Add(new String[] { });
        }

        protected void AddIntArray()
        {
            dataFormat.Add(typeof(Int32[]));
            data.Add(new Int32[] { });
        }

        /// <summary>
        /// Crafts a simply packet.
        /// </summary>
        public Packet()
        {
        }

        /// <summary>
        /// The peer this packet belongs to e.g the sender.
        /// </summary>
        public Peer Peer
        {
            get { return peer; }
            set { peer = value; }
        }

        /// <summary>
        /// Retrieves the data associated within this packet.
        /// </summary>
        public List<object> Data
        {
            get { return data; }
        }

        /// <summary>
        /// Calculates the length of the data.
        /// </summary>
        public int DataLength
        {
            get
            {
                int length = 0; // 0 since we do not count the length of the
                                // data in four bit octets.
                length += 4;    // Type of the packet.
                // Count everything up
                foreach (Object obj in data)
                {
                    if (obj is Int32)
                    { // Int32: 4 bytes.
                        length += 4;
                    }
                    else if (obj is String)
                    { // String: Length of string + String itself.
                        String value = obj as String;
                        length += 4;            // Length of string
                        length += value.Length; // Length of the string
                    }
                    else if (obj is Byte)
                    { // Byte: Just one bit octet
                        length += 1;
                    }
                }
                return length;
            }
        }

        /// <summary>
        /// Sets or retrieves the packet type.
        /// </summary>
        public PacketType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Direction of the package.
        /// </summary>
        public PacketDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /// <summary>
        /// Generates a byte array out of the given dataFormat and
        /// the given data.
        /// </summary>
        /// <returns>The byte array.</returns>
        public byte[] ToByteArray()
        {
            MemoryStream memory = new MemoryStream();
            PacketStream stream = new PacketStream(memory);
            int reallength = DataLength + 4;
            byte[] array = new byte[reallength];

            // Data length
            stream.Write(DataLength);
            // Ooops... Forgot the type over here :)
            stream.Write((Int32)type);
            foreach (Object obj in data)
            {
                if (obj is String)
                { // Write string
                    stream.Write((String)obj);
                }
                else if ( obj is Int32 )
                { // Write the object
                    stream.Write((Int32)obj);
                }
                else if (obj is Byte)
                { // Write byte.
                    stream.Write((Byte)obj);
                }
                else if (obj is String[])
                { // Write the string array.
                    stream.Write((String[])obj);
                }
                else if (obj is Int32[])
                { // Write each item.
                    stream.Write((Int32[])obj);
                }
            }
            // Copy to our array, including the four needed
            // Since we do not count the "Length" field in our
            // calculation.
            Array.Copy(memory.GetBuffer(), array, reallength);
            // And return it.
            return array;
        }

        /// <summary>
        /// Parses the input data and extracts all needed
        /// information out of it.
        /// </summary>
        /// <param name="array">Array to read from</param>
        public virtual void Parse(byte[] array)
        {
            MemoryStream stream = new MemoryStream(array);
            PacketStream reader = new PacketStream(stream);

            try
            {
                Int32 index = 0;
                foreach (Type type in dataFormat)
                { // Read all information.
                    Object value = null;
                    if (type.Equals(typeof(Byte)))
                    { // Read a byte
                        value = reader.ReadByte();
                    }
                    else if (type.Equals(typeof(Int32)))
                    { // Read an int
                        value = reader.ReadInt();
                    }
                    else if (type.Equals(typeof(String)))
                    { // Read the string
                        value = reader.ReadString();
                    }
                    else if (type.Equals(typeof(String[])))
                    { // String array... where can I find the count?
                        value = reader.ReadStrings();
                    }
                    else if (type.Equals(typeof(Int32[])))
                    { // Read all integers.
                        value = reader.ReadInts();
                    }
                    if (value != null)
                    { // Set value
                        data[index] = value;
                    }
                    ++index;
                }
            }
            catch (Exception e)
            { // Throw some nasty exception here.
                new InvalidPacketException(e.Message);
            }
        }
    }
}
