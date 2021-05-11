using System;
using System.Collections.Generic;

namespace Core.Extensions
{
    public static class DictionaryExtension
    {

        public static T2 Get<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key)
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            else
                return default(T2);
        }
    }
}
