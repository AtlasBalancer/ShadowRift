using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace com.ab.domain.price
{
    public readonly struct PriceRef : IComponent
    {
        public readonly PriceMono Val;

        public PriceRef(PriceMono val) => Val = val;
    }

    public class PriceMono : EntityLink
    {
        public Button Btn;

        public Image BackGround;
        public Color AvailableColor;
        public Color NotAvailableColor;

        public bool CustomID;

        [ShowIf("CustomID")] public ConfigIDEntSo ItemID;
        public Transform ItemContainer;
        public List<PriceItemMono> Items = new();

        protected override void Register()
        {
            if (CustomID) 
                AddConfigID(Ent, ItemID);

            Ent.Add(new PriceRef(this));
            Ent.ApplyTag<PriceRegisterTag>(true);
            base.Register();
        }

        public void ClearItems()
        {
            if (Items.Count == 0)
                return;

            foreach (var item in Items) Destroy(item);
            Items.Clear();
        }

        public void UpdateAvailable(bool available)
        {
            Btn.interactable = available;

            if (available)
                BackGround.color = AvailableColor;
            else
                BackGround.color = NotAvailableColor;
        }
    }
}