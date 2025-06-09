/// <copyright file="FolderSynchronizer.cs" author ="Yehor Kolohoida">
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
    /// Synchronizes contents between a source and replica folder.
    /// </summary>
    internal class FolderSynchronizer : ISynchronizer
    {
        private readonly string SourcePath;
        private readonly string ReplicaPath;
        private readonly ILogger Logger;

        public FolderSynchronizer(string sourcePath, string replicaPath, ILogger logger)
        {
            this.SourcePath = sourcePath;
            this.ReplicaPath = replicaPath;
            this.Logger = logger;
        }

        /// <summary>
        /// Starts the synchronization process and handles any exceptions.
        /// </summary>
        /// <returns>True if synchronization succeeded; otherwise, false.</returns>
        public bool Synchronize()
        {
            try
            {
                Logger.Log("Starting synchronization...");
                SynchronizeInternal();
                Logger.Log("Synchronization finished.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"[ERROR] Synchronization failed: {ex.Message}");
                return false;
            }

        }

        /// <summary>
        /// Performs the core synchronization logic:
        /// - Copies new files from source to replica.
        /// - Deletes files in replica that do not exist in source.
        /// </summary>
        private void SynchronizeInternal()
        {
            try
            {
                var sourceFiles = Directory.GetFiles(SourcePath, "*", SearchOption.AllDirectories);
                var replicaFiles = Directory.GetFiles(ReplicaPath, "*", SearchOption.AllDirectories);

                // Edge Case 1: Both source and replica folders are empty
                if (sourceFiles.Length == 0 && replicaFiles.Length == 0)
                {
                    Logger.Log("Both source and replica folders are empty. No synchronization needed.");
                    return;
                }

                // Create maps: relative path => full path
                var sourceMap = sourceFiles.ToDictionary(
                    path => Path.GetRelativePath(SourcePath, path),
                    path => path);

                var replicaMap = replicaFiles.ToDictionary(
                    path => Path.GetRelativePath(ReplicaPath, path),
                    path => path);

                // Edge Case 2: Delete files from replica that are not present in source
                foreach (var relPath in replicaMap.Keys)
                {
                    if (!sourceMap.ContainsKey(relPath))
                    {
                        File.Delete(replicaMap[relPath]);
                        Logger.Log($"Deleted from replica: {relPath}");
                    }
                }

                // Edge Case 3: Copy new or changed files to replica
                foreach (var relPath in sourceMap.Keys)
                {
                    var sourceFile = sourceMap[relPath];
                    var targetFile = Path.Combine(ReplicaPath, relPath);

                    // Copy if file does not exist in replica or differs
                    if (!File.Exists(targetFile) ||
                        !FileComparer.AreFilesEqual(sourceFile, targetFile, HashAlgorithmType.MD5)) // or SHA256
                    {
                        var targetDir = Path.GetDirectoryName(targetFile);

                        if (!string.IsNullOrEmpty(targetDir) && !Directory.Exists(targetDir))
                        {
                            Directory.CreateDirectory(targetDir);
                        }

                        File.Copy(sourceFile, targetFile, true);
                        Logger.Log($"Copied/updated: {relPath}");
                    }
                }

                Logger.Log("Synchronization completed.");
            }
            catch (Exception ex)
            {
                Logger.Log($"Error during synchronization: {ex.Message}");
            }
        }
    }
}
