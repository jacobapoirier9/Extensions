using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    /// <summary>
    /// A collection of easy to read DateTime operations
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines whether a date is before or on a given date
        /// </summary>
        public static bool IsBeforeOrOn(this DateTime current, DateTime target) =>
            current.IsBefore(target) || current.IsOn(target);

        /// <summary>
        /// Determines whether a date is after or on a given date
        /// </summary>
        public static bool IsAfterOrOn(this DateTime current, DateTime target) =>
            current.IsAfter(target) || current.IsOn(target);

        /// <summary>
        /// Determines whether a date is on a given date
        /// </summary>
        public static bool IsOn(this DateTime current, DateTime target) => current.Date == target.Date;

        /// <summary>
        /// Determines whether a date is not on a given date
        /// </summary>
        public static bool IsNotOnSameDayAs(this DateTime current, DateTime target) => current.Date != target.Date;

        /// <summary>
        /// Determines whether a date is today
        /// </summary>
        public static bool IsToday(this DateTime current) => current.IsOn(DateTime.Today);
        
        /// <summary>
        /// Determines whether a date is tomorrow
        /// </summary>
        public static bool IsTomorrow(this DateTime current) => current.IsOn(DateTime.Today.GetTomorrow());

        /// <summary>
        /// Determines whether a date is yesterday
        /// </summary>
        public static bool IsYesterday(this DateTime current) => current.IsOn(DateTime.Today.GetYesterday());

        /// <summary>
        /// Determines whether a date is before a given date
        /// </summary>
        public static bool IsBefore(this DateTime current, DateTime target) => current < target;

        /// <summary>
        /// Determines whether a date is after a given date
        /// </summary>
        public static bool IsAfter(this DateTime current, DateTime target) => current > target;

        /// <summary>
        /// Determines whether a date is between either of the two given dates
        /// </summary>
        public static bool IsBetween(this DateTime current, DateTime targetOne, DateTime targetTwo)
        {
            if (targetOne.IsBefore(targetTwo))
                return (current.IsBefore(targetTwo) && current.IsAfter(targetOne));
            else
                return (current.IsBefore(targetOne) && current.IsAfter(targetTwo));
        }

        /// <summary>
        /// Determines whether a date is not between either of the two given dates
        /// </summary>
        public static bool IsNotBetween(this DateTime current, DateTime targetOne, DateTime targetTwo) => 
            !current.IsBetween(targetOne, targetTwo);

        /// <summary>
        /// Gets a date representing tomorrow
        /// </summary>
        public static DateTime GetTomorrow(this DateTime current) => current.AddDays(1);

        /// <summary>
        /// Gets a date representing yesterday
        /// </summary>
        public static DateTime GetYesterday(this DateTime current) => current.AddDays(-1);
    }
}
