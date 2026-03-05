using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    [RequireComponent(typeof(LayoutElement))]
    public class GridSectionAutoHeight : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private int fixedColumns = 0; // 0 => брать из constraint
        private LayoutElement le;

        void Awake()
        {
            le = GetComponent<LayoutElement>();
            if (!grid) grid = GetComponentInChildren<GridLayoutGroup>();
        }

        void OnEnable() => Rebuild();
        void OnTransformChildrenChanged() => Rebuild();

        public void Rebuild()
        {
            int childCount = grid.transform.childCount;

            int columns = fixedColumns;
            if (columns <= 0)
            {
                if (grid.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
                    columns = grid.constraintCount;
                else
                    columns = 1; // если не фикс колонки — лучше явно настроить constraint
            }

            int rows = Mathf.CeilToInt(childCount / (float)columns);
            rows = Mathf.Max(rows, 1); // чтобы пустая секция не схлопнулась в 0 при желании

            float cellH = grid.cellSize.y;
            float spacingY = grid.spacing.y;
            float paddingY = grid.padding.top + grid.padding.bottom;

            float height = paddingY + rows * cellH + Mathf.Max(0, rows - 1) * spacingY;

            le.preferredHeight = height;

            // форсим обновление лейаута (полезно при динамических добавлениях)
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.parent);
        }
    }
}