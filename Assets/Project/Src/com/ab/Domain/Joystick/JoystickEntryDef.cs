using System;
using com.ab.common;

namespace com.ab.domain.joystick
{
    public class JoystickEntryDef : StaticEntryParamDef<JoystickEntryDef.Settings>,
        IStaticUpdateDef, IStaticCreateProtoEntityDef, IStaticContextSetDef
    {
        public void SetContext()
        {
            JoystickToMovementSystem.Context.ContextSet(Def.JoystickToDirectionSys);
        }

        public void CreateProtoEntities()
        {
        }

        public void RegisterUpdate()
        {
            Sys.Add(new JoystickToMovementSystem());
        }

        [Serializable]
        public class Settings
        {
            public JoystickToMovementSystem.Context JoystickToDirectionSys;
        }
    }
}