using System;
using com.ab.common;
using Sirenix.OdinInspector;

namespace com.ab.domain.harv
{
    public class HarvEntryDef : StaticEntryParamDef<HarvEntryDef.Settings>,
        IStaticUpdateDef, IStaticContextSetDef
    {
        HarvLoadSystem load;

        public void SetContext()
        {
            W.SetResource(new HarvFactory(Def.HarvFactory));
        }

        public void RegisterUpdate()
        {
            load = new HarvLoadSystem(Def.HarvLoadSystem);
            Sys.Add(load);
            Sys.Add(new HarvSpawnSystem(Def.HarvesterSpawnSystem));
            Sys.Add(new HarvCollectSystem(Def.HarvestCollectSystem));
        }

        [Button]
        public void Report()
        {
            load.Report();
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

        [Serializable]
        public class Settings
        {
            public HarvLoadSystem.Settings HarvLoadSystem;
            public HarvSpawnSystem.Settings HarvesterSpawnSystem;
            public HarvCollectSystem.Settings HarvestCollectSystem;
            public HarvFactory.Settings HarvFactory;
        }
    }
}