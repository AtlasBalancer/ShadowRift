using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.inventory
{
    [CreateAssetMenu(fileName = "InventoryTable", menuName = "com.ab/inventory/item_table")]
    public class InvItemTable : ConfigTableSo<InvItemEntry>
    {
    }

    [Serializable]
    public struct InvItemEntry : IComponent
    {
        public ConfigIDEntSo Type;
        public GameObject SingleIcon;
    }
}