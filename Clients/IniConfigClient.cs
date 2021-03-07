using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Extensions.Clients
{
    /// <summary>
    /// Holds a config entry for an ini config section
    /// </summary>
    public class IniConfigSetting
    {
        public string SectionName { get; internal set; }
        public string Key { get; internal set; }
        public string Value { get; internal set; }
    }

    /// <summary>
    /// Holds a config section for an ini config file
    /// </summary>
    public class IniConfigSection
    {
        public string Name { get; internal set; }
        public List<IniConfigSetting> Settings { get; internal set; }
    }

    /// <summary>
    /// Holds config file data for easy readablitity
    /// </summary>
    public class IniConfigClient : IDisposable
    {
        /// <summary>
        /// A list of config sections
        /// </summary>
        public List<IniConfigSection> Sections
        {
            get
            {
                var toReturn = new List<IniConfigSection>();

                foreach (var section in master)
                {
                    toReturn.Add(new IniConfigSection() { Name = section.Key, Settings = section.Value });
                }

                return toReturn;
            }
        }

        /// <summary>
        /// A list of config settings
        /// </summary>
        public List<IniConfigSetting> Settings
        {
            get
            {
                var toReturn = new List<IniConfigSetting>();

                foreach (var section in master)
                {
                    foreach (var item in section.Value)
                    {
                        toReturn.Add(item);
                    }
                }

                return toReturn;
            }
        }

        /// <summary>
        /// Path of the ini config file
        /// </summary>
        public string FilePath => iniFilePath;

        /// <summary>
        /// A master collection of ini config settings
        /// </summary>
        private Dictionary<string, List<IniConfigSetting>> master;

        /// <summary>
        /// The file path to the ini config file
        /// </summary>
        private string iniFilePath;

        /// <summary>
        /// Creates an instance of a ini config client
        /// </summary>
        public IniConfigClient(string iniFilePath)
        {
            this.iniFilePath = iniFilePath;
            var lines = File.ReadAllLines(iniFilePath);

            master = new Dictionary<string, List<IniConfigSetting>>();
            var currentSection = "";
            
            foreach (var line in lines)
            {
                if (line.ContainsAll("[", "]"))
                {
                    currentSection = line.TrimStart('[').TrimEnd(']');
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    var data = line.Split('=');
                    master.AddTo(currentSection, new IniConfigSetting() { SectionName = currentSection, Key = data[0], Value = data[1] });
                }
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Read'/>
        public IniConfigSection Read(string section)
        {
            var data = new IniConfigSection() { Name = section };

            foreach (var item in master[section])
            {
                data.Settings.Add(item);
            }

            return data;
            //return master[section];
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Read'/>
        public IniConfigSetting Read(string section, string key)
        {
            return master[section].Find(setting => setting.Key == key);
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Add'/>
        public void Add(string section)
        {
            master.Add(section, new List<IniConfigSetting>());
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Add'/>
        public void Add(string section, string key, string value)
        {
            if (!master.ContainsKey(section))
                Add(section);

            master[section].Add(new IniConfigSetting() { Key = key, Value = value });
        }

        // ----------------------------------------------------------------------------------

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Add'/>
        public void Add(IniConfigSection section)
        {
            Assert.IsNotNull(section.Name);
            master.Add(section.Name, section.Settings ?? new List<IniConfigSetting>());
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Add'/>
        public void Add(IniConfigSetting setting)
        {
            Assert.HasNoNullProperties(setting);
            Add(setting.SectionName, setting.Key, setting.Value);
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Update'/>
        public void Update(IniConfigSetting setting)
        {
            Assert.HasNoNullProperties(setting);
            var toRemove = Settings.Find(s => s.SectionName == setting.SectionName && s.Key == setting.Key);
            Remove(toRemove.SectionName, toRemove.Key);
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Remove'/>
        public void Remove(IniConfigSection section)
        {
            Remove(section.Name);
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Remove'/>
        public void Remove(IniConfigSetting setting)
        {
            Remove(setting.SectionName, setting.Key);
        }
        // ----------------------------------------------------------------------------------

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Update'/>
        public void Update(string section, string key, string value)
        {
            Remove(section, key);
            Add(section, key, value);
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Remove'/>
        public void Remove(string section)
        {
            master.RemoveKey(section);
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Remove'/>
        public void Remove(string section, string key)
        {
            master[section].RemoveAll(setting => setting.Key == key);
        }

        /// <summary>
        /// Rewrites the file with any updated data
        /// </summary>
        public void Dispose()
        {
            using (var writer = new StreamWriter(iniFilePath))
            {
                foreach (var section in master)
                {
                    writer.WriteLine($"[{section.Key}]");
                    foreach (var line in section.Value)
                    {
                        writer.WriteLine($"{line.Key}={line.Value}");
                    }
                    writer.WriteLine();
                }
            }
        }




        #region Static Methods

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Read'/>
        public static IniConfigSection ReadFrom(string iniFilePath, string section)
        {
            using (var client = new IniConfigClient(iniFilePath))
            {
                return client.Read(section);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Read'/>
        public static IniConfigSetting ReadFrom(string iniFilePath, string section, string key)
        {
            using (var client = new IniConfigClient(iniFilePath))
            {
                return client.Read(section, key);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Add'/>
        public static void AddTo(string iniFilePath, string section)
        {
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Add(section);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Add'/>
        public static void AddTo(string iniFilePath, string section, string key, string value)
        {
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Add(section, key, value);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Update'/>
        public static void UpdateTo(string iniFilePath, string section, string key, string value)
        {
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Update(section, key, value);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Remove'/>
        public static void RemoveFrom(string iniFilePath, string section)
        {
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Remove(section);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Remove'/>
        public static void RemoveFrom(string iniFilePath, string section, string key)
        {
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Remove(section, key);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Add'/>
        public static void AddTo(string iniFilePath, IniConfigSection section)
        {
            Assert.IsNotNull(section.Name);
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Add(section);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Add'/>
        public static void AddTo(string iniFilePath, IniConfigSetting setting)
        {
            Assert.HasNoNullProperties(setting);
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Add(setting);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Update'/>
        public static void UpdateTo(string iniFilePath, IniConfigSetting setting)
        {
            Assert.HasNoNullProperties(setting);
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Remove(setting);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Remove'/>
        public static void RemoveFrom(string iniFilePath, IniConfigSection section)
        {
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Remove(section);
            }
        }

        /// <include file='Summary.xml' path='Data/Clients/IniConfig/Remove'/>
        public static void RemoveFrom(string iniFilePath, IniConfigSetting setting)
        {
            using (var client = new IniConfigClient(iniFilePath))
            {
                client.Remove(setting);
            }
        }

        #endregion
    }
}
