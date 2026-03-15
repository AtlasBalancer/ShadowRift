using System;
using com.ab.common;
using com.ab.complexity.core;

namespace Project.Src.com.ab.Domain.ItemTable
{
    public class DropTableService
    {
        [Serializable]
        public class Settings
        {
            public ItemsDef Itemses;
            
            public DropTable DropTable;

            public CollectItemSo Collect;
            public EquipmentSo Equipment;
            public CraftItemSo Craft;
            
            public InventoryCardSo InventoryCards;
            public InventoryMaterialSo InventoryMaterial;
            public InventoryItemSo InventoryItem;
        }
        
        public DropTableService(Settings def)
        {
            Def = def;
        }

        public Settings Def { get; private set; }

        public DropItem[] GetDrop(W.Entity id)
        {
            // if (!Def.DropTable.Runtime.TryGetValue(id, out var drop))
                // throw new ArgumentException($"{nameof(DropTableService)}:: Can't find {id} in {nameof(Def.DropTable)}");

            throw new NotImplementedException("Cahnge ID to entity");
        }
    }
}