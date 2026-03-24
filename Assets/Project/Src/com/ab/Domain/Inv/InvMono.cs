using System;
using System.Collections.Generic;
using com.ab.common;
using com.ab.domain.craft;
using Project.Src.com.ab.Domain.Equipment;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvMono : ViewMono
    {
        public EquipPuppetMono Puppet;

        public RectTransform CategoryRoot;

        public List<Pair> Cards;

        [Serializable]
        public class Pair
        {
            public ConfigIDEntSo Category;
            public InvCardItemMono Card;
        }
    }
}