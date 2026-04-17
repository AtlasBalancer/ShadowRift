using com.ab.complexity.features.player;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.complexity.core
{
    public class MovementDirectionSystem : ISystem
    {
        public void Update()
        {
            foreach (var ent in W.Query<All<Direction, LogicRender>>().Entities())
            {
                var dir = ent.Read<Direction>();
                var @ref = ent.Read<LogicRender>();

                if (dir.Value.x != 0)
                    @ref.Value.transform.localScale = new Vector3(dir.Value.x, 1, 1);
            }
        }
    }
}