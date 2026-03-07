using System;
using UnityEngine;
using com.ab.core;
using UnityEngine.UI;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Equipment;
using Object = UnityEngine.Object;
using Project.Src.com.ab.Domain.ItemTable;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryViewSystem : ViewPresenter<InventoryViewMono>, IInitSystem, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public Button InventoryButton;

            public Transform Root;
            public InventoryViewMono Prefab;
        }

        public InventoryViewSystem(Settings def)
        {
            _def = def;
            base.Init(_def.Prefab, _def.Root, _def.InventoryButton);
            W.Context<EquipInventoryPuppetViewMono>.Set(View.Puppet);
        }

        Settings _def;
        InventoryService _inventory;
        ItemTableService _itemTable;

        EventReceiver<T, InventoryAddMaterial> _addMaterialReceiver;
        EventReceiver<T, InventoryRemoveMaterial> _removeReceiver;
        EventReceiver<T, InventoryAddItem> _addItemReceiver;


        public void Init()
        {
            _itemTable = W.Context<ItemTableService>.Get();
            _inventory = W.Context<InventoryService>.Get();

            _addMaterialReceiver = W.Events.RegisterEventReceiver<InventoryAddMaterial>();
            _removeReceiver = W.Events.RegisterEventReceiver<InventoryRemoveMaterial>();

            _addItemReceiver = W.Events.RegisterEventReceiver<InventoryAddItem>();

            View.Card.Hide();
            View.Materials = new();
            UpdateAllMaterials();
            UpdateAllItems();
        }

        void UpdateAllMaterials()
        {
            foreach (var @item in _inventory.MaterialLinks)
                UpdateMaterial(@item.Key);
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
                var prefab = _itemTable.Def.InventoryMaterial.Entries[id].Prefab;
                itemView = CreateInventoryItemView(prefab, _inventory.Get(id), View.MaterialsRoot);
                View.Materials.Add(id, itemView);
            }

            var amount = _inventory.Get(id).Amount;
            itemView.UpdateAmount(amount.Value);
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

        InventoryItemMono CreateInventoryItemView(InventoryItemMono prefab, InventoryItemLink link, Transform root)
        {
            InventoryItemMono itemView;
            itemView = Object.Instantiate(prefab, root);
            itemView.Init(link);

            return itemView;
        }

        protected override void Show()
        {
            UpdateAllMaterials();
            UpdateAllItems();
        }

        public void Update()
        {
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

            foreach (var ent in W.Query.Entities<All<InventoryMaterial>, TagAll<Delete>>())
            {
                var id = ent.Ref<InventoryMaterial>().ID;

                Object.Destroy(View.Materials[id]);
                View.Materials.Remove(id);
            }

            foreach (var ent in W.Query.Entities<All<InventoryItem>, TagAll<Delete>>())
            {
                // var id = ent.Ref<InventoryItem>().ID;

                // Object.Destroy(View.Items[id]);
                // View.Items.Remove(id);
            }

            if (!IsActive())
                return;

            foreach (var @event in _addMaterialReceiver)
                UpdateMaterial(@event.Value.ID);

            foreach (var @event in _addItemReceiver)
            {
                // UpdateItem(@event.Value.ID);
            }
        }
    }
}