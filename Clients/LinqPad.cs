using ServiceStack;
using System;
using System.Collections.Generic;

namespace Extensions.Clients
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
                throw new Exception("LinqPad query outtputted too many tables");
            else
                return data.FromJson<List<T>>();
            
        }
    }
}
