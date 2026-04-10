using System;
using com.ab.common;
using com.ab.complexity.core;

namespace com.ab.domain.harv
{
    public class HarvEntryDef : StaticEntryParamDef<HarvEntryDef.Settings>,
        IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public HarvLoadSystem.Settings HarvLoadSystem;
            public HarvSpawnSystem.Settings HarvesterSpawnSystem;
            public HarvCollectSystem.Settings HarvestCollectSystem;
        }

        public void RegisterUpdate()
        {
            SysReg.Add(new HarvLoadSystem(Def.HarvLoadSystem));
            // SysReg.Add(new HarvSpawnSystem(Def.HarvesterSpawnSystem));
            // SysReg.Add(new HarvCollectSystem(Def.HarvestCollectSystem));
        }
    }
}