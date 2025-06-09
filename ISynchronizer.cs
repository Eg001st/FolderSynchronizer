/// <copyright file="ISynchronizer.cs" author ="Yehor Kolohoida">
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
    /// Interface that defines a method to perform folder synchronization.
    /// </summary>
    internal interface ISynchronizer
    {
        /// <summary>
        /// Starts the synchronization process.
        /// </summary>
        /// <returns>True if synchronization was successful; otherwise, false.</returns>
        bool Synchronize();
    }
}
