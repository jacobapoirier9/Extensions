using ServiceStack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Extensions
{
    /// <summary>
    /// A collection of usefull extension methods for collections
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines if an item is in a collection
        /// </summary>
        public static bool IsIn<T>(this T item, IEnumerable<T> data)
        {
            foreach (var thing in data)
            {
                if (item.Equals(thing))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Determines if a list contains any of the given elements
        /// </summary>
        public static bool ContainsAny(this IEnumerable<string> data, params string[] targets)
        {
            for (var i = 0; i < targets.Length; i++)
            {
                if (data.Contains(targets[i]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Determines if a list contains all of the given elements
        /// </summary>
        /// <param name="data"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        public static bool ContainsAll(this IEnumerable<string> data, params string[] targets)
        {
            var count = 0;
            for (var i = 0; i < targets.Length; i++)
            {
                if (data.Contains(targets[i]))
                    count++;
            }

            return count == targets.Length;
        }

        /// <summary>
        /// Prints items in a collection to the console
        /// </summary>
        public static void PrintAll<T>(this IEnumerable<T> data)
        {
            var list = data.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Debug.WriteLine($"{i}: {list[i]}");
                Console.WriteLine($"{i}: {list[i]}");
            }
        }

        /// <summary>
        /// Adds an item to a list under a key
        /// </summary>
        public static void AddTo<T, T2>(this Dictionary<T, List<T2>> data, T key, T2 item)
        {
            try
            {
                data[key].Add(item);
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                data.Add(key, new List<T2>() { item });
            }
        }

        /// <summary>
        /// Replaces all occurances of an item with another
        /// </summary>
        public static List<string> ReplaceAll(this IEnumerable<string> data, string replaceWhat, string replaceWith)
        {
            var toReturn = data.ToList();
            for (var i = 0; i < toReturn.Count; i++)
            {
                toReturn[i] = data.ElementAt(i).ReplaceAll(replaceWhat, replaceWith);
            }
            return toReturn;
        }
    }
}