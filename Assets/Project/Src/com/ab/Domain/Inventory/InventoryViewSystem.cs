using System;
using com.ab.common;
using UnityEngine;
using com.ab.core;
using UnityEngine.UI;
using com.ab.complexity.core;
using com.ab.domain.inventory;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Equipment;
using Object = UnityEngine.Object;
using Project.Src.com.ab.Domain.ItemTable;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryViewSystem : ViewPresenter<InventoryView>, IInitSystem, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public InvItemTable ItemTable;
            public InvCategoryTable CategoryTable;

            public Button InventoryButton;

            public Transform Root;
            public InventoryView Prefab;
            public InvItemView ItemPrefab;
            public InvCategoryView CategoryPrefab;
        }

        public InventoryViewSystem(Settings def)
        {
            _def = def;

            base.Init(_def.Prefab, _def.Root, _def.InventoryButton);
            W.Context<EquipInventoryPuppetViewMono>.Set(View.Puppet);

            CreateCategories();
        }

        void CreateCategories()
        {
            foreach (var ent in W.Query.Entities<All<InvCategoryEntry>>())
            {
                var item = Object.Instantiate(_def.CategoryPrefab, View.CategoryRoot);
                
                // item.SetTitle();
            }

            // foreach (var def in _def.CategoryTable.Runtime)
            // {
                // var item = Object.Instantiate(_def.CategoryPrefab, View.CategoryRoot);
                // item.SetTitle(def.Value.LKTitle);
                // var ent = item.Init();
                // ent.Add(new InvCategoryRef(item));
            // }
        }

        Settings _def;
        DropTableService _dropTable;

        public void Init()
        {
            View.Card.Hide();
            View.Materials = new();
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

        InvItemView CreateInventoryItemView(InvItemView prefab, InventoryItemLink link, Transform root)
        {
            InvItemView itemView;
            itemView = Object.Instantiate(prefab, root);
            // itemView.Init(link);

            return itemView;
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
                    // Create inventory view
                    var item = Object.Instantiate(_def.ItemPrefab);
                    var id = ent.Ref<IDRef>().ID;
                    item.Init(ent);
                    ent.Add(new InvItemRef(item));

                    if (!ent.HasAllOf<Amount>())
                        ent.Add(new Amount(1));

                    // if (!_def.ItemTable.Runtime.TryGetValue(id, out var itemDef))
                    // throw new ArgumentException($"{nameof(InventoryItemTable)}:: Can't find {id}");

                    // Object.Instantiate(itemDef.SingleIcon, item.transform);
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
    }

    public struct InvItemRef : IComponent
    {
        public InvItemView Ref;

        public InvItemRef(InvItemView @ref) =>
            Ref = @ref;
    }

    internal struct InvCategoryRef : IComponent
    {
        public InvCategoryView Ref;

        public InvCategoryRef(InvCategoryView @ref) =>
            Ref = @ref;
    }
}