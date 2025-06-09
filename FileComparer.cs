/// <copyright file="FileComparer.cs" author ="Yehor Kolohoida">
/// Copyright (c) 2025. All rights reserved.
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    /// <summary>
    /// Provides methods for comparing files by content using hash algorithms.
    /// </summary>
    internal static class FileComparer
    {
        /// <summary>
        /// Compares two files using the specified hash algorithm.
        /// </summary>
        /// <param name="path1">Path to the first file.</param>
        /// <param name="path2">Path to the second file.</param>
        /// <param name="algorithm">Hash algorithm to use. Default is MD5.</param>
        /// <returns>True if the files are identical; otherwise, false.</returns>
        public static bool AreFilesEqual(string path1, string path2, HashAlgorithmType algorithm = HashAlgorithmType.MD5)
        {
            if (!File.Exists(path1) || !File.Exists(path2))
                return false;

            // Compare file sizes as a quick check
            var fileInfo1 = new FileInfo(path1);
            var fileInfo2 = new FileInfo(path2);

            if (fileInfo1.Length != fileInfo2.Length)
                return false;

            byte[] hash1 = ComputeHash(path1, algorithm);
            byte[] hash2 = ComputeHash(path2, algorithm);

            return hash1.SequenceEqual(hash2);
        }

        /// <summary>
        /// Computes the hash of a file and returns it as a hexadecimal string.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="algorithm">Hash algorithm to use. Default is MD5.</param>
        /// <returns>The computed hash as a string.</returns>
        public static string ComputeHashString(string filePath, HashAlgorithmType algorithm = HashAlgorithmType.MD5)
        {
            var hash = ComputeHash(filePath, algorithm);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// Computes the hash of a file and returns it as a byte array.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="algorithm">Hash algorithm to use.</param>
        /// <returns>The computed hash as a byte array.</returns>
        private static byte[] ComputeHash(string filePath, HashAlgorithmType algorithm)
        {
            using var stream = File.OpenRead(filePath);
            using HashAlgorithm hasher = algorithm switch
            {
                HashAlgorithmType.MD5 => MD5.Create(),
                HashAlgorithmType.SHA256 => SHA256.Create(),
                _ => throw new ArgumentException($"Unsupported hash algorithm: {algorithm}")
            };
            return hasher.ComputeHash(stream);
        }

    }
}
