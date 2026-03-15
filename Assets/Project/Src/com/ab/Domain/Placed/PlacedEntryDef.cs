using System;
using com.ab.complexity.core;
using com.ab.domain.placed;
using Project.Src.com.ab.Domain.Collect;
using UnityEngine.Pool;

namespace com.ab.domain.collect
{
    public class PlacedEntryDef : StaticEntryParamDef<PlacedEntryDef.Settings>, IStaticRegisterTypeDef,
        IStaticUpdateDef, IStaticCreateEntityDef, IStaticContextSetDef
    {
        [Serializable]
        public class Settings
        {
            public PlacedItemTable ItemTable;
            public PlacedToInventorySystem.Settings CollectToInventorySystem;
            public PlacedSpawnSystem.Settings CollectSpawnSystem;
        }

        public void RegisterType()
        {
            W.RegisterComponentType<PlacedToInventory>();
            W.RegisterTagType<PlacedSpawnByDropTable>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new PlacedToInventorySystem(Def.CollectToInventorySystem));
            SysReg.AddUpdate(new PlacedSpawnSystem(Def.CollectSpawnSystem));
        }

        public void SetContext()
        {
            W.Context<PlacedItemTable>.Set(Def.ItemTable);
        }

        public void CreateEntities()
        {
        }
    }
}