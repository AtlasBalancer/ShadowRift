using System;
using com.ab.common;

namespace com.ab.domain.equip
{
    public class EquipEntryDef : StaticEntryParamDef<EquipEntryDef.Settings>, IStaticUpdateDef
    {
        public void RegisterUpdate()
        {
            Sys.Add(new EquipPuppetSystem(Def.EquipPuppetSystem));
            Sys.Add(new EquipUnitSystem(Def.EquipUnitSystem));
        }

        [Serializable]
        public class Settings
        {
            public EquipPuppetSystem.Settings EquipPuppetSystem;
            public EquipUnitSystem.Settings EquipUnitSystem;
        }
    }
}