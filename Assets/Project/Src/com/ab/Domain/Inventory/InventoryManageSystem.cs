using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryManageSystem : IInitSystem, IUpdateSystem
    {
        InventoryService _inventory;
        EventReceiver<T, InventoryAddMaterial> _addMaterialReceiver;

        EventReceiver<T, InventoryAddItem> _addItemReceiver;

        public void Init()
        {
            _inventory = W.Context<InventoryService>.Get();

            _addMaterialReceiver = W.Events.RegisterEventReceiver<InventoryAddMaterial>();
            _addItemReceiver = W.Events.RegisterEventReceiver<InventoryAddItem>();
        }

        public void Update()
        {
            foreach (var @event in _addMaterialReceiver) 
                _inventory.Add(@event.Value.ID, @event.Value.Amount);

            foreach (var @event in _addItemReceiver) 
                _inventory.Add(@event.Value.ID, @event.Value.Amount);
        }
    }
}