# FolderSynchronizer

 **FolderSynchronizer**  
 *Test assignment for Junior Developer in QA position*  
 Company: **Veeam Software**  
 Author: Yehor Kolohoida (yehor.kolohoida@gmail.com)
 Copyright © 2025

---

##  Project Overview

**FolderSynchronizer** is a console application written in C# that synchronizes the contents of a _source_ folder to a _replica_ folder at regular intervals.

This project was implemented as a **test assignment** for the **Junior Developer in QA** role at **Veeam Software**.  
It emphasizes good software design, logging, and edge-case handling.

---

##  Features

- Full synchronization of files from a source directory to a replica directory
- Recursive folder and file comparison using hashing (MD5 or SHA256)
- Logging with timestamped messages to both console and `.log` files
- Support for command-line arguments (source, replica, interval, log folder)
- Unit tests for key components
- Clear structure and extensibility

---

##  Project Structure

| Component | Description |
|----------|-------------|
| `Program.cs` | Entry point. Parses CLI arguments and starts synchronization. |
| `ILogger` | Interface for logging (extensible). |
| `FileLogger` | Logs messages to console and to a log file with timestamp. |
| `ISynchronizer` | Interface for folder synchronization logic. |
| `FolderSynchronizer` | Implements synchronization logic and handles edge cases. |
| `CommandLineOptions` | Parses and validates command-line arguments. |
| `Defaults` | Default values used when CLI arguments are not provided. |
| `FileComparer` | Compares files using hashes and computes hash values. |
| `Types.cs` | Contains enumerations, like `HashAlgorithmType`. |

---

##  Synchronization Logic

The core method `SynchronizeInternal()` in `FolderSynchronizer` follows this logic:

1. **Edge Case 1**: If both folders are empty — log the info and exit.
2. **Edge Case 2**: If a file exists in the replica but not in the source — delete it from the replica.
3. **Edge Case 3**: If a file exists in the source but not in the replica — copy it.

---

##  Running the App

You can run the program in two ways:

###  1. Using Default Parameters

If no arguments are passed via the command line, the app will fall back to **default values** defined in the `Defaults.cs` file.

**Default folders:**
- `defaultSource`
- `defaultReplica`
- `Logs`

These folders are expected to be located inside the working directory:  
`bin/Debug/netX.Y/` (e.g., `bin/Debug/net8.0/`)

> **Tip:** You should create these folders manually before running the app from Visual Studio if you're using default values.

###  2. Using Command-Line Parameters
Example: FolderSynchronizer.exe "C:\MySource" "D:\MyReplica" 90 "E:\MyLogs"
