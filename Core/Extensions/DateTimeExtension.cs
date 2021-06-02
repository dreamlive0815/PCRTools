using System;

namespace Core.Extensions
{
    public static class DateTimeExtension
    {
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            var dateTimeOffset = new DateTimeOffset(dateTime);
            var unixTime = dateTimeOffset.ToUnixTimeSeconds();
            return unixTime;
        }
    }
}