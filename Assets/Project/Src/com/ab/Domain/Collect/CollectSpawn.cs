using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.ItemTable;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Collect
{
    public struct CollectSpawn : IEvent
    {
        public Vector3 Position;
        public ResourceDefID ID;
    }
}