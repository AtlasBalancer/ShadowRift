using UnityEngine;

namespace com.ab.core
{
    public static class Vector2IntExtensions
    {
        public static int Rand(this Vector2Int source)
        {
            return Random.Range(source.x, source.y);
        }
    }
}