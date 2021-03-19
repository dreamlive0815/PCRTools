using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (args.ReadLineCallBack != null)
            {
                var outStream = proc.StandardOutput;
                string line;
                while (true)
                {
                    line = outStream.ReadLine();
                    if (line == null)
                        break;
                    args.ReadLineCallBack(line);
                }
            }
            else
            {

            }

            return new ShellResult();
        }
    }

    public class ShellArgs
    {
        public string FileName { get; set; }

        public string Arguments { get; set; }

        public bool IgnoreOutput { get; set; } = false;

        public Action<string> ReadLineCallBack { get; set; }
    }

    public class ShellResult
    {
        public string Output { get; set; }

        //public Task
    }
}
