using com.ab.core;
using UnityEngine;

namespace com.ab.complexity.core
{
    [CreateAssetMenu(fileName = "#Name#MoveEntryDef", menuName = "com.ab/scene/move")]
    public class MovementEntryDef : ScriptableObject, IStaticUpdateDef
    {
        public void RegisterUpdate()
        {
            Sys.Add(new MovementInitSystem());
            Sys.Add(new MovementVelocitySystem());
            Sys.Add(new MovementUpdatePositionSystem());
            Sys.Add(new MovementSamePositionSystem());
            Sys.Add(new MovementDirectionSystem());
            Sys.Add(new MovementAnimationLocomotionSystem());
        }
    }
}