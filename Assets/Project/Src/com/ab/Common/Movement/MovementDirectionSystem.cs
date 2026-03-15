using UnityEngine;
using FFS.Libraries.StaticEcs;
using com.ab.complexity.features.player;

namespace com.ab.complexity.core
{
    public class MovementDirectionSystem : IUpdateSystem
    {
        public void Update()
        {
            W.Query.For((ref Direction dir, ref LogicRender @ref) =>
            {
                if (dir.Value.x != 0) 
                    @ref.Value.transform.localScale = new Vector3(dir.Value.x, 1, 1);
            });
        }
    }
}