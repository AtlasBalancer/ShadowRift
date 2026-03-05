using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    [RequireComponent(typeof(LayoutElement))]
    public class InventoryPartLayoutAutoHeightMono : MonoBehaviour
    {
        public LayoutElement TitleLE;
        public GridLayoutGroup Grid;
        public RectTransform GridRect;
        public LayoutElement GridLE;

        public LayoutElement sectionLE;
        public VerticalLayoutGroup InnerVlg;

        [Header("Columns")] [SerializeField] public int MinColumns = 1;
        public int MaxColumns = 12;

        void OnEnable() => Rebuild();

        [Button]
        public void Rebuild()
        {
            Canvas.ForceUpdateCanvases();
            int columns = CalcColumns();
            ApplyColumns(columns);
            
            RebuildGrid(columns);
            RebuildSelection(columns);
            
            UpdateCanvas();
        }

        void RebuildGrid(int columns)
        {
            int childCount = Grid.transform.childCount;

            if (columns <= 0) columns = 1;

            int rows = Mathf.CeilToInt(childCount / (float)columns);
            rows = Mathf.Max(rows, 1);

            float height =
                Grid.padding.top + Grid.padding.bottom +
                rows * Grid.cellSize.y +
                Mathf.Max(0, rows - 1) * Grid.spacing.y;

            GridLE.preferredHeight = height;
        }

        void RebuildSelection(int columns)
        {

            float gridH = CalcGridHeight(columns);
            float spacingY = InnerVlg ? InnerVlg.spacing : 0f;
            float paddingY = InnerVlg ? InnerVlg.padding.top + InnerVlg.padding.bottom : 0f;

            sectionLE.preferredHeight = paddingY + TitleLE.preferredHeight + spacingY + gridH;
        }

        void UpdateCanvas()
        {
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.parent);
        }

        int CalcColumns()
        {
            float width = GridRect.rect.width;

            float available = width - Grid.padding.left - Grid.padding.right;
            float cellW = Grid.cellSize.x;
            float spacingX = Grid.spacing.x;

            int columns = Mathf.FloorToInt((available + spacingX) / (cellW + spacingX));
            columns = Mathf.Clamp(columns, MinColumns, MaxColumns);
            return Mathf.Max(1, columns);
        }

        void ApplyColumns(int columns)
        {
            Grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            if (Grid.constraintCount != columns)
                Grid.constraintCount = columns;
        }

        float CalcGridHeight(int columns)
        {
            int childCount = Grid.transform.childCount;
            if (childCount == 0) return Grid.padding.top + Grid.padding.bottom; // или 0

            int rows = Mathf.CeilToInt(childCount / (float)columns);

            float paddingY = Grid.padding.top + Grid.padding.bottom;
            float cellH = Grid.cellSize.y;
            float spacingY = Grid.spacing.y;

            return paddingY + rows * cellH + Mathf.Max(0, rows - 1) * spacingY;
        }
    }
}