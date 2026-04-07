using System;
using UnityEngine;
using com.ab.common;
using FFS.Libraries.StaticEcs;

namespace com.ab.domain.harv
{
    [CreateAssetMenu(fileName = "HarvTable#Name#", menuName = "com.ab/harv/item_table")]
    public class HarvItemTable : ConfigTableSo<HarvItemEntry>
    {
        
    }

    [Serializable]
    public struct HarvItemEntry : IComponent
    {
        public string AKSprite;

        public Vector2Int AmountRange;
        
        public float ProgressBarOffset;
    }
}