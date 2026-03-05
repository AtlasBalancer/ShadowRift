using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Inventory
{
    public struct InventoryRemoveMaterial : IEvent
    {
        public Vector2Int From;
        public Vector2Int To;
    }
}