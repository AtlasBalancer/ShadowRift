using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Project.Src.com.ab.Domain.ItemTable
{
    [CreateAssetMenu(fileName = "#Name#HarvestItemsDef", menuName = "com.ab/drop/harvest")]
    public class HarvestItemSo : SerializedScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public DropID DropTableID;
        }

        public Dictionary<ResourceDefID, Entry> Items = new ();
    }
}