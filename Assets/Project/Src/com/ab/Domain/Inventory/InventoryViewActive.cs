using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Inventory
{
    public readonly struct InventoryViewActive : IEvent
    {
        public readonly bool Active;

        public InventoryViewActive(bool active) =>
            Active = active;
    }
}