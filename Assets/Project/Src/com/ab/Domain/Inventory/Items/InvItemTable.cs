using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.inventory
{
    [CreateAssetMenu(fileName = "InventoryTable", menuName = "com.ab/inventory/table")]
    public class InvItemTable : EntIDTableSo<InvItemEntry>
    {
    }

    [Serializable]
    public struct InvItemEntry : IComponent
    {
        public IDEntSo Type;
        public GameObject SingleIcon;
    }
}