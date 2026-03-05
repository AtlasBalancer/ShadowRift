using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.ItemTable;

namespace Project.Src.com.ab.Domain.Inventory
{
    public struct InventoryAddItem : IEvent
    {
        public ItemDefID ID;
        public int Amount;
    }
    
    public struct InventoryAddMaterial : IEvent
    {
        public ResourceDefID ID;
        public int Amount;
    }
}