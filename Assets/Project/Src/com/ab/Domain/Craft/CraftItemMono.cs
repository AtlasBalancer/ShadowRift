using com.ab.common;
using com.ab.complexity.core;
using DG.Tweening;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace com.ab.domain.craft
{
    public readonly struct CraftItemRef : IComponent
    {
        public readonly CraftItemMono Val;
        public CraftItemRef(CraftItemMono val) => Val = val;
    }
    
    public class CraftItemMono : EntityLink
    {
        public RectTransform PriceRoot;

        public Button Button;
        public Image Bg;

        public Color Available;
        public Color NotAvailable;


        protected override void RegisterComponentRef()
        {
            Ent.Add(new CraftItemRef(this));
            
            Button.onClick.AddListener(Press);
            Active(false);
        }

        public void Press()
        {
            if (!Button.interactable)
                return;

            Button.interactable = false;
            Button.transform.DOScale(0.95f, 0.1f)
                .OnComplete(() =>
                {
                    Button.transform.DOScale(1f, 0.1f);
                    Button.interactable = true;
                });
        }

        [Button]
        public void Deactive() => Active(false);

        [Button]
        public void Active() => Active(true);

        public void Active(bool active)
        {
            Button.interactable = active;

            if (active)
                Bg.color = Available;
            else
                Bg.color = NotAvailable;
        }

        public void AddPrice(Transform price)
        {
            price.transform.SetParent(PriceRoot, false);
        }
    }
}