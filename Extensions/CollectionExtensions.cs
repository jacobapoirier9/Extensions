using System;
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

        [Obsolete]
        public static void CleanSplit(this IEnumerable<string> data, string pattern)
        {
            var toReturn = new List<string>();
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
    }
}