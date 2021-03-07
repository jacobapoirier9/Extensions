using System;

namespace Extensions
{
    /// <summary>
    /// Allows a summary to be placed to provide a summary for items
    /// </summary>
    public class SummaryAttribute : Attribute
    {
        /// <summary>
        /// The message to be displayed
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Creates an attribute with a message
        /// </summary>
        public SummaryAttribute(string msg)
        {
            Message = msg;
        }
    }
}