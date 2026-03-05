using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Project.Src.com.ab.Domain.ItemTable
{
    public enum DropID
    {
        // Mining 
        Rock = 0,
        Copper = 1,
        Iron = 2,

        // Trees
        Oak = 3,
        Bleach = 4,

        // Eat
        BerryBush = 5
    }

    [CreateAssetMenu(fileName = "#Name#DropTablesDef", menuName = "com.ab/drop/tables")]
    public class DropTableSo : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<DropID, DropEntry> Table = new();

        [Serializable]
        public struct DropEntry
        {
            public DropItem[] Items;
        }

        [Serializable]
        public struct DropItem
        {
            public ResourceDefID ResourceDefID;
            public Vector2Int AmountRange;
            public Vector2 ChanceRange; 
        }
    }
}