using System;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace com.ab.domain.harv
{
    [Serializable]
    public struct HarvSpawnerDer : IComponent
    {
        public bool FillInit;
        public Vector2 DelayRange;
        public Tilemap Layer;
        public HarvItemTable ItemTable;
    }
}