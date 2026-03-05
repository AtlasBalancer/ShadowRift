using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryDeleteSystem : IInitSystem, IUpdateSystem
    {
        InventoryService _inventory;

        public void Init()
        {
            _inventory = W.Context<InventoryService>.Get();
        }

        public void Update()
        {
            foreach (var ent in W.Query.Entities<All<InventoryMaterial>, TagAll<Delete>>())
            {
                var id = ent.Ref<InventoryMaterial>().ID;
                _inventory.Remove(id);
                ent.Destroy();
            }
            
            foreach (var ent in W.Query.Entities<All<InventoryItem>, TagAll<Delete>>())
            {
                var id = ent.Ref<InventoryItem>().ID;
                _inventory.Remove(id);
                ent.Destroy();
            }
        }
    }
}