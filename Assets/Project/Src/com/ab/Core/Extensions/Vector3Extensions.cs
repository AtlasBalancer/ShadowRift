using UnityEngine;

namespace com.ab.core
{
    public static class Vector3Extensions
    {
        public static Vector3 ChangeXY(this Vector3 source, Vector3 predicate)
        {
            return new Vector3(predicate.x, predicate.y, source.z);
        }
    }
}