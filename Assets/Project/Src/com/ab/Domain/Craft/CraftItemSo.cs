using System;
using System.Collections.Generic;
using Project.Src.com.ab.Domain.ItemTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.domain.craft
{
    [CreateAssetMenu(fileName = "#Name#CraftItemsDef", menuName = "com.ab/drop/craft")]
    public class CraftItemSo : SerializedScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public ItemDefID ItemDefID;
            public List<Price> Price = new();
        }

        public Dictionary<CraftID, Entry> Entrys = new ();
    }
}