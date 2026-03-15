using System;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Src.com.ab.Domain.Harvest
{
    [Serializable]
    public struct HarvestSpawnLoopDef : IComponent
    {
        public bool FillInit;
        public Vector2 DelayRange;
        public Tilemap Layer;
        public HarvItemTable ItemTable;
    }
}