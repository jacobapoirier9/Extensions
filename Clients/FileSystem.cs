using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Extensions.Clients
{
    /// <summary>
    /// Holds methods that interact with the file system
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// Compares two files with eachother
        /// </summary>
        public static void Compare(string filePathOne, string filePathTwo)
        {
            Compare(new FileInfo(filePathOne), new FileInfo(filePathTwo));
        }

        /// <summary>
        /// Compares two files with eachother
        /// </summary>
        public static void Compare(FileInfo fileOne, FileInfo fileTwo, string separator = " ")
        {
            var linesOne = fileOne.ReadAllLines();
            var linesTwo = fileTwo.ReadAllLines();

            var builder = new StringBuilder();

            // line count matches
            if (linesOne.Count == linesTwo.Count)
            {
                // check each lineto make sure they are the same
                for (var i = 0; i < linesOne.Count; i++)
                {
                    // lines are not the same - check length first to save on some speeed
                    if (linesOne[i].Length != linesTwo[i].Length || linesOne[i] != linesTwo[i])
                    {
                        builder.AppendLine($"Line # {i}\n");
                        builder.AppendLine("\tFile One\n");
                        builder.AppendLine($"\t\t{linesOne[i]}\n");
                        builder.AppendLine("\tFile Two\n");
                        builder.AppendLine($"\t\t{linesTwo[i]}\n");
                    }
                }
            }

            // line count does not match
            else
            {
                builder.AppendLine(string.Format("{0, 10}: {1, 4} - {2, 4}", "File", "One", "Two"));
                builder.AppendLine(string.Format("{0, 10}: {1, 4} - {2, 4}", "Line count", linesOne.Count, linesTwo.Count));
            }

            if (builder.Length == 0)
            {
                Console.WriteLine("There are no errors");
            }
            else
            {
                Console.WriteLine(builder);
            }
        }

        /// <summary>
        /// Reads all lines of a file info
        /// </summary>
        public static List<string> ReadAllLines(this FileInfo info)
        {
            return File.ReadAllLines(info.FullName).ToList();
        }
    }
}
