/// <copyright file="ILogger.cs" author ="Yehor Kolohoida">
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
    /// Logging interface that defines a method for writing log messages.
    /// </summary>
    internal interface ILogger
    {
        /// <summary>
        /// Writes a message to the log.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Log(string message);
    }
}
