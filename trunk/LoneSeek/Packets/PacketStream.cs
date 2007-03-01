using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Stream abstraction layer to read SoulSeek types.
    /// </summary>
    public class PacketStream
    {
        private BinaryReader reader = null;
        private BinaryWriter writer = null;

        public PacketStream(Stream basicStream)
        {
            reader = new BinaryReader(basicStream);
            writer = new BinaryWriter(basicStream);
        }

        /// <summary>
        /// Read an array of strings.
        /// </summary>
        /// <returns></returns>
        public String[] ReadStrings()
        {
            List<String> strings = new List<String>();
            Int32 cnt = ReadInt();

            while (cnt > 0)
            { // Read all strings.
                String read = ReadString();
                strings.Add(read);
                --cnt;
            }
            return strings.ToArray();
        }

        /// <summary>
        /// Read an array of Integers.
        /// </summary>
        /// <returns></returns>
        public Int32[] ReadInts()
        {
            List<Int32> ints = new List<Int32>();
            Int32 cnt = ReadInt();

            while (cnt > 0)
            { // Read all ints.
                Int32 read = ReadInt();
                ints.Add(read);
                --cnt;
            }
            return ints.ToArray();
        }

        /// <summary>
        /// Read byte.
        /// </summary>
        /// <returns></returns>
        public Byte ReadByte()
        {
            return reader.ReadByte();
        }

        /// <summary>
        /// Read an SoulSeek integer.
        /// </summary>
        /// <returns></returns>
        public Int32 ReadInt()
        {
            return reader.ReadInt32();
        }

        /// <summary>
        /// Read a SoulSeek string.
        /// </summary>
        /// <returns></returns>
        public String ReadString()
        {
            Int32 length = ReadInt();
            byte[] byteString = reader.ReadBytes(length);
            String value = "";
            // Here we go
            value = Encoding.UTF8.GetString(byteString);
            // Return the read value
            return value;
        }

        /// <summary>
        /// Write a string.
        /// </summary>
        /// <param name="value"></param>
        public void Write(String value)
        {
            writer.Write(value.Length); // Length of the string
            writer.Write(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Write an Int32
        /// </summary>
        /// <param name="value"></param>
        public void Write(Int32 value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Write a Byte.
        /// </summary>
        /// <param name="value"></param>
        public void Writer(Byte value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Write an array of Strings
        /// </summary>
        /// <param name="value"></param>
        public void Write(String[] value)
        {
            foreach (String item in value)
            { // Write the item
                Write(item);
            }
        }

        /// <summary>
        /// Write an array of Int
        /// </summary>
        /// <param name="value"></param>
        public void Write(Int32[] value)
        {
            foreach (Int32 item in value)
            { // Write the item
                Write(item);
            }
        }
    }
}
