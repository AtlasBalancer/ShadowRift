using System;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.domain.craft;
using Project.Src.com.ab.Domain.Unit.Items;

namespace Project.Src.com.ab.Domain.ItemTable
{
    [CreateAssetMenu(fileName = "#Name#ItemTableEntryDef", menuName = "com.ab/itemTable/def")]
    public class ItemTableEntryDef : StaticEntrySOParamDef<ItemTableEntryDef.Settings>, IStaticContextSetDef
    {
        [Serializable]
        public class Settings
        {
            public ItemTableService.Settings ItemTableSystem;
        }

        public void SetContext()
        {
            W.Context<ItemTableService>.Set(new ItemTableService(Def.ItemTableSystem));
        }
    }

    public class ItemTableService
    {
        [Serializable]
        public class Settings
        {
            public ItemsDef Itemses;
            
            public DropTableSo DropTable;

            public HarvestItemSo Harvest;
            public CollectItemSo Collect;
            public EquipmentSo Equipment;
            public CraftItemSo Craft;
            
            public InventoryCardSo InventoryCards;
            public InventoryMaterialSo InventoryMaterial;
            public InventoryItemSo InventoryItem;
        }
        
        public ItemTableService(Settings def)
        {
            Def = def;
        }

        public Settings Def { get; private set; }
    }
}