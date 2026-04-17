using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.item
{
    [CreateAssetMenu(fileName = "DropTable#Name#", menuName = "com.ab/drop/tables")]
    public class DropTable : ConfigTableSo<DropEntry>
    {
    }

    [Serializable]
    public struct DropEntry : IComponent
    {
        public DropItem[] Items;
    }

    [Serializable]
    public struct DropItem
    {
        public ConfigIDEntSo PlaceSo;
        public Vector2Int AmountRange;
        public Vector2 ChanceRange;
    }
}