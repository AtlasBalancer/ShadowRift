using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public struct MovementSamePosition : IComponent
    {
        public Transform UpdateSource;
        public Transform TargetSource;
    }
}