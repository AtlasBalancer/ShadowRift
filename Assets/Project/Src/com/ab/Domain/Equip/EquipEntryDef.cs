using System;
using com.ab.complexity.core;

namespace com.ab.domain.equip
{
    public class EquipEntryDef : StaticEntryParamDef<EquipEntryDef.Settings>, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public EquipPuppetSystem.Settings EquipPuppetSystem;
            public EquipUnitSystem.Settings EquipUnitSystem;
        }

        public void RegisterUpdate()
        {
            SysReg.Add(new EquipPuppetSystem(Def.EquipPuppetSystem));
            SysReg.Add(new EquipUnitSystem(Def.EquipUnitSystem));
        }
    }
}