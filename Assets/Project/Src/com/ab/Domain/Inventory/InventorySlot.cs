using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.ItemTable;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryItemLink
    {
        public W.Entity Ent;
        public InventoryAmount Amount;
    }

    public struct InventoryAmount : IComponent
    {
        public int Value;
    }
    
    public readonly struct InventoryMaterial : IComponent
    {
        public readonly ResourceDefID ID;
        public InventoryMaterial(ResourceDefID id) => 
            ID = id;
    }

    public readonly struct InventoryItem : IComponent
    {
        public readonly ItemDefID ID;
        public InventoryItem(ItemDefID id) => ID = id;
    }
}