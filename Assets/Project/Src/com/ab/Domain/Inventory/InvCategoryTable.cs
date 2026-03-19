using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Inventory
{
    [CreateAssetMenu(fileName = "InvCategoryTable", menuName = "com.ab/inventory/table_categories")]
    public class InvCategoryTable : EntIDTableSo<InvCategoryEntry>
    {
    }

    [Serializable]
    public struct InvCategoryEntry : IComponent
    {
        public string LKTitle;
    }
}