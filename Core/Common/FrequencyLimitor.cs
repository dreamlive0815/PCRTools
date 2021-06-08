using System;
using System.Collections.Generic;

namespace Core.Common
{
    public class FrequencyLimitor
    {

        class ObjCacheInfo
        {
            public object Obj { get; set; }

            public int LifeTimeMS { get; set; }

            public DateTime lastHitTime { get; set; } = DateTime.Now;

            public bool Expired
            {
                get
                {
                    var span = DateTime.Now - lastHitTime;
                    return span.TotalMilliseconds > LifeTimeMS;
                }
            }
        }

        private static Dictionary<string, ObjCacheInfo> objCacheContainer = new Dictionary<string, ObjCacheInfo>();

        public static T ParseObjWithCache<T>(string key, int liftTimeMS, Func<string, T> parsor)
        {
            if (objCacheContainer.ContainsKey(key))
            {
                var info = objCacheContainer[key];
                if (!info.Expired)
                    return (T)info.Obj;
            }
            var obj = parsor(key);
            var newInfo = new ObjCacheInfo()
            {
                Obj = obj,
                LifeTimeMS = liftTimeMS,
            };
            objCacheContainer[key] = newInfo;
            return obj;
        }

        public static void ClearObjCache(string key)
        {
            objCacheContainer.Remove(key);
        }

        public static void ClearAllObjCache()
        {
            objCacheContainer.Clear();
        }
    }
}
