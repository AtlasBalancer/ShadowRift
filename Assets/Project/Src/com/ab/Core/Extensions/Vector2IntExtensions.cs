using UnityEngine;

namespace com.ab.core
{
    public static class Vector2IntExtensions
    {
        public static int Rand(this Vector2Int source) => 
            Random.Range(source.x, source.y);
    }
}