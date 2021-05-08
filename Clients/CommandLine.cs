using Extensions.Attributes;
using ServiceStack;
using System;
using System.Diagnostics;
using System.Linq;

namespace Extensions.Clients
{
    /// <summary>
    /// Allows the user to interact with the client
    /// </summary>
    public static class CommandLine
    {
        /// <summary>
        /// Runs a command with no parameters on cmd
        /// </summary>
        public static string Run(string command)
        {
            return Run(command, string.Empty);
        }

        /// <summary>
        /// Runs a command with the given parameters on cmd
        /// </summary>
        public static string Run(string command, string args)
        {
            var process = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = $"/c {(command.Contains(' ') ? $"\"{command}\"" : command)} {args}"
            };

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
                case 0: 
                    PrintUsage<T>(); 
                    break;
                case 1: 
                    try { typeof(T).Execute(null, args[0]); }
                    catch { typeof(BuiltInCommands).Execute(null, args[0]); }
                    break;
                case 2: 
                    try  {
                        typeof(T).Execute(null, args[0], args.ToList().GetRange(1, args.Length - 1).ToArray());
                    }
                    catch {
                        typeof(BuiltInCommands).Execute(null, args[0], args.ToList().GetRange(1, args.Length - 1).ToArray());
                    }
                    break;
            }
        }

        /// <summary>
        /// Prints a summery of each method
        /// </summary>
        private static void PrintUsage<T>()
        {
            foreach (var item in typeof(T).GetMethods(m => m.DeclaringType != typeof(object)))
            {
                var summary = $"{ (typeof(T).Get<SummaryAttribute>(item.Name) ?? new SummaryAttribute("")).Message}";

                if (typeof(T).Get<HiddenAttribute>(item.Name) is null)
                    Console.WriteLine($"{item.Name, -15} -\t{summary, 30}");
            }
        }
    }
}
