using System;
using com.ab.complexity.core;
using Project.Src.com.ab.Domain.Collect;

namespace com.ab.domain.collect
{
    public class PlacedEntryDef : StaticEntryParamDef<PlacedEntryDef.Settings>, IStaticRegisterTypeDef,
        IStaticUpdateDef, IStaticCreateEntityDef, IStaticContextSetDef
    {
        [Serializable]
        public class Settings
        {
            // public ItemTable ItemTable;
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
            // W.Context<ItemTable>.Set(Def.ItemTable);
        }

        public void CreateEntities()
        {
        }
    }
}