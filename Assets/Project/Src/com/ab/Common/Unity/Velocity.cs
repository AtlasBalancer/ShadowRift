using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.complexity.core
{
    public struct Velocity : IComponent
    {
        public float Magnitude;
        public Vector2 Value;
    }
}