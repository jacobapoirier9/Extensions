using ServiceStack;
using System;
using System.Diagnostics;
using System.Linq;

namespace Extensions.Clients
{
    public static class CommandLine
    {
        /// <summary>
        /// Runs a command on the command line and returns the output
        /// </summary>
        public static string Run(string command) => Run(command, "");

        /// <summary>
        /// Runs a command on the command line with parameters and returns the output
        /// </summary>
        public static string Run(string command, string args)
        {
            var process = new ProcessStartInfo(command, args);
            process.RedirectStandardOutput = true;
            process.UseShellExecute = false;
            
            var reg = Process.Start(process);

            using (var reader = reg.StandardOutput)
            {
                return reader.ReadToEnd();
            }
        }


        /// <summary>
        /// Acts as a client to run methods from the command line
        /// </summary>
        public static void Run<T>(string[] args)
        {
            switch (args.Length)
            {
                case 0: PrintUsage<T>(); break;
                case 1: typeof(T).Execute(args[0]); break;
                case 2: typeof(T).Execute(args[0], args.ToList().GetRange(1, args.Length - 1).ToArray()); break;
            }
        }

        /// <summary>
        /// Prints a summery of each method
        /// </summary>
        private static void PrintUsage<T>()
        {
            foreach (var item in typeof(T).GetCustomMethods())
            {
                var summary = $"{(typeof(T).Get<SummaryAttribute>(item.Name) ?? new SummaryAttribute("")).Message}";

                Console.WriteLine($"{item.Name, -15} -\t{summary, 30}");
            }
        }
    }
}
