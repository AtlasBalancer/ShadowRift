using System.Collections.Generic;
using com.ab.common;
using com.ab.domain.craft;
using Project.Src.com.ab.Domain.Equipment;
using Sirenix.Serialization;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvMono : ViewMono
    {
        public EquipInventoryPuppetViewMono Puppet;

        public RectTransform CategoryRoot;

        public InvCardItemMono Card;

        [OdinSerialize]
        public Dictionary<ConfigIDEntSo, InvCardItemMono> Cards;
    }
}