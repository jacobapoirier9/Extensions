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

        public static string Encode(this string value) => Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        public static string Decode(this string value) => Encoding.ASCII.GetString(Convert.FromBase64String(value));
    }
}