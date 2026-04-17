using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    [ExecuteAlways]
    [RequireComponent(typeof(GridLayoutGroup))]
    public class GridAutoCellSizeMono : MonoBehaviour
    {
        public Vector2Int GridSize = new(3, 2);

        GridLayoutGroup grid;
        RectTransform rect;

        void Awake()
        {
            grid = GetComponent<GridLayoutGroup>();
            rect = (RectTransform)transform;
            Apply();
        }

#if UNITY_EDITOR
        void Update()
        {
            // чтобы в редакторе тоже обновлялось при изменениях
            if (!Application.isPlaying) Apply();
        }
#endif

        void OnEnable()
        {
            Apply();
        }

        void OnRectTransformDimensionsChange()
        {
            Apply();
        }

        void OnValidate()
        {
            Apply();
        }

        public void Apply(Vector2Int gridSize)
        {
            GridSize = gridSize;
            Apply();
        }

        void Apply()
        {
            if (grid == null) grid = GetComponent<GridLayoutGroup>();
            if (rect == null) rect = (RectTransform)transform;

            var cols = Mathf.Max(1, GridSize.x);
            var rows = Mathf.Max(1, GridSize.y);

            // Размер доступной области внутри padding
            var availableW = rect.rect.width - grid.padding.left - grid.padding.right;
            var availableH = rect.rect.height - grid.padding.top - grid.padding.bottom;

            // Вычитаем spacing между ячейками
            var totalSpacingW = grid.spacing.x * (cols - 1);
            var totalSpacingH = grid.spacing.y * (rows - 1);

            var cellW = (availableW - totalSpacingW) / cols;
            var cellH = (availableH - totalSpacingH) / rows;

            // Защита от отрицательных значений
            cellW = Mathf.Max(0, cellW);
            cellH = Mathf.Max(0, cellH);

            grid.cellSize = new Vector2(cellW, cellH);
        }
    }
}