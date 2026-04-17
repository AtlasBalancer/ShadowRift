using System.Collections.Generic;
using UnityEngine;

namespace com.ab.core
{
    public static class ListExtensions
    {
        public static T RandAndRemove<T>(this List<T> source)
        {
            var index = Random.Range(0, source.Count);
            var item = source[index];
            source.RemoveAt(index);
            return item;
        }

        public static T Rand<T>(this List<T> source)
        {
            return source[Random.Range(0, source.Count)];
        }
    }
}