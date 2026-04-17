using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public struct MovementSamePositionSmoothChangeTarget : IComponent
    {
        public float Speed;
        public Transform Target;
    }
}