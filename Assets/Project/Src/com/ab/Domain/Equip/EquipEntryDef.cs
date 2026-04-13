using System;
using com.ab.complexity.core;
using com.ab.core;

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
            Sys.Add(new EquipPuppetSystem(Def.EquipPuppetSystem));
            Sys.Add(new EquipUnitSystem(Def.EquipUnitSystem));
        }
    }
}