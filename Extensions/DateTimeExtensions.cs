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
        public static bool IsBeforeOrOn(this DateTime current, DateTime target) =>
            current.IsBefore(target) || current.IsOnSameDayAs(target);
        public static bool IsAfterOrOn(this DateTime current, DateTime target) =>
            current.IsAfter(target) || current.IsOnSameDayAs(target);

        public static bool IsToday(this DateTime current) => current.IsOnSameDayAs(DateTime.Today);
        public static bool IsTomorrow(this DateTime current) => current.IsOnSameDayAs(DateTime.Today.GetTomorrow());
        public static bool IsYesterday(this DateTime current) => current.IsOnSameDayAs(DateTime.Today.GetYesterday());

        public static bool IsOnSameDayAs(this DateTime current, DateTime target) => current.Date == target.Date;
        public static bool IsNotOnSameDayAs(this DateTime current, DateTime target) => current.Date != target.Date;
        public static bool IsBefore(this DateTime current, DateTime target) => current < target;
        public static bool IsAfter(this DateTime current, DateTime target) => current > target;
        public static bool IsBetween(this DateTime current, DateTime targetOne, DateTime targetTwo)
        {
            if (targetOne.IsBefore(targetTwo))
                return (current.IsBefore(targetTwo) && current.IsAfter(targetOne));
            else
                return (current.IsBefore(targetOne) && current.IsAfter(targetTwo));
        }

        public static bool IsNotBetween(this DateTime current, DateTime targetOne, DateTime targetTwo) => 
            !current.IsBetween(targetOne, targetTwo);

        public static DateTime GetTomorrow(this DateTime current) => current.AddDays(1);
        public static DateTime GetYesterday(this DateTime current) => current.AddDays(-1);
    }
}
