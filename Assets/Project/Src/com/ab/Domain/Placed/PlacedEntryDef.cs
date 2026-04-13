using System;
using com.ab.complexity.core;
using com.ab.core;
using Project.Src.com.ab.Domain.Collect;

namespace com.ab.domain.collect
{
    public class PlacedEntryDef : StaticEntryParamDef<PlacedEntryDef.Settings>,
        IStaticUpdateDef, IStaticCreateProtoEntityDef, IStaticContextSetDef
    {
        [Serializable]
        public class Settings
        {
            // public ItemTable ItemTable;
            public PlacedToInventorySystem.Settings CollectToInventorySystem;
            public PlacedSpawnSystem.Settings CollectSpawnSystem;
        }

        public void RegisterUpdate()
        {
            Sys.Add(new PlacedToInventorySystem(Def.CollectToInventorySystem));
            Sys.Add(new PlacedSpawnSystem(Def.CollectSpawnSystem));
        }

        public void SetContext()
        {
            // W.Context<ItemTable>.Set(Def.ItemTable);
        }

        public void CreateProtoEntities() { }
    }
}