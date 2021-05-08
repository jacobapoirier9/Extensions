using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Extensions.FileReaders
{
    public class IniReader : IDisposable
    {
        /// <summary>
        /// Path of the ini config file
        /// </summary>
        public string FilePath => _iniFilePath;

        /// <summary>
        /// Holds each line entry
        /// </summary>
        private List<string> _lines;

        /// <summary>
        /// The file path to the ini config file
        /// </summary>
        private string _iniFilePath;

        /// <summary>
        /// Creates an instance of a ini config client
        /// </summary>
        public IniReader(string iniFilePath)
        {
            this._iniFilePath = iniFilePath;
            this._lines = File.ReadAllLines(iniFilePath).ToList();
            return;
        }

        /// <summary>
        /// Reads a config section for a given key
        /// </summary>
        public T Read<T>(string section, string key)
        {
            var foundSection = false;
            foreach (var line in _lines)
            {
                if (line == $"[{section}]")
                {
                    foundSection = true;
                }
                else if (foundSection && line.StartsWith($"{key}="))
                {
                    return (T)Convert.ChangeType(line.Split(new char[] { '=' }, 2)[1], typeof(T));
                }
            }

            if (foundSection)
            {
                throw new KeyNotFoundException(key);
            }
            else
            {
                throw new SectionNotFoundException(section);
            }
        }

        /// <summary>
        /// Rewrites the file with any updated data
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// One and done read
        /// </summary>
        public static T Read<T>(string iniFilepath, string section, string key)
        {
            using (var reader = new IniReader(iniFilepath))
            {
                return reader.Read<T>(section, key);
            }
        }
    }
}
