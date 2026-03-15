using com.ab.common;
using TMPro;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvCategoryView : EntityLink
    {
        public TMP_Text Title;
        public RectTransform ItemRoot;

        public void SetTitle(string title) => 
            Title.SetText(title);
    }
}