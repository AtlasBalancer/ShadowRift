using System.Collections.Generic;
using com.ab.complexity.core;
using com.ab.domain.craft;
using Project.Src.com.ab.Domain.Equipment;
using Project.Src.com.ab.Domain.ItemTable;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    
    public class InventoryViewMono : ViewMono
    {
        public EquipInventoryPuppetViewMono Puppet;
        
        public Transform ItemsRoot;
        // public Dictionary<ItemDefID, InventoryItemMono> Items = new Dictionary<ItemDefID, InventoryItemMono>();
        
        public Transform MaterialsRoot;
        public Dictionary<ResourceDefID, InventoryItemMono> Materials = new Dictionary<ResourceDefID, InventoryItemMono>();

        public InventoryCardItemMono Card;

        public override void Init() => 
            Card.Init();


        public void ShowCard(W.Entity ent, Image icon, int amount, string title, string description, bool equipped)
        {
            Card.Show(ent, icon, amount, title, description, equipped);
        }
    }
}