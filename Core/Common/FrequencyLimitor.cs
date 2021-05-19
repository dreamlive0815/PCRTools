using System;

namespace Core.Common
{
    public class FrequencyLimitor
    {

        private DateTime lastHitTime;

        public FrequencyLimitor(int milliseconds)
        {
            CD = milliseconds;
        }

        /// <summary>
        /// 单位: 毫秒
        /// </summary>
        public int CD { get; private set; }

        public bool CanHit
        {
            get
            {
                var span = DateTime.Now - lastHitTime;
                return span.TotalMilliseconds >= CD;
            }
        }

        public void Hit()
        {
            lastHitTime = DateTime.Now;
        }
    }

    public class FrequencyLimitGetter<T>
    {
        private FrequencyLimitor limitor;
        private Func<T> getter;
        private T val;
        private bool vis;

        public FrequencyLimitGetter(int milliseconds, Func<T> rawGetter)
        {
            limitor = new FrequencyLimitor(milliseconds);
            CD = milliseconds;
            getter = rawGetter;
            vis = false;
        }

        public int CD { get; private set; }

        public T Get()
        {
            if (vis && !limitor.CanHit)
                return val;
            val = getter();
            limitor.Hit();
            vis = true;
            return val;
        }
    }
}
