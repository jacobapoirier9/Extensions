using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void Log(this object obj)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                Log($"{prop.Name} - {prop.GetValue(obj)}");
            }
        }
    }
}
