using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public class MovementInitSystem : ISystem
    {
        public void Update()
        {
            foreach (var entity in W.Query<
                             All<MovementEntry>,
                             None<Direction, Velocity, Position>>()
                         .Entities())
            {
                var @ref = entity.Ref<Ref>();
                var def = entity.Ref<MovementEntry>();

                if (!entity.Has<Position>())
                    entity.Add<Position>().Val = @ref.Val.position;

                if (!entity.Has<Velocity>())
                    entity.Add<Velocity>();

                if (!entity.Has<Direction>())
                    entity.Add<Direction>().Value = Vector3.zero;
            }
        }
    }
}