using System;
using UnityEngine;
using com.ab.core;
using com.ab.common;
using UnityEngine.UI;
using System.Threading;
using com.ab.domain.item;
using com.ab.domain.equip;
using com.ab.complexity.core;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using Object = UnityEngine.Object;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvViewSystem : ViewPresenter<InvMono>, IInitSystem, IUpdateSystem, IPreInitLoad
    {
        public InvViewSystem(Settings def)
        {
            _def = def;
            Register(_def.ViewRef, _def.InventoryButton);

            _localization = W.Context<LocalizationService>.Get();
            _atlas = W.Context<AtlasService>.Get();

            W.Context<EquipPuppetMono>.Set(View.Puppet);
        }

        readonly Settings _def;
        readonly LocalizationService _localization;
        readonly AtlasService _atlas;

        public UniTask PreInitLoad(CancellationToken ct) =>
            _localization.PreloadStringTableAsync(_def.LocalizationTable);

        public void Init()
        {
            InitCards();
            CreateCategories();
        }

        void InitCards()
        {
            View.Cards.ForEach(item =>
            {
                item.Card.Subscribe();
                item.Card.Hide();
            });
        }

        void CreateCategories()
        {
            foreach (var entC in WC.Query.Entities<All<InvCategoryEntry>>())
            {
                var item = Object.Instantiate(_def.CategoryPrefab, View.CategoryRoot);
                var ent = item.Init(entC, true);
                ent.Add(new InvCategoryRef(item));

                var titleKey = entC.Ref<InvCategoryEntry>().LKTitle;
                item.SetTitle(_localization.GetString(titleKey));
            }
        }

        public void Update()
        {
            foreach (var ent in W.Query.Entities<TagAll<InvToUpdateTag>>())
            {
                if (!ent.HasAllOf<InvItemRef>())
                {
                    var itemEntry = ent.GetConfigTable<ItemEntry>();

                    // Create inventory view
                    var item = Object.Instantiate(_def.ItemPrefab);
                    item.UpdateIcon(_atlas.GetSprite(_def.AtlasKey, itemEntry.AKSprite));
                    item.Init(ent);
                    ent.Add(new InvItemRef(item));

                    if (!ent.HasAllOf<Amount>())
                        ent.Add(new Amount(1));

                    itemEntry.Category.TryToFindRuntimeRef<InvCategoryRef>(out var entCategory, out _);
                    var categoryRef = entCategory.Ref<InvCategoryRef>().Ref;
                    categoryRef.AddItem(item.transform);
                }

                var amount = ent.Ref<Amount>().Val;
                ent.Ref<InvItemRef>().Ref.UpdateAmount(amount);
            }

            foreach (var ent in W.Query.Entities<All<InvItemRef>, TagAll<Click>>())
            {
                Debug.Log("PRESSED");

                var itemDef = ent.GetConfigTable<ItemEntry>();
                int amount = ent.Ref<Amount>().Val;

                foreach (var card in View.Cards)
                {
                    if (card.Category.Equals(itemDef.Category))
                    {
                        var icon = _atlas.GetSprite(_def.AtlasKey, itemDef.AKSprite);
                        var title = _localization.GetString(itemDef.LKTitle, _def.LocalizationTable);
                        var descr = _localization.GetString(itemDef.LKDescription, _def.LocalizationTable);
                        bool equip = ent.HasAllOfTags<EquipTag>();

                        card.Card.Show(ent, equip, icon, amount, title, descr);
                    }
                    else
                    {
                        card.Card.gameObject.SetActive(false);
                    }
                }

                // ItemDefID id = ent.Ref<InventoryItem>().ID;
                // int amount = ent.Ref<InventoryAmount>().Value;
                // bool isEquipped = ent.HasAllOfTags<Equipped>();

                // if (!_itemTable.Def.InventoryCards.Items.TryGetValue(id, out var cardDef))
                // throw new ArgumentException($"{nameof(InventoryViewSystem)}:: Can't find {id} in ItemTable");


                // View.Card.Show(ent, cardDef.Icon, amount, cardDef.Title, cardDef.Decription, isEquipped);

                ent.ApplyTag<Click>(false);
            }

            // foreach (var ent in W.Query.Entities<All<InventoryMaterial>, TagAll<Delete>>())
            // {
            // var id = ent.Ref<InventoryMaterial>().ID;

            // Object.Destroy(View.Materials[id]);
            // View.Materials.Remove(id);
            // }

            // foreach (var ent in W.Query.Entities<All<InventoryItem>, TagAll<Delete>>())
            // {
            // var id = ent.Ref<InventoryItem>().ID;

            // Object.Destroy(View.Items[id]);
            // View.Items.Remove(id);
            // }

            if (!IsActive())
                return;

            // foreach (var @event in _addMaterialReceiver)
            // UpdateMaterial(@event.Value.ID);

            // foreach (var @event in _addItemReceiver)
            {
                // UpdateItem(@event.Value.ID);
            }
        }

        [Serializable]
        public class Settings
        {
            public string AtlasKey;
            public string LocalizationTable;

            public Button InventoryButton;

            public Transform Root;
            public InvMono ViewRef;
            public InvItemMono ItemPrefab;
            public InvCategoryView CategoryPrefab;
        }
    }
}