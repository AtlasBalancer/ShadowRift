using UnityEngine;

namespace com.ab.domain.craft
{
    public class CraftViewMono : ViewMono
    {
        public RectTransform ItemRoot;
        
        public void AddItem(Transform item)
        {
            item.SetParent(ItemRoot, false);
        }
    }
}