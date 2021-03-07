using Extensions.Clients;
using ServiceStack;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Extensions
{
    /// <summary>
    /// A collection of methods to use LinqPad
    /// </summary>
    public static class LinqPad
    {
        private const string commandPath = @"C:\Program Files (x86)\LINQPad5\LPRun.exe";

        /// <summary>
        /// Run a linq pad query and map the output to List of <typeparamref name="T"/>
        /// </summary>
        public static List<T> Run<T>(string linqFilePath, string linqCommandPath = null)
        {
            var data = CommandLine.Run(linqCommandPath ?? commandPath, linqFilePath);

            if (data.CleanSplit('[').Count > 1)
                throw new TooManyTablesOutputtedException();
            else
                return data.FromJson<List<T>>();
            
        }
    }
}
