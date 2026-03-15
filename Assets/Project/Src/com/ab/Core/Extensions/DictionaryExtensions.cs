using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace com.ab.complexity.core
{
    public static class DictionaryExtensions
    {
        public static (T, TValue) RandVal<T, TValue>(this Dictionary<T, TValue> source)
        {
            var item = source.ElementAt(Random.Range(0, source.Count));
            return (item.Key, item.Value);
        }
    }
}