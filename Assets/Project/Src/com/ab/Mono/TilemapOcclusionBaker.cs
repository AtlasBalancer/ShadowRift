using UnityEngine;
using UnityEngine.Tilemaps;

namespace ab.Mono
{
    /// <summary>
    ///     Removes tiles in back layers that are fully occluded by front layers.
    ///     Assign layers in front-to-back order (index 0 = frontmost).
    ///     Call BakeOcclusion() once after the map is loaded.
    /// </summary>
    public class TilemapOcclusionBaker : MonoBehaviour
    {
        [Tooltip("Tilemaps ordered front-to-back (0 = frontmost layer)")] [SerializeField]
        Tilemap[] _layers;

        void Start()
        {
            BakeOcclusion();
        }

        public void BakeOcclusion()
        {
            if (_layers == null || _layers.Length < 2) return;

            // Iterate from the back layer forward
            for (var backIdx = _layers.Length - 1; backIdx > 0; backIdx--)
            {
                var backLayer = _layers[backIdx];
                if (backLayer == null) continue;

                backLayer.CompressBounds();
                var bounds = backLayer.cellBounds;

                foreach (var pos in bounds.allPositionsWithin)
                {
                    if (backLayer.GetTile(pos) == null) continue;

                    if (IsCovered(pos, backIdx))
                        backLayer.SetTile(pos, null);
                }
            }
        }

        bool IsCovered(Vector3Int pos, int backIdx)
        {
            for (var frontIdx = 0; frontIdx < backIdx; frontIdx++)
                if (_layers[frontIdx] != null && _layers[frontIdx].GetTile(pos) != null)
                    return true;
            return false;
        }
    }
}