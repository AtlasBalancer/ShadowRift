using System.Collections.Generic;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.harv
{
    public struct HarvAvailablePositions : IComponent
    {
        public List<Vector3Int> Positions;
    }
}