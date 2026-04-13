using com.ab.complexity.core;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Common.Movement;
using UnityEngine;

namespace com.ab.complexity
{
    public class MovementSamePositionSystem : ISystem
    {
        public void Update()
        {
            foreach (var ent in W.Query<All<MovementSamePosition>>().Entities())
            {
                var item = ent.Ref<MovementSamePosition>();

                if (ent.Has<MovementSamePositionSmoothChangeTarget>())
                {
                    var another = ent.Ref<MovementSamePositionSmoothChangeTarget>();

                    if (Vector3.Distance(item.UpdateSource.position, another.Target.position) < 0.01)
                    {
                        ent.Delete<MovementSamePositionSmoothChangeTarget>();
                        continue;
                    }
                    
                    var newPosition = Vector3.MoveTowards(item.UpdateSource.position, another.Target.position,
                        another.Speed * Time.deltaTime);

                    item.UpdateSource.position = newPosition;

                    continue;
                }


                if (item.UpdateSource == null || item.TargetSource == null)
                    continue;

                item.UpdateSource.position =
                    item.UpdateSource.position
                        .ChangeXY(item.TargetSource.position);
            }
        }
    }
}