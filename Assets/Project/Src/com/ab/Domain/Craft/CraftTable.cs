using System;
using UnityEngine;
using com.ab.common;
using FFS.Libraries.StaticEcs;

namespace com.ab.domain.craft
{
    [CreateAssetMenu(fileName = "CraftTable#Name#", menuName = "com.ab/craft/table")]
    public class CraftTable : ConfigTableSo<CraftEntry>
    {
        
    }
    
    [Serializable]
    public struct CraftEntry : IComponent
    {
    }
}