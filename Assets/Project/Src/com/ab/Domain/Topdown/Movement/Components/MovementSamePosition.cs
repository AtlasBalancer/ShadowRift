using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.complexity
{
    public struct MovementSamePosition : IComponent
    {
        public Transform UpdateSource;
        public Transform PositionSource;
    }
}