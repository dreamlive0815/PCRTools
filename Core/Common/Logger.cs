using System;

namespace Core.Common
{
    public class Logger
    {

        private static Logger instance;

        public static Logger GetInstance()
        {
            instance = instance ?? new Logger();
            return instance;
        }

        public event Action<string, string> OnDebug;

        public event Action<string, string> OnInfo;

        public event Action<string, string> OnError;

        private Logger()
        {

        }

        public void Debug(string tag, string msg)
        {
            OnDebug?.Invoke(tag, msg);
            Console.WriteLine($"[DEBUG]{msg}");
        }

        public void Info(string tag, string msg)
        {
            OnInfo?.Invoke(tag, msg);
            Console.WriteLine($"[INFO]{msg}");
        }

        public void Error(string tag, string msg)
        {
            OnError?.Invoke(tag, msg);
            Console.WriteLine($"[ERROR]{msg}");
        }
    }
}
