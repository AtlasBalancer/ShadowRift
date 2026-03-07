using System;
using System.Collections.Generic;
using com.ab.common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.domain.placed
{
    [CreateAssetMenu(fileName = "PlacedTable#Name#", menuName = "com.ab/placed/table")]
    public class PlacedItemTable : SerializedScriptableObject
    {
        public Dictionary<ItemID, Entry> Entries = new Dictionary<ItemID, Entry>();

        [Serializable]
        public class Entry
        {
            public SpriteRenderer Prefab;
        }
    }
}