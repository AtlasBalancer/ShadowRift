using UnityEngine;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Common.Movement
{
    public struct MovementSamePositionSmoothChangeTarget : IComponent
    {
        public float Speed;
        public Transform Target;
    }
}