using System.Collections.Generic;
using com.ab.domain.craft;
using Sirenix.OdinInspector;

namespace Project.Src.com.ab.Domain.ItemTable
{
    public class CraftItemSo
    {
        [Searchable]
        public class Entry
        {
            public List<Price> Price;
        }
        
        public Dictionary<CraftID, Entry> Entries = new();
    }
}