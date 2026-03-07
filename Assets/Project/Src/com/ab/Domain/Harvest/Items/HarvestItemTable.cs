using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Project.Src.com.ab.Domain.ItemTable;

namespace Project.Src.com.ab.Domain.Harvest
{
    [CreateAssetMenu(fileName = "HarvTables#Name#", menuName = "com.ab/harvest/table")]
    public class HarvestItemTable : SerializedScriptableObject
    {
        // public Dictionary<ItemID, Entry> Entries = new Dictionary<ItemID, Entry>();

        [Serializable]
        public class Entry
        {
            public SpriteRenderer Prefab;
        }
    }
}