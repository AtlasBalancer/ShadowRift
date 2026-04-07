using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace com.ab.feature.mine
{
    
    [CreateAssetMenu(fileName = "MineTable#Name#", menuName = "com.ab/mine/table")]
    public class MineTable : ConfigTableSo<MineEntry>
    {
        
    }
    
    [Serializable]
    public struct MineEntry : IComponent
    {
        public int Number;
        public string LKTitle;

        public int HarvAmount;
        
        public Tilemap FlorPrefab;
        public Tilemap HarvestSpawn;
        public Tilemap DownHolePoint;
    }
}