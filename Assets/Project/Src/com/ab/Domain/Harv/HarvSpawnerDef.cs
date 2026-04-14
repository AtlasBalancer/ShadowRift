using System;
using System.Collections.Generic;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace com.ab.domain.harv
{
    [Serializable]
    public struct HarvSpawnerDef : IComponent
    {
        public string id;
        
        public bool FillInit;
        public Tilemap Layer;
        public Vector2 DelayRange;
        public List<ConfigIDEntSo> SpawnedItems;
    }
}