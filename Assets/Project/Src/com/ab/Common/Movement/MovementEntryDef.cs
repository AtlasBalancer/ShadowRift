using UnityEngine;

namespace com.ab.complexity.core
{
    [CreateAssetMenu(fileName = "#Name#MoveEntryDef", menuName = "com.ab/scene/move")]
    public class MovementEntryDef : ScriptableObject, 
        IStaticRegisterTypeDef, IStaticUpdateDef
    {
        public void RegisterType()
        {
            W.RegisterComponentType<MovementEntry>();
            W.RegisterComponentType<MovementSamePosition>();
            
            W.RegisterTagType<Movement>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new MovementInitSystem());
            SysReg.AddUpdate(new MovementVelocitySystem());
            SysReg.AddUpdate(new MovementUpdatePositionSystem());
            SysReg.AddUpdate(new MovementSamePositionSystem());
            SysReg.AddUpdate(new MovementDirectionSystem());
            SysReg.AddUpdate(new MovementAnimationLocomotionSystem());
        }
    }
}