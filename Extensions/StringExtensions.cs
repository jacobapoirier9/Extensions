using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Inserts object properties into a string
        /// </summary>
        public static string Insert(this string str, object data)
        {
            var props = data.GetType().GetProperties();
            foreach (var prop in props)
            {
                var test = prop.GetValue(data);
                str = str.ReplaceAll("{" + prop.Name.ToLower() + "}", prop.GetValue(data) as string);
            }
            return str;
        }

        /// <summary>
        /// Determines whether or not two string are like each other (case insensitive)
        /// </summary>
        public static bool IsLike(this string str1, string str2)
        {
            return str1.ToLower() == str2.ToLower();
        }

        /// <summary>
        /// Splits a string and keeps the original split at character
        /// </summary>
        public static List<string> CleanSplit(this string str, char splitAt)
        {
            var data = str.Split(splitAt);
            var toReturn = new List<string>();

            for (var i = 0; i < data.Length - 1; i++)
            {
                toReturn.Add(data[i] += splitAt);
            }

            return toReturn;
        }

        /// <summary>
        /// Returns a file name as a txt extension
        /// </summary>
        public static string AsTxt(this string str) => $"{str}.txt";

        /// <summary>
        /// Returns a file name as a html extension
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AsHtml(this string str) => $"{str}.html";

        /// <summary>
        /// Determines if a string contains any elements
        /// </summary>
        public static bool ContainsAny(this string str, params string[] targets) => str.ContainsAny(targets as IEnumerable<string>);

        /// <summary>
        /// Determines if a string contains any elements
        /// </summary>
        public static bool ContainsAny(this string str, IEnumerable<string> targets)
        {
            foreach (var item in targets)
            {
                if (str.Contains(item))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if a string contains all elements
        /// </summary>
        public static bool ContainsAll(this string str, params string[] targets) => str.ContainsAll(targets as IEnumerable<string>);

        /// <summary>
        /// Determines if a string contains all elements
        /// </summary>
        public static bool ContainsAll(this string str, IEnumerable<string> targets)
        {
            var data = targets.ToList().FindAll(item => str.Contains(item));
            return data.Count == targets.Count();
        }

        // TODO FINISH THIS
        /// <summary>
        /// Splits a string in half at a specified string
        /// </summary>
        public static void SplitInHalf(this string data, string splitAt, int occurance)
        {
            var testString = "Name=oisehtsek3sjgnsdf==";
            var lines = testString.Split(splitAt.ToChar()).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                lines[i] += splitAt;
            }

            lines.PrintAll();
        }

        /// <summary>
        /// Converts a string to a char
        /// </summary>
        public static char ToChar(this string str) => Convert.ToChar(str);

    }
}