using FFS.Libraries.StaticEcs;

namespace com.ab.complexity.core
{
    public struct MovementUpdatePositionSystem : IUpdateSystem
    {
        public void Update() => W.Query.For((ref Ref @ref, ref Position pos) => 
            @ref.Value.position = pos.Value);
    }
}