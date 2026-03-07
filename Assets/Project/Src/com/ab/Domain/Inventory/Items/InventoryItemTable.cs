using System;
using System.Collections.Generic;
using com.ab.common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.domain.inventory
{
    [CreateAssetMenu(fileName = "InventoryTable", menuName = "com.ab/inventory/table")]
    public class InventoryItemTable : SerializedScriptableObject
    {
        public Dictionary<ItemID, Entry> Entries = new Dictionary<ItemID, Entry>();

        [Serializable]
        public class Entry
        {
            public GameObject Icon;
        }
    }
}