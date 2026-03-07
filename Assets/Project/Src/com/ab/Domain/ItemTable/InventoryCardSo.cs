using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.ItemTable
{
    [CreateAssetMenu(fileName = "#Name#InventoryCardDef", menuName = "com.ab/drop/inventory/card")]
    public class InventoryCardSo : SerializedScriptableObject
    {
        [Serializable]
        public struct Entry
        {
            public string Title;
            public string Decription;
            public Image Icon;
        }

        // public Dictionary<ItemDefID, Entry> Items = new Dictionary<ItemDefID, Entry>();
    }
}