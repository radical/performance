﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorld
{
    /// <summary>
    /// An ITestOutputHelper implementation that logs to a file
    /// </summary>
    public class FileTestOutputHelper : ITestOutputHelper, IDisposable
    {
        readonly StreamWriter _logWriter;
        readonly object _lock;

        public FileTestOutputHelper(string logFilePath, FileMode fileMode = FileMode.Create)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
            FileStream fs = new FileStream(logFilePath, fileMode);
            _logWriter = new StreamWriter(fs);
            _logWriter.AutoFlush = true;
            _lock = new object();
        }

        public void WriteLine(string message)
        {
            lock (_lock)
            {
                _logWriter.WriteLine(message);
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                _logWriter.Dispose();
            }
        }
    }
}
