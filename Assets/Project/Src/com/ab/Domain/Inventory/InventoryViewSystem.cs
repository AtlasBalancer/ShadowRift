using System;
using System.Threading;
using com.ab.common;
using UnityEngine;
using com.ab.core;
using UnityEngine.UI;
using com.ab.complexity.core;
using com.ab.domain.inventory;
using com.ab.domain.item;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Equipment;
using Object = UnityEngine.Object;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryViewSystem : ViewPresenter<InventoryView>, IInitSystem, IUpdateSystem, IPreInitLoad
    {
        public InventoryViewSystem(Settings def)
        {
            _def = def;
            base.Init(_def.Prefab, _def.Root, _def.InventoryButton);

            _localization = W.Context<LocalizationService>.Get();
            _atlas = W.Context<AtlasService>.Get();

            W.Context<EquipInventoryPuppetViewMono>.Set(View.Puppet);
        }

        readonly Settings _def;
        readonly LocalizationService _localization;
        readonly AtlasService _atlas;

        public UniTask PreInitLoad(CancellationToken ct) =>
            _localization.PreloadStringTableAsync(_def.LocalizationTable);

        public void Init()
        {
            View.Card.Hide();
            CreateCategories();
        }

        void CreateCategories()
        {
            foreach (var ent in W.Query.Entities<All<InvCategoryEntry>>())
            {
                var item = Object.Instantiate(_def.CategoryPrefab, View.CategoryRoot);
                ent.Add(new InvCategoryRef(item));

                var titleKey = ent.Ref<InvCategoryEntry>().LKTitle;
                item.SetTitle(_localization.GetString(titleKey));
            }
        }

        public void Update()
        {
            foreach (var ent in W.Query.Entities<TagAll<InventoryToUpdate>>())
            {
                if (!ent.HasAllOf<InvItemRef>())
                {
                    var idRef = ent.Ref<IDRef>();
                    var itemEntry = idRef.ID.RuntimeID.Ref<ItemEntry>();

                    // Create inventory view
                    var item = Object.Instantiate(_def.ItemPrefab);
                    item.UpdateIcon(_atlas.GetSprite(_def.AtlasKey, itemEntry.AKSprite));
                    item.Init(ent);
                    ent.Add(new InvItemRef(item));

                    if (!ent.HasAllOf<Amount>())
                        ent.Add(new Amount(1));

                    var categoryRef = itemEntry.Category.RuntimeID.Ref<InvCategoryRef>().Ref;
                    categoryRef.AddItem(item.transform);
                }

                var amount = ent.Ref<Amount>().Val;
                ent.Ref<InvItemRef>().Ref.UpdateAmount(amount);
            }


            foreach (var ent in W.Query.Entities<All<InventoryItem>, TagAll<ViewPressed>>())
            {
                Debug.Log("PRESSED");

                // ItemDefID id = ent.Ref<InventoryItem>().ID;
                // int amount = ent.Ref<InventoryAmount>().Value;
                // bool isEquipped = ent.HasAllOfTags<Equipped>();

                // if (!_itemTable.Def.InventoryCards.Items.TryGetValue(id, out var cardDef))
                // throw new ArgumentException($"{nameof(InventoryViewSystem)}:: Can't find {id} in ItemTable");

                // View.ShowCard(ent, cardDef.Icon, amount, cardDef.Title, cardDef.Decription, isEquipped);

                ent.ApplyTag<ViewPressed>(false);
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

            public InvItemTable ItemTable;
            public InvCategoryTable CategoryTable;

            public Button InventoryButton;

            public Transform Root;
            public InventoryView Prefab;
            public InvItemMono ItemPrefab;
            public InvCategoryView CategoryPrefab;
        }
    }
}