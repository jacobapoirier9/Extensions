using Extensions.Attributes;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class JsonExtensions
    {
        [Obsolete]
        public static T FromJsonToDto<T>(this string json)
        {
            var keys = new List<string>();

            foreach (var line in json.TrimStart('{').TrimEnd('}').Split(','))
            {
                keys.Add(line.Replace("\"", "").Split(':')[0]);
            }

            var type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                var attr = type.Get<JsonTextAttribute>(prop.Name);
                if (attr != null)
                {
                    json = json.Replace(attr.JsonText, prop.Name);
                }
            }

            return json.FromJson<T>();
        }
    }

    
}
