using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Extensions
{
    public static class FileSystemExtensions
    {
        public static DirectoryInfo ExtractTo(string archivedFilePath, string destinationFilePath)
        {
            return default;
        }

        public static DirectoryInfo ExtractTo(this FileInfo archivedFile, string destinationFilePath)
        {
            return default;
        }

        public static DirectoryInfo ExtractTo(this FileInfo archivedFile, DirectoryInfo destination)
        {
            return default;
        }

        /// <summary>
        /// Recursively searches through a directory for fileinfos that match func
        /// </summary>
        public static void SearchFor(this DirectoryInfo dir, Func<FileInfo, bool> func)
        {
            foreach (var file in dir.GetFiles())
            {
                if (func.Invoke(file))
                {
                    Console.WriteLine(file.FullName);
                }
            }

            foreach (var d in dir.GetDirectories())
            {
                d.SearchFor(func);
            }
        }
    }
}
