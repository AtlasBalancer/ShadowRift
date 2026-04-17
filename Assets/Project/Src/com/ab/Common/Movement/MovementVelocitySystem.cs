using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public readonly struct MovementVelocitySystem : ISystem
    {
        public const float GAP = 0.01f;

        public void Update()
        {
            W.Query<All<Position, Velocity, Direction, MovementEntry>>()
                .For((World<WT>.Entity ent, ref Position pos, ref Velocity vel, ref Direction dir,
                    ref MovementEntry def) =>
                {
                    pos.Val += dir.Value * (vel.Magnitude * def.Speed * Time.deltaTime);
                    ent.Apply<Movement>(vel.Magnitude > GAP);
                });
        }
    }
}