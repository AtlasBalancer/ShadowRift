using System;
using System.Collections.Generic;
using com.ab.common;
using com.ab.domain.equip;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvMono : ViewMono
    {
        public RectTransform CategoryRoot;

        public List<Pair> Cards;
        public EquipPuppetMono Puppet;

        [Serializable]
        public class Pair
        {
            public ConfigIDEntSo Category;
            public InvCardItemMono Card;
        }
    }
}