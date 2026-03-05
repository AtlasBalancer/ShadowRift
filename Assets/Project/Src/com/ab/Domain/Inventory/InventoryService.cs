using System;
using com.ab.complexity.core;
using System.Collections.Generic;
using Project.Src.com.ab.Domain.ItemTable;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryService 
    {
        public Dictionary<ResourceDefID, InventoryItemLink> MaterialLinks =
            new Dictionary<ResourceDefID, InventoryItemLink>();

        public Dictionary<ItemDefID, InventoryItemLink> ItemLinks =
            new Dictionary<ItemDefID, InventoryItemLink>();

        public bool HasAndGreaterThan(ResourceDefID id, int than) => 
            MaterialLinks.TryGetValue(id, out var link) && link.Amount.Value > than;

        public InventoryItemLink Get(ResourceDefID id)
        {
            if (!MaterialLinks.TryGetValue(id, out var link))
                throw new ArgumentException($"{nameof(InventoryService)}:: Can't find {id} in {nameof(MaterialLinks)}");

            return link;
        }

        public InventoryItemLink Get(ItemDefID id)
        {
            if (!ItemLinks.TryGetValue(id, out var link))
                throw new ArgumentException($"{nameof(InventoryService)}:: Can't find {id} in {nameof(ItemLinks)}");

            return link;
        }

        public void Add(ItemDefID id, int amount)
        {
            if (!ItemLinks.ContainsKey(id))
            {
                var item = CreateInventoryItem(amount);
                item.Ent.Add(new InventoryItem(id));
                ItemLinks.Add(id, item);
            }
            else
                ChangeAmountInventoryItem(Get(id), amount);
        }

        public void Add(ResourceDefID id, int amount)
        {
            if (!MaterialLinks.ContainsKey(id))
            {
                var item = CreateInventoryItem(amount);
                item.Ent.Add(new InventoryMaterial(id)); 
                MaterialLinks.Add(id, item);
            }
            else
                ChangeAmountInventoryItem(Get(id), amount);
        }

        public InventoryItemLink CreateInventoryItem(int addAmount)
        {
            var amount = new InventoryAmount { Value = addAmount };
            W.Entity ent = W.Entity.New();
            ent.Add(amount);
            return new InventoryItemLink { Ent = ent, Amount = amount };
        }

        public void ChangeAmountInventoryItem(InventoryItemLink source, int addAmount)
        {
            ref var amount = ref source.Amount;
            amount.Value += addAmount;

            if (amount.Value <= 0)
                source.Ent.ApplyTag<Delete>(true);
        }

        public void Remove(ItemDefID id) => 
            ItemLinks.Remove(id);
        
        public void Remove(ResourceDefID id) => 
            MaterialLinks.Remove(id);
    }
}