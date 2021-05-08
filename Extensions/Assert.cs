using System;

namespace Extensions.Assertions
{
    /// <summary>
    /// A collection of methods to handle validation checks
    /// </summary>
    public static class Assert
    {
        /// <summary>
        /// Throws an exception is an item has any null properties
        /// </summary>
        public static void HasNoNullProperties<T>(T item)
        {
            Assert.IsNotNull(item);
            if (item.HasAnyNullProperties())
                throw new ArgumentNullException("Item can not have null properties");
        }

        /// <summary>
        /// Throws an exception if an item is null
        /// </summary>
        public static void IsNotNull<T>(T item)
        {
            if (item == null)
                throw new ArgumentNullException("Item can not be null");
        }
    }
}