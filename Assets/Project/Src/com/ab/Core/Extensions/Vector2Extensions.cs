using UnityEngine;

namespace com.ab.core
{
    public static class Vector2Extensions
    {
        public static float Rand(this Vector2 source)
        {
            return Random.Range(source.x, source.y);
        }

        public static bool RandHappen(this Vector2 source)
        {
            return Random.Range(0, 1) <= source.Rand();
        }
    }
}