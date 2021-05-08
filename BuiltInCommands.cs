using Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public partial class BuiltInCommands
    {
        [Hidden]
        public static void Encode(string text)
        {
            Console.WriteLine(text.Encode());
        }

        [Hidden]
        public static void Decode(string text)
        {
            Console.WriteLine(text.Decode());
        }
    }
}
