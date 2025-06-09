/// <copyright file="CommandLineOptions.cs" author ="Yehor Kolohoida">
/// Copyright (c) 2025. All rights reserved.
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    /// <summary>
    /// Represents command-line arguments for configuring the synchronizer.
    /// </summary>
    internal class CommandLineOptions
    {
        public string SourcePath { get; set; } = Defaults.SourcePath;
        public string ReplicaPath { get; set; } = Defaults.ReplicaPath;
        public int IntervalSeconds { get; set; } = Defaults.IntervalSeconds;
        public string LogFilePath { get; set; } = Defaults.LogFilePath;


        /// <summary>
        /// Parses the command-line arguments into an instance of <see cref="CommandLineOptions"/>.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>An instance of <see cref="CommandLineOptions"/>.</returns>
        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();

            // If not enough arguments are provided, fall back to default paths
            if (args.Length < 2)
            {
                Console.WriteLine("You must provide at least sourcePath and replicaPath. Default values will be used.");
            }
            else
            {
                options.SourcePath = args[0];
                options.ReplicaPath = args[1];
            }

            // Parse synchronization interval if provided
            if (args.Length > 2 && Int32.TryParse(args[2], out int interval))
            {
                if (interval > 0)
                {
                    options.IntervalSeconds = interval;
                }
                else
                {
                    Console.WriteLine("Interval must be greater than 0. Default value of 60 seconds will be used.");
                }
            }

            // Set custom log file path if provided
            if (args.Length > 3)
            {
                options.LogFilePath = args[3];
            }

            return options;
        }

        /// <summary>
        /// Validates the command-line arguments.
        /// </summary>
        /// <param name="error">An error message if validation fails.</param>
        /// <returns>True if validation succeeds; otherwise, false.</returns>
        public bool Validate(out string error)
        {
            if (String.IsNullOrWhiteSpace(SourcePath) || !Directory.Exists(SourcePath))
            {
                error = "Invalid path to source folder";
                return false;
            }

            if (String.IsNullOrWhiteSpace(ReplicaPath) || !Directory.Exists(ReplicaPath))
            {
                error = "Invalid path to replica folder";
                return false;
            }

            if (String.IsNullOrWhiteSpace(LogFilePath) || !Directory.Exists(LogFilePath))
            {
                error = "Invalid path to logs folder";
                return false;
            }

            error = "";
            return true;
        }

    }
}
