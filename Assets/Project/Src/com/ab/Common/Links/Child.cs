using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public struct Child : ILinksType
    {
        public void OnAdd<WT>(World<WT>.Entity self, EntityGID link) where WT : struct, IWorldType
        {
            Debug.Log($"{nameof(Child)}::{nameof(OnAdd)}");
        }

        public void OnDelete<WT>(World<WT>.Entity self, EntityGID link, HookReason reason) where WT : struct, IWorldType
        {
        }
    }
}