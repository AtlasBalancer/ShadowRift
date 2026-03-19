using System;
using System.Threading;
using com.ab.common;
using UnityEngine;
using com.ab.core;
using UnityEngine.UI;
using com.ab.complexity.core;
using com.ab.domain.inventory;
using com.ab.domain.placed;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Equipment;
using Object = UnityEngine.Object;
using Project.Src.com.ab.Domain.ItemTable;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryViewSystem : ViewPresenter<InventoryView>, IInitSystem, IUpdateSystem, IPastInitLoad
    {
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

        public InventoryViewSystem(Settings def)
        {
            _def = def;

            base.Init(_def.Prefab, _def.Root, _def.InventoryButton);
            W.Context<EquipInventoryPuppetViewMono>.Set(View.Puppet);
        }

        void CreateCategories()
        {
            foreach (var ent in W.Query.Entities<All<InvCategoryEntry>>())
            {
                var item = Object.Instantiate(_def.CategoryPrefab, View.CategoryRoot);
                ent.Add<InvCategoryRef>().Ref = item;

                var titleKey = ent.Ref<InvCategoryEntry>().LKTitle;
                item.SetTitle(_localization.GetString(titleKey));
            }
        }

        Settings _def;
        DropTableService _dropTable;
        LocalizationService _localization;
        AtlasService _atlas;
        public void Init()
        {
            _localization = W.Context<LocalizationService>.Get();
            _atlas = W.Context<AtlasService>.Get();
            
            View.Card.Hide();
            View.Materials = new();
            CreateCategories();
            UpdateAllMaterials();
            UpdateAllItems();
        }

        void UpdateAllMaterials()
        {
            // foreach (var @item in _inventory.MaterialLinks)
            //     UpdateMaterial(@item.Key);
        }

        void UpdateAllItems()
        {
            // foreach (var @item in _inventory.ItemLinks)
            // UpdateItem(@item.Key);
        }

        void UpdateMaterial(ResourceDefID id)
        {
            if (!View.Materials.TryGetValue(id, out var itemView))
            {
                // var prefab = _dropTable.Def.InventoryMaterial.Entries[id].Prefab;
                // itemView = CreateInventoryItemView(prefab, _inventory.Get(id), View.MaterialsRoot);
                View.Materials.Add(id, itemView);
            }

            // var amount = _inventory.Get(id).Amount;
            // itemView.UpdateAmount(amount.Value);
        }

        // void UpdateItem(ItemDefID id)
        // {
        // InventoryItemMono itemView;
        // if (!View.Items.TryGetValue(id, out itemView))
        // {
        // InventoryItemMono prefab = _itemTable.Def.InventoryItem.Items[id].Prefab;
        // itemView = CreateInventoryItemView(prefab, _inventory.Get(id), View.ItemsRoot);
        // View.Items.Add(id, itemView);
        // }

        // var amount = _inventory.Get(id).Amount;
        // itemView.UpdateAmount(amount.Value);
        // }

        InvItemMono CreateInventoryItemView(InvItemMono prefab, InventoryItemLink link, Transform root)
        {
            InvItemMono itemMono;
            itemMono = Object.Instantiate(prefab, root);
            // itemView.Init(link);

            return itemMono;
        }

        protected override void Show()
        {
            UpdateAllMaterials();
            UpdateAllItems();
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
                    item.UpdateIcon(_atlas.GetSprite(_def.AtlasKey, itemEntry.LKSprite));
                    item.Init(idRef.ID);
                    ent.Add(new InvItemRef(item));

                    if (!ent.HasAllOf<Amount>())
                        ent.Add(new Amount(1));

                    var categoryRef = itemEntry.Category.RuntimeID.Ref<InvCategoryRef>().Ref;
                    categoryRef.AddItem(item.transform);
                }

                var amount = ent.Ref<Amount>().Value;
                ent.Ref<InvItemRef>().Ref.UpdateAmount(amount);
            }


            foreach (var ent in W.Query.Entities<All<InventoryItem>, TagAll<ViewPressed>>())
            {
                Debug.Log("PRESSED");

                // ItemDefID id = ent.Ref<InventoryItem>().ID;
                int amount = ent.Ref<InventoryAmount>().Value;
                bool isEquipped = ent.HasAllOfTags<Equipped>();

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

        public UniTask PastInitLoad(CancellationToken ct) =>
            _localization.PreloadStringTableAsync(_def.LocalizationTable);
    }

    public struct InvItemRef : IComponent
    {
        public InvItemMono Ref;

        public InvItemRef(InvItemMono @ref) =>
            Ref = @ref;
    }

    internal struct InvCategoryRef : IComponent
    {
        public InvCategoryView Ref;

        public InvCategoryRef(InvCategoryView @ref) =>
            Ref = @ref;
    }
}