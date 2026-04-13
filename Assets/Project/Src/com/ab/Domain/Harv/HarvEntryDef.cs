using System;
using com.ab.complexity.core;
using com.ab.core;

namespace com.ab.domain.harv
{
    public class HarvEntryDef : StaticEntryParamDef<HarvEntryDef.Settings>,
        IStaticUpdateDef, IStaticContextSetDef
    {
        [Serializable]
        public class Settings
        {
            public HarvLoadSystem.Settings HarvLoadSystem;
            public HarvSpawnSystem.Settings HarvesterSpawnSystem;
            public HarvCollectSystem.Settings HarvestCollectSystem;
            public HarvFactory.Settings HarvFactory;
        }

        public void RegisterUpdate()
        {
            Sys.Add(new HarvLoadSystem(Def.HarvLoadSystem));
            Sys.Add(new HarvSpawnSystem(Def.HarvesterSpawnSystem));
            // SysReg.Add(new HarvCollectSystem(Def.HarvestCollectSystem));
        }

        public void SetContext()
        {
            W.SetResource(new HarvFactory(Def.HarvFactory));
        }
    }
}