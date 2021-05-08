using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    /// <summary>
    /// Generic extension methods for any type
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Determines if all properties of an item are null
        /// </summary>
        public static bool HasAllNullProperties<T>(this T item)
        {
            var props = typeof(T).GetProperties();
            var propCount = props.Length;
            var nullCount = 0;

            foreach (var prop in props)
            {
                if (prop.GetValue(item) is null)
                    nullCount += 1;
            }

            return propCount == nullCount;
        }

        /// <summary>
        /// Determines if any property of an item are null
        /// </summary>
        public static bool HasAnyNullProperties<T>(this T item)
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.GetValue(item) is null)
                    return true;
            }

            return false;
        }
    }
}