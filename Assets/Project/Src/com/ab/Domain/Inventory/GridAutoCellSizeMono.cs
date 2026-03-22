using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    [ExecuteAlways]
    [RequireComponent(typeof(GridLayoutGroup))]
    public class GridAutoCellSizeMono : MonoBehaviour
    {
        public Vector2Int GridSize = new Vector2Int(3, 2);

        public void Apply(Vector2Int gridSize)
        {
            GridSize = gridSize;
            Apply();
        }
        
        private GridLayoutGroup grid;
        private RectTransform rect;
    
        private void Awake()
        {
            grid = GetComponent<GridLayoutGroup>();
            rect = (RectTransform)transform;
            Apply();
        }
    
        private void OnEnable() => Apply();
        private void OnRectTransformDimensionsChange() => Apply();

        void OnValidate()
        {
            Apply();
        }

#if UNITY_EDITOR
        private void Update()
        {
            // чтобы в редакторе тоже обновлялось при изменениях
            if (!Application.isPlaying) Apply();
        }
#endif
        
        private void Apply()
        {
            if (grid == null) grid = GetComponent<GridLayoutGroup>();
            if (rect == null) rect = (RectTransform)transform;
    
            int cols = Mathf.Max(1, GridSize.x);
            int rows = Mathf.Max(1, GridSize.y);
    
            // Размер доступной области внутри padding
            float availableW = rect.rect.width - grid.padding.left - grid.padding.right;
            float availableH = rect.rect.height - grid.padding.top - grid.padding.bottom;
    
            // Вычитаем spacing между ячейками
            float totalSpacingW = grid.spacing.x * (cols - 1);
            float totalSpacingH = grid.spacing.y * (rows - 1);
    
            float cellW = (availableW - totalSpacingW) / cols;
            float cellH = (availableH - totalSpacingH) / rows;
    
            // Защита от отрицательных значений
            cellW = Mathf.Max(0, cellW);
            cellH = Mathf.Max(0, cellH);
    
            grid.cellSize = new Vector2(cellW, cellH);
        }
    }
}