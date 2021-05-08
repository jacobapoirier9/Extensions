using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Takes an easily readable string and format's it with deisred data
        /// </summary>
        public static string FormatWith(this string str, object data)
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
        /// Repeats a string a number of times
        /// </summary>
        public static string Repeat(this string str, int count)
        {
            var toReturn = "";
            for (var i = 0; i < count; i++)
            {
                toReturn += str;
            }
            return toReturn;
        }

        /// <summary>
        /// Determines whether or not two string are like each other (case insensitive)
        /// </summary>
        public static bool IsLike(this string str1, string str2)
        {
            return str1.ToLower() == str2.ToLower();
        }

        [Obsolete("This kind of works")]
        /// <summary>
        /// Splits a string and keeps the original split at character
        /// </summary>
        public static List<string> CleanSplit(this string str, string splitAt)
        {
            return str.CleanSplit(splitAt.ToChar());
        }

        [Obsolete("This kind of works")]
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
        public static string AsTxt(this string str)
        {
            return $"{str}.txt";
        }

        /// <summary>
        /// Returns a file name as a html extension
        /// </summary>
        public static string AsHtml(this string str)
        {
            return $"{str}.html";
        }

        /// <summary>
        /// Determines if a string contains any elements
        /// </summary>
        public static bool ContainsAny(this string str, params string[] targets)
        {
            return str.ContainsAny(targets as IEnumerable<string>);
        }

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
        public static bool ContainsAll(this string str, params string[] targets)
        {
            return str.ContainsAll(targets as IEnumerable<string>);
        }

        /// <summary>
        /// Determines if a string contains all elements
        /// </summary>
        public static bool ContainsAll(this string str, IEnumerable<string> targets)
        {
            var data = targets.ToList().FindAll(item => str.Contains(item));
            return data.Count == targets.Count();
        }

        /// <summary>
        /// Converts a string to a char
        /// </summary>
        public static char ToChar(this string str)
        {
            return Convert.ToChar(str);
        }

        /// <summary>
        /// Builds a Uri based on the anon object provided
        /// </summary>
        public static string BuildUri(this string baseUri, object data)
        {
            if (data is string)
            {
                return baseUri + data;
            }
            else
            {
                baseUri += (baseUri.Contains("?")) ? "&" : "?";

                var props = data.GetType().GetProperties();
                for (var i = 0; i < props.Length; i++)
                {
                    var propValue = props[i].GetValue(data);
                    if (propValue != null)
                        baseUri += $"{props[i].Name}={propValue}{(i < props.Length - 1 ? "&" : "")}";
                }

                return baseUri;
            }
        }
    }
}