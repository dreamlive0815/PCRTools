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
}
