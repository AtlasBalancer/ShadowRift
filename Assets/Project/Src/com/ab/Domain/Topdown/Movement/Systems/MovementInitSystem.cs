using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.complexity.core
{
    public class MovementInitSystem : IUpdateSystem
    {
        public void Update()
        {
            All<MovementEntry> all = default;
            None<Direction, Velocity, Position> filter = default;
            var with = With.Create(all, filter);

            foreach (var entity in W.Query.Entities(with))
            {
                var @ref = entity.Ref<Ref>();
                var def = entity.Ref<MovementEntry>();

                if (!entity.HasAllOf<Position>())
                    entity.Add<Position>().Value = @ref.Value.position;

                if (!entity.HasAllOf<Velocity>())
                    entity.Add<Velocity>();

                if (!entity.HasAllOf<Direction>())
                    entity.Add<Direction>().Value = Vector3.zero;
            }
        }
    }
}