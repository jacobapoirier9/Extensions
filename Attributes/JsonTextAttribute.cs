using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions.Attributes
{
    [Obsolete("This is pointless")]
    /// <summary>
    /// Replaces raw json text with desired type text
    /// </summary>
    public class JsonTextAttribute : Attribute
    {
        public string JsonText { get; private set; }

        public JsonTextAttribute(string jsonText)
        {
            this.JsonText = jsonText;
        }
    }
}
