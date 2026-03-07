using System;
using System.Collections.Generic;
using com.ab.common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.domain.equipment
{
    [CreateAssetMenu(fileName = "EquipmentTable", menuName = "com.ab/equipment/table")]
    public class EquipmentItemTable : SerializedScriptableObject
    {
        public Dictionary<ItemID, Entry> Entries = new Dictionary<ItemID, Entry>();

        [Serializable]
        public class Entry
        {
            public GameObject Icon;
        }
    }
}