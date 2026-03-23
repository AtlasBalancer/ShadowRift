using System;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.domain.harv
{
    public class HarvEntryDef : StaticEntryParamDef<HarvEntryDef.Settings>, IStaticRegisterTypeDef,
        IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public HarvSpawnSysytem.Settings HarvesterSpawnSystem;
            public HarvCollectSystem.Settings HarvestCollectSystem;
        }

        public void RegisterType()
        {
            W.RegisterComponentType<HarvCollector>();
            W.RegisterComponentType<HarvesterSpawner>();
            W.RegisterComponentType<HarvSpawnLoop>();
            W.RegisterComponentType<HarvAvailablePositions>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new HarvSpawnSysytem(Def.HarvesterSpawnSystem));
            SysReg.AddUpdate(new HarvCollectSystem(Def.HarvestCollectSystem));
        }
    }

    public struct HarvesterSpawner : IComponent
    {
        public Timer Timer;
    }
}