using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.complexity.core
{
    public readonly struct MovementVelocitySystem : IUpdateSystem
    {
        public const float GAP = 0.01f;

        public void Update() =>
            W.Query.For((W.Entity ent, ref Position pos, ref Velocity vel, ref Direction dir, ref MovementEntry def) =>
            {
                pos.Value += dir.Value * (vel.Magnitude * def.Speed * Time.deltaTime);
                ent.ApplyTag<Movement>(vel.Magnitude > GAP);
            });
    }
}