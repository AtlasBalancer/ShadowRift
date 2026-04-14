using System;
using com.ab.complexity.core;
using com.ab.core;
using Sirenix.OdinInspector;

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

        HarvLoadSystem load;

        public void RegisterUpdate()
        {
            load = new HarvLoadSystem(Def.HarvLoadSystem);
            Sys.Add(load);
            Sys.Add(new HarvSpawnSystem(Def.HarvesterSpawnSystem));
            Sys.Add(new HarvCollectSystem(Def.HarvestCollectSystem));
        }

        public void SetContext()
        {
            W.SetResource(new HarvFactory(Def.HarvFactory));
        }

        [Button]
        public void Save()
        {
            load.Save();
        }

        [Button]
        public void Load()
        {
            load.Load();
        }
    }
}