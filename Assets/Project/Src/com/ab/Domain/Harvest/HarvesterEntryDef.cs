using System;
using com.ab.complexity.core;
using com.ab.complexity.harvestable;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Harvest
{
    public class HarvesterEntryDef : StaticEntryParamDef<HarvesterEntryDef.Settings>, IStaticRegisterTypeDef,
        IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public HarvesterSpawnSystem.Settings HarvesterSpawnSystem;
            public HarvestCollectSystem.Settings HarvestCollectSystem;
        }

        public void RegisterType()
        {
            W.RegisterComponentType<Harvestable>();
            W.RegisterComponentType<HarvestCollector>();
            W.RegisterComponentType<HarvesterSpawner>();
            W.RegisterComponentType<HarvestSpawnLoopDef>();
            W.RegisterComponentType<HarvAvailablePositions>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new HarvesterSpawnSystem(Def.HarvesterSpawnSystem));
            SysReg.AddUpdate(new HarvestCollectSystem(Def.HarvestCollectSystem));
        }
    }

    public struct HarvesterSpawner : IComponent
    {
        public Timer Timer;
    }
}