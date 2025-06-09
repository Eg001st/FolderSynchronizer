/// <copyright file="FileLogger.cs" author ="Yehor Kolohoida">
/// Copyright (c) 2025. All rights reserved.
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FolderSynchronizer
{
    /// <summary>
    /// Implementation of ILogger that writes log messages to a file and the console.
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly string LogFilePath;

        public FileLogger(string logFilePath)
        {
            this.LogFilePath = logFilePath;

            if (!String.IsNullOrWhiteSpace(Defaults.LogFilePath) && !Directory.Exists(Defaults.LogFilePath))
            {
                Directory.CreateDirectory(Defaults.LogFilePath);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string logFileName = $"sync_{timestamp}.log";
            LogFilePath = Path.Combine(Defaults.LogFilePath, logFileName);
        }

        /// <summary>
        /// Logs a timestamped message to the console and a log file.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string fullMessage = $"{timestamp} | {message}";

            Console.WriteLine(fullMessage);
            File.AppendAllText(LogFilePath, fullMessage + Environment.NewLine, Encoding.UTF8);
        }
    }
}
