using System;
using UnityEngine;
using com.ab.common;
using FFS.Libraries.StaticEcs;

namespace com.ab.domain.item
{
    [CreateAssetMenu(fileName = "PlacedTable#Name#", menuName = "com.ab/placed/table")]
    public class ItemTable : EntIDTableSo<ItemEntry>
    {
    }

    [Serializable]
    public struct ItemEntry : IComponent
    {
        public IDEntSo Category;
        public string AKSprite;
        public string LKTitle;
        public string LKDescription;
    }
}