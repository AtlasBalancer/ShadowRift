using System;
using UnityEngine;
using com.ab.common;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Harvest
{
    [CreateAssetMenu(fileName = "HarvTables#Name#", menuName = "com.ab/harv/item")]
    public class HarvItemTable : EntIDTableSo<HarvItemEntry>
    {
        
    }

    [Serializable]
    public struct HarvItemEntry : IComponent
    {
        public string AKSprite;
    }
}