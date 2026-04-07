using System;
using com.ab.complexity.core;

namespace com.ab.domain.equip
{
    public class EquipEntryDef : StaticEntryParamDef<EquipEntryDef.Settings>,
        IStaticRegisterTypeDef, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public EquipPuppetSystem.Settings EquipPuppetSystem;
            public EquipUnitSystem.Settings EquipUnitSystem;
        }

        public void RegisterType()
        {
            W.RegisterTagType<EquipTag>();
            
            W.RegisterComponentType<EquipUnitRef>();

            W.Events.RegisterEventType<EquipUnitRegisterEvent>();
            W.Events.RegisterEventType<EquipUnSetEvent>();
            W.Events.RegisterEventType<EquipSetEvent>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new EquipPuppetSystem(Def.EquipPuppetSystem));
            SysReg.AddUpdate(new EquipUnitSystem(Def.EquipUnitSystem));
            
        }
    }
}