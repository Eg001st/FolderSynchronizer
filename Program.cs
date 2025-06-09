/// <copyright file="Program.cs" author ="Yehor Kolohoida">
/// Copyright (c) 2025. All rights reserved.
/// </copyright>


using System;
using System.Timers;

namespace FolderSynchronizer
{
    /// <summary>
    /// Entry point of the application. Initializes logger, synchronizer, and starts the timer.
    /// </summary>
    class Program
    {
        private static System.Timers.Timer? timer;

        /// <summary>
        /// Main method executed on program start.
        /// </summary>
        static void Main(string[] args)
        {
            var options = CommandLineOptions.Parse(args);

            if (!options.Validate(out string error))
            {
                Console.WriteLine(error);
                return;
            }

            ILogger logger = new FileLogger(options.LogFilePath);
            ISynchronizer synchronizer = new FolderSynchronizer(options.SourcePath, options.ReplicaPath, logger);

            timer = new System.Timers.Timer(options.IntervalSeconds * 1000);
            timer.Elapsed += (s, e) => synchronizer.Synchronize();
            timer.Start();

            Console.WriteLine("Synchronization started. Press Enter to stop.");
            Console.ReadLine();
        }
    }
}