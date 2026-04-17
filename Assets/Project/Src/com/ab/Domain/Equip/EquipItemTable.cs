using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.equip
{
    [CreateAssetMenu(fileName = "EquipTable", menuName = "com.ab/equip/table")]
    public class EquipItemTable : ConfigTableSo<EquipEntry>
    {
    }

    [Serializable]
    public struct EquipEntry : IComponent
    {
        public ConfigIDEntSo Type;
    }
}