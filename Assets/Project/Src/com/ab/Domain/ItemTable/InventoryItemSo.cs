using System;
using System.Collections.Generic;
using Project.Src.com.ab.Domain.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Src.com.ab.Domain.ItemTable
{
    [CreateAssetMenu(fileName = "#Name#InventoryItemDef", menuName = "com.ab/drop/inventory/item")]
    public class InventoryItemSo : SerializedScriptableObject
    {
        [Serializable]
        public struct Entry
        {
            public InventoryItemMono Prefab;
        }

        public Dictionary<ItemDefID, Entry> Items = new();
    }
}