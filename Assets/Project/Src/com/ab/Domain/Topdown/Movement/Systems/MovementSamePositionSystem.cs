using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.complexity
{
    public class MovementSamePositionSystem : IUpdateSystem
    {
        public void Update() =>
            W.Query.For((ref MovementSamePosition item) =>
                item.UpdateSource.position =
                    item.UpdateSource.position
                        .ChangeXY(item.PositionSource.position));
    }
}