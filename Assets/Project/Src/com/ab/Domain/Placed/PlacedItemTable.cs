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
        public Dictionary<IDEntSo, Entry> Entries = new Dictionary<IDEntSo, Entry>();

        [Serializable]
        public class Entry
        {
            public GameObject SinglePrefab;
        }
    }
}