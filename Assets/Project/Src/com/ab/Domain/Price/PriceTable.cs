using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.price
{
    [CreateAssetMenu(fileName = "PriceTable#Name#", menuName = "com.ab/price/table")]
    public class PriceTable : ConfigTableSo<PriceEntry>
    {
    }

    [Serializable]
    public struct PriceEntry : IComponent
    {
        public PriceAmount[] Price;
    }

    [Serializable]
    public struct PriceAmount
    {
        public int Amount;
        public ConfigIDEntSo Item;
    }
}