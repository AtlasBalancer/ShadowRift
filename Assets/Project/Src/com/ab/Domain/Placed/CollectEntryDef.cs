using System;
using com.ab.complexity.core;
using Project.Src.com.ab.Domain.Collect;
using UnityEngine.Pool;

namespace com.ab.domain.collect
{
    public class CollectEntryDef : StaticEntryParamDef<CollectEntryDef.Settings>, IStaticRegisterTypeDef,
        IStaticUpdateDef, IStaticCreateEntityDef
    {
        [Serializable]
        public class Settings
        {
            public CollectToInventorySystem.Settings CollectToInventorySystem;
            public CollectSpawnSystem.Settings CollectSpawnSystem;
        }

        public void RegisterType()
        {
            W.RegisterComponentType<CollectorToInventory>();
            
            W.Events.RegisterEventType<CollectSpawn>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new CollectToInventorySystem(Def.CollectToInventorySystem));
            Sys.AddUpdate(new CollectSpawnSystem(Def.CollectSpawnSystem));
        }

        public void CreateEntities()
        {
        }
    }
}