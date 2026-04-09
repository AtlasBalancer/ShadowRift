using Project.Src.com.ab.Common.Movement;
using UnityEngine;

namespace com.ab.complexity.core
{
    [CreateAssetMenu(fileName = "#Name#MoveEntryDef", menuName = "com.ab/scene/move")]
    public class MovementEntryDef : ScriptableObject, IStaticUpdateDef
    {
        public void RegisterUpdate()
        {
            SysReg.Add(new MovementInitSystem());
            SysReg.Add(new MovementVelocitySystem());
            SysReg.Add(new MovementUpdatePositionSystem());
            SysReg.Add(new MovementSamePositionSystem());
            SysReg.Add(new MovementDirectionSystem());
            SysReg.Add(new MovementAnimationLocomotionSystem());
        }
    }
}