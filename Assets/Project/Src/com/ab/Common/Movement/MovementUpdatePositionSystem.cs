using com.ab.common;
using FFS.Libraries.StaticEcs;

namespace com.ab.complexity.core
{
    public struct MovementUpdatePositionSystem : ISystem
    {
        public void Update()
        {
            W.Query<All<Ref, Position>>().For((ref Ref @ref, ref Position pos) =>
                @ref.Val.position = pos.Val);
        }
    }
}