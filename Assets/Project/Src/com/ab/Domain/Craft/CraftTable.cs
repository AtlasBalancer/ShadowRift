using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.craft
{
    [CreateAssetMenu(fileName = "CraftTable#Name#", menuName = "com.ab/craft/table")]
    public class CraftTable : EntIDTableSo<CraftEntry>
    {
        
    }
    
    [Serializable]
    public struct CraftEntry : IComponent
    {
        public CraftAmount[] Price;
    }

    [Serializable]
    public struct CraftAmount
    {
        public int Amount;
        public IDEntSo Item;
    }
}