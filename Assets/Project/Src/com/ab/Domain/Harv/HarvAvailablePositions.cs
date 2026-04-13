using System.Collections.Generic;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.harv
{
    public readonly struct HarvAvailablePositions : IComponent
    {
        public readonly List<Vector3Int> Val;

        public HarvAvailablePositions(List<Vector3Int> val) => Val = val;
    }
}