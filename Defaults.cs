/// <copyright file="Defaults.cs" author ="Yehor Kolohoida">
/// Copyright (c) 2025. All rights reserved.
/// </copyright>

namespace FolderSynchronizer
{
    /// <summary>
    /// Contains default values for the synchronization process.
    /// </summary>
    /// 

    /// <remarks>
    /// Note:
    /// Make sure that the following directories exist before running the application or tests:
    /// - defaultSource
    /// - defaultReplica
    /// - Logs
    ///
    /// These folders are expected to be located in the current working directory,
    /// which is typically the 'bin/Debug/netX.Y' folder when running from Visual Studio.
    /// 
    /// If they don't exist, create them manually inside that folder to avoid DirectoryNotFoundException.
    /// </remarks>

    public static class Defaults
    {
        public const int IntervalSeconds = 60;
        public static string LogFilePath => Path.Combine(AppContext.BaseDirectory, "Logs");
        public static string SourcePath => Path.Combine(AppContext.BaseDirectory, "defaultSource");
        public static string ReplicaPath => Path.Combine(AppContext.BaseDirectory, "defaultReplica");
    }
}
