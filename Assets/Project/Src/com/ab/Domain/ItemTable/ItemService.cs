using System;
using System.Collections.Generic;
using System.Linq;
using com.ab.common;
using com.ab.item;

namespace com.ab.domain.item
{
    public class ItemService
    {
        readonly Dictionary<IDEntSo, ItemEntry> _items;
        readonly Dictionary<IDEntSo, DropEntry> _drop;

        public ItemService(ItemTable[] item, DropTable[] drop)
        {
            _items = item.SelectMany(item => item.Entries)
                .ToDictionary(item => item.Key, item => item.Value);
            
            _drop = drop.SelectMany(item => item.Entries)
                .ToDictionary(item => item.Key, item => item.Value);
        }

        public ItemEntry Get(IDEntSo id)
        {
            if (!_items.TryGetValue(id, out var itemDef))
                throw new ArgumentException(
                    $"{nameof(ItemService)}:: Can't find {id.ID} in ItemTable");

            return itemDef;
        }
    }
}