using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem
{

    public class EventMgr
    {

        public static void RegisterListener(string key, Action<object> handler)
        {
            Default.Register(key, handler);
        }

        public static void FireEvent(string key, object arg)
        {
            Default.Fire(key, arg);
        }

        public static void RemoveListeners(string key)
        {
            Default.Remove(key);
        }

        private static EventMgr instance;

        public static EventMgr Default { get { return instance = instance ?? new EventMgr(); } }

        private Dictionary<string, Action<object>> handlers = new Dictionary<string, Action<object>>();


        private EventMgr()
        {
        }

        public void Register(string key, Action<object> handler)
        {
            if (handlers.ContainsKey(key))
                handlers[key] += handler;
            else
                handlers[key] = handler;
        }

        public void Fire(string key, object arg)
        {
            if (!handlers.ContainsKey(key))
            {
                //Console.WriteLine($"The event named: {key} doesn't exist");
                return;
            }
            var handler = handlers[key];
            handler(arg);
        }

        public void Remove(string key)
        {
            handlers.Remove(key);
        }

        public void Clear()
        {
            handlers.Clear();
        }
    }
}
