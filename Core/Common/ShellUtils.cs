using System;
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

        private void AssertNoError()
        {
            var err = GetError();
            if (!string.IsNullOrWhiteSpace(err))
                throw new Exception(err);
        }

        public string GetOutput()
        {
            AssertNoError();
            var stream = Process.StandardOutput;
            var output = stream.ReadToEnd();
            return output;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Process?.Dispose();
                    Process = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
