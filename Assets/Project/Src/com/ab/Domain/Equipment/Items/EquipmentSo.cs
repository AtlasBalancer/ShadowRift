using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Project.Src.com.ab.Domain.ItemTable;

namespace Project.Src.com.ab.Domain.Unit.Items
{
    [CreateAssetMenu(fileName = "#Name#EquipmentDef", menuName = "com.ab/drop/equipment")]
    public class EquipmentSo : SerializedScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public EquipmentMono Prefab;
        }

        public Dictionary<ItemDefID, Entry> Entrys = new ();
    }
}