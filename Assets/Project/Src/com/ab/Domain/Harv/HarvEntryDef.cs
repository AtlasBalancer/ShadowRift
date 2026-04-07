using System;
using com.ab.common;
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
            public HarvSpawnSystem.Settings HarvesterSpawnSystem;
            public HarvCollectSystem.Settings HarvestCollectSystem;
        }

        public void RegisterType()
        {
            W.RegisterComponentType<HarvCollectorRef>();
            W.RegisterComponentType<HarvesterSpawner>();
            W.RegisterComponentType<HarvSpawnLoop>();
            W.RegisterComponentType<HarvAvailablePositions>();
            W.RegisterComponentType<HarvRef>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new HarvSpawnSystem(Def.HarvesterSpawnSystem));
            SysReg.AddUpdate(new HarvCollectSystem(Def.HarvestCollectSystem));
        }
    }

    public struct HarvesterSpawner : IComponent
    {
        public Timer Timer;
    }
}