using System;

namespace Core.Extensions
{
    public static class StringExtension
    {

        public static string LimitLength(this string str, int length)
        {
            if (str.Length <= length)
                return str;
            else
                return str.Substring(0, length) + "...";
        }
    }
}
