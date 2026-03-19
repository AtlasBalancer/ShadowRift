using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Project.Src.com.ab.Domain.ItemTable
{
    [CreateAssetMenu(fileName = "DropTable#Name#", menuName = "com.ab/drop/tables")]
    public class DropTable : EntIDTableSo<DropEntry>
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
        public IDEntSo PlaceSo;
        public Vector2Int AmountRange;
        public Vector2 ChanceRange;
    }
}