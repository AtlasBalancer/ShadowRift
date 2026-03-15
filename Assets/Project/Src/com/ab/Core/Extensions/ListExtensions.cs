using System.Collections.Generic;

namespace com.ab.complexity.core
{
    public static class ListExtensions
    {
        public static T RandAndRemove<T>(this List<T> source)
        {
            var index = UnityEngine.Random.Range(0, source.Count);
            T item = source[index];
            source.RemoveAt(index);
            return item;
        }

        public static T Rand<T>(this List<T> source) => 
            source[UnityEngine.Random.Range(0, source.Count)];
    }
}