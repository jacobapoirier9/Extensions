using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Extensions.FileReaders
{
    /// <summary>
    /// Allows you to perform simple search queries on an xml doc
    /// </summary>
    public class XmlReader : IDisposable
    {
        /// <summary>
        /// Object that holds the xml data
        /// </summary>
        private XmlDocument _doc;

        /// <summary>
        /// Creates a SimpleXPathReader given a stream
        /// </summary>
        public XmlReader(Stream stream)
        {
            _doc = new XmlDocument();
            _doc.Load(stream);
        }
        /// <summary>
        /// Creates a SimpleXPathReader given a file
        /// </summary>
        public XmlReader(FileInfo xmlFile) : this(xmlFile.OpenRead())
        {
        }
        /// <summary>
        /// Creates a SimpleXPathReader given a file path
        /// </summary>
        public XmlReader(string xmlFilePath) : this(new FileInfo(xmlFilePath))
        {
        }

        /// <summary>
        /// Gets a value from an xml document and converts it to a simple type
        /// </summary>
        public T Read<T>(string xpath)
        {
            XmlNode node = _doc.SelectSingleNode(xpath);

            if (xpath.Contains("@"))
            {
                var data = xpath.Split('@');
                xpath = data[0].TrimEnd('/');
                var attribute = data[1];
                node = _doc.SelectSingleNode(xpath);
                var d = node.Attributes[attribute];

                return (T)Convert.ChangeType(node.InnerText, typeof(T));
            }

            return (T)Convert.ChangeType(node.InnerText, typeof(T));
        }


        public void Dispose()
        {
        }
    }
}
