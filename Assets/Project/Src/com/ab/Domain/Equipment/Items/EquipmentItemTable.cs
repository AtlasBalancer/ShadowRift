using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.equipment
{
    [CreateAssetMenu(fileName = "EquipmentTable", menuName = "com.ab/equipment/table")]
    public class EquipmentItemTable : EntIDTableSo<EquipmentItemEntry>
    { }

    [Serializable]
    public struct EquipmentItemEntry : IComponent
    {
        public GameObject Sprite;
    }
}