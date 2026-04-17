using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace com.ab.core
{
    public static class TilemapExtensions
    {
        public static List<Vector3Int> GetPositions(this Tilemap map)
        {
            var positions = new List<Vector3Int>();
            foreach (var pos in map.cellBounds.allPositionsWithin)
                if (map.HasTile(pos))
                    positions.Add(pos);

            return positions;
        }
    }
}