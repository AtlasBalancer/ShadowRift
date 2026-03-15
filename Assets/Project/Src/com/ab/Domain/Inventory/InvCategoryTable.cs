using System;
using System.Collections.Generic;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Inventory
{
    [CreateAssetMenu(fileName = "Categories", menuName = "com.ab/inventory/categories")]
    public class InvCategoryTable : EntIDTableSo<InvCategoryEntry>
    {
    }

    [Serializable]
    public struct InvCategoryEntry : IComponent
    {
        public string LKTitle;
    }
}