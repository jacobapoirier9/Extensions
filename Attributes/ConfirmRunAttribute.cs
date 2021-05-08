using System;

namespace Extensions.Attributes
{
    /// <summary>
    /// Runs before a reflection execution
    /// </summary>
    public class ConfirmRunAttribute : Attribute
    {
        public void Confirm()
        {
            Console.Write("Are you sure? [Y/N] >> ");
            if (Console.ReadLine().ToUpper() != "Y")
            {
                Environment.Exit(1);
            }
        }
    }
}