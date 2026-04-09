using System;

namespace com.ab.complexity.core
{
    public class JoystickEntryDef : StaticEntryParamDef<JoystickEntryDef.Settings>, 
        IStaticUpdateDef, IStaticCreateProtoEntityDef, IStaticContextSetDef
    {
        [Serializable]
        public class Settings
        {
            public JoystickToMovementSystem.Context JoystickToDirectionSys;
        }
        
        public void SetContext()
        {
            JoystickToMovementSystem.Context.ContextSet(Def.JoystickToDirectionSys);
        }

        public void RegisterUpdate()
        {
            SysReg.Add(new JoystickToMovementSystem());
        }

        public void CreateProtoEntities()
        {
            
        }
    }
}