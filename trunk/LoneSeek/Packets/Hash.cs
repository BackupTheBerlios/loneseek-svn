using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Hash calculation helpers.
    /// </summary>
    public static class Hash
    {

        /// <summary>
        /// Makes a byte array human readable.
        /// </summary>
        /// <param name="array">Array to transform.</param>
        /// <returns>String representation of the byte array.</returns>
        public static String ByteArrayToString(byte[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte b in array)
            {
                builder.Append(String.Format("{0:x2}", b));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Calculates the MD5 hash of the given value.
        /// </summary>
        /// <param name="data">Value to hash</param>
        /// <param name="encoding">String encoding.</param>
        /// <returns>The hash of the input data.</returns>
        public static String CalculateHash(String data, Encoding encoding)
        {
            HashAlgorithm algo = HashAlgorithm.Create("MD5");
            byte[] output = algo.ComputeHash(encoding.GetBytes(data));

            return ByteArrayToString(output);
        }

        /// <summary>
        /// Calculates the MD5 hash of the given value using UTF8 Encoding.
        /// </summary>
        /// <param name="data">Value to hash</param>
        /// <returns>The hash of the input data.</returns>
        public static String CalculateHash(String data)
        { 
            return CalculateHash(data, Encoding.UTF8);
        }
    }
}
