using System;

namespace Extensions
{
    public interface IConfirmRunable
    {
        void Confirm();
    }
    
    public class ConfirmRunAttribute : Attribute, IConfirmRunable
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