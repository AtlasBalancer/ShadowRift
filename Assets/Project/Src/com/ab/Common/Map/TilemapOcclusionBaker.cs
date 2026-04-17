using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace com.ab.common.Map
{
    public class TilemapOcclusionBakerService
    {
        readonly Settings _def;

        public TilemapOcclusionBakerService(Settings def)
        {
            _def = def;
        }

        public void BakeOcclusion()
        {
            var layers = _def.Layers;

            if (layers == null || layers.Length < 2) return;

            // Iterate from the back layer forward
            for (var backIdx = layers.Length - 1; backIdx > 0; backIdx--)
            {
                var backLayer = layers[backIdx];
                if (backLayer == null) continue;

                backLayer.CompressBounds();
                var bounds = backLayer.cellBounds;

                foreach (var pos in bounds.allPositionsWithin)
                {
                    if (backLayer.GetTile(pos) == null) continue;

                    if (IsCovered(layers, pos, backIdx))
                        backLayer.SetTile(pos, null);
                }
            }
        }

        bool IsCovered(Tilemap[] layers, Vector3Int pos, int backIdx)
        {
            for (var frontIdx = 0; frontIdx < backIdx; frontIdx++)
                if (layers[frontIdx] != null && layers[frontIdx].GetTile(pos) != null)
                    return true;

            return false;
        }

        [Serializable]
        public class Settings
        {
            [Tooltip("Tilemaps ordered front-to-back (0 = frontmost layer)")] [SerializeField]
            public Tilemap[] Layers;
        }
    }
}