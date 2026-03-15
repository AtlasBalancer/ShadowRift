using System;
using com.ab.complexity.core;
using DG.Tweening;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.UI;
using Project.Src.com.ab.Domain.ItemTable;
using Sirenix.OdinInspector;

namespace com.ab.domain.craft
{
    [Serializable]
    public struct Price
    {
        public int Amount;
        public ResourceDefID Resource;
        public Sprite Icon;
    }

    public class CraftItemViewMono : MonoBehaviour
    {
        public CraftID ID;
        // public CraftItemSo.Entry Def;

        public RectTransform PriceContainer;

        public Button Button;
        public Image Bg;

        public Color Available;
        public Color NotAvailable;
        public World<WT>.Entity Ent { get; set; }

        public void Init(bool active)
        {
            Button.onClick.AddListener(Press);
            Active(active);
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

            // Ent.Add(new CraftCommand { Def = Def });
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
    }
}