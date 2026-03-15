using System.Collections.Generic;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Harvest
{
    public struct HarvAvailablePositions : IComponent
    {
        public List<Vector3Int> Positions;
    }
}