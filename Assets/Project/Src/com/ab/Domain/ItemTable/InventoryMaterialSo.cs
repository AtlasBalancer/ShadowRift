using System;
using System.Collections.Generic;
using Project.Src.com.ab.Domain.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Src.com.ab.Domain.ItemTable
{
    [CreateAssetMenu(fileName = "#Name#InventoryMaterialDef", menuName = "com.ab/drop/inventory/material")]
    public class InventoryMaterialSo : SerializedScriptableObject
    {
        [Serializable]
        public struct Entry
        {
            public InvItemView Prefab;
        }

        public Dictionary<ResourceDefID, Entry> Entries = new Dictionary<ResourceDefID, Entry>();
    }
}