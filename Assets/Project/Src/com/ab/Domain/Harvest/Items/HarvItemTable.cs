using System;
using UnityEngine;
using com.ab.common;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Harvest
{
    [CreateAssetMenu(fileName = "HarvTable#Name#", menuName = "com.ab/harv/table")]
    public class HarvItemTable : EntIDTableSo<HarvItemEntry>
    {
        
    }

    [Serializable]
    public struct HarvItemEntry : IComponent
    {
        public Sprite AKSprite;
    }
}