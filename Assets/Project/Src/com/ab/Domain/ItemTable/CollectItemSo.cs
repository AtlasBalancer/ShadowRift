using System;
using System.Collections.Generic;
using Project.Src.com.ab.Domain.Collect;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Src.com.ab.Domain.ItemTable
{
    [CreateAssetMenu(fileName = "#Name#CollectItemsDef", menuName = "com.ab/drop/collect")]
    public class CollectItemSo : SerializedScriptableObject
    {
        [Serializable]
        public struct Entry
        {
            public PlacedMono Prefab;
        }

        public Dictionary<ResourceDefID, Entry> Items = new();
    }
}