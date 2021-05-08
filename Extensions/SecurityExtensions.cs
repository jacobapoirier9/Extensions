using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Extensions
{
    public static class SecurityExtensions
    {
        /// <summary>
        /// One way encrypt a string
        /// </summary>
        public static string Encrypt(this string str)
        {
            var s = new SHA256CryptoServiceProvider();

            var bytes = new byte[str.Length];
            
            for (var i = 0; i < str.Length; i++)
            {
                bytes[i] = (byte)str[i];
            }

            string toReturn = "";
            foreach (var b in s.ComputeHash(bytes))
            {
                toReturn += b;
            }

            return toReturn;
        }


        /// <summary>
        /// Base64 encodes a string
        /// </summary>
        public static string Encode(this string value)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        }

        /// <summary>
        /// Decodes a base64 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Decode(this string value)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(value));
        }
    }
}