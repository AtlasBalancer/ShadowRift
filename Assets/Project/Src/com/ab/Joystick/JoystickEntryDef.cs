using System;

namespace com.ab.complexity.core
{
    public class JoystickEntryDef : StaticEntryParamDef<JoystickEntryDef.Settings>,
        IStaticRegisterTypeDef, IStaticUpdateDef, IStaticCreateProtoEntityDef, IStaticContextSetDef
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

        public void RegisterType()
        {
            W.RegisterTagType<JoystickAttachTag>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new JoystickToMovementSystem());
        }

        public void CreateProtoEntities()
        {
            
        }
    }
}