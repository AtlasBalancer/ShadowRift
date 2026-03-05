using UnityEngine;

namespace com.ab.complexity.core
{
    public static class Vector2Extensions
    {
        public static float Rand(this Vector2 source) =>
            Random.Range(source.x, source.y);

        public static bool RandHappen(this Vector2 source) =>
            Random.Range(0, 1) <= source.Rand();
    }
}