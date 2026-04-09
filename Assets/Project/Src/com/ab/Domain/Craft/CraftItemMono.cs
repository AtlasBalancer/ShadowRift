using com.ab.common;
using DG.Tweening;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

namespace com.ab.domain.craft
{
    public class CraftItemMono : EntityLink
    {
        public RectTransform PriceRoot;

        public TMP_Text Title;
        public TMP_Text Description;
        
        public Button Button;
        public Image Bg;
        public Image Icon;

        public Color Available;
        public Color NotAvailable;

        protected override void Subscribe()
        {
            Button.onClick.AddListener(Press);
            UpdateCraftAvailable(false);
        }

        protected override void UnSubscribe()
        {
            Button.onClick.RemoveListener(Press);
        }

        protected override void Register()
        {
            Ent.Set(new CraftItemRef(this));
            base.Register();
        }

        public void Press()
        {
            if (!Button.interactable)
                return;

            OnClick();
            
            Button.interactable = false;
            Button.transform.DOScale(0.95f, 0.1f)
                .OnComplete(() =>
                {
                    Button.transform.DOScale(1f, 0.1f);
                    Button.interactable = true;
                });
        }

        [Button]
        public void Deactive() => UpdateCraftAvailable(false);

        [Button]
        public void Active() => UpdateCraftAvailable(true);

        public void AddPrice(Transform price)
        {
            price.transform.SetParent(PriceRoot, false);
        }

        public void UpdateIcon(Sprite sprite) => Icon.sprite = sprite;

        public void UpdateTile(string title) => Title.SetText(title);

        public void UpdateDescription(string description) => Description.SetText(description);

        public void UpdateCraftAvailable(bool available)
        {
            Button.interactable = available;

            if (available)
                Bg.color = Available;
            else
                Bg.color = NotAvailable;
        }
    }

    public readonly struct CraftItemRef : IComponent
    {
        public readonly CraftItemMono Val;
        public CraftItemRef(CraftItemMono val) => Val = val;
    }
}