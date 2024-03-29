﻿using System;
using System.Diagnostics;

namespace Core.Common
{
    public class ShellUtils
    {

        public static ShellResult DoShell(string fileName, string args)
        {
            return DoShell(new ShellArgs()
            {
                FileName = fileName,
                Arguments = args,
            });
        }

        public static ShellResult DoShell(ShellArgs args)
        {
            var ignoreOutput = args.IgnoreOutput;
            var startInfo = new ProcessStartInfo()
            {
                FileName = args.FileName,
                Arguments = args.Arguments,
                WindowStyle = ProcessWindowStyle.Minimized,
                CreateNoWindow = true,
                UseShellExecute = ignoreOutput,
                RedirectStandardOutput = !ignoreOutput,
                RedirectStandardError = !ignoreOutput,
            };

            var proc = Process.Start(startInfo);
            if (ignoreOutput)
                return null;

            return new ShellResult(proc);
        }
    }

    public class ShellArgs
    {
        public string FileName { get; set; }

        public string Arguments { get; set; }

        public bool IgnoreOutput { get; set; } = false;
    }

    public class ShellResult : IDisposable
    {

        public ShellResult(Process process)
        {
            Process = process;
        }

        public Process Process { get; private set; }

        public string GetError()
        {
            var stream = Process.StandardError;
            var err = stream.ReadToEnd();
            return err;
        }

        public string GetOutput()
        {
            var err = GetError();
            if (!string.IsNullOrWhiteSpace(err))
                throw new Exception(err);
            var stream = Process.StandardOutput;
            var output = stream.ReadToEnd();
            return output;
        }

        public void Dispose()
        {
            Process?.Dispose();
            Process = null;
        }
    }
}
