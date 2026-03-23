using com.ab.complexity.core;
using com.ab.domain.craft;
using Project.Src.com.ab.Domain.Equipment;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvMono : ViewMono
    {
        public EquipInventoryPuppetViewMono Puppet;

        public RectTransform CategoryRoot;

        public InvCardItemMono Card;

        public override void Init() => 
            Card.Init();

        public void ShowCard(W.Entity ent, Image icon, int amount, string title, string description, bool equipped)
        {
            Card.Show(ent, icon, amount, title, description, equipped);
        }
    }
}