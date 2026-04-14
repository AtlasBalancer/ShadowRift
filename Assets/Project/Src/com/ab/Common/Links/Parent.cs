using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public struct Parent : ILinkType
    {
        public void OnAdd<WT>(World<WT>.Entity self, EntityGID link) where WT : struct, IWorldType
        {
            if (link.TryUnpack<WT>(out var parent))
            {
                ref var children = ref parent.Add<World<WT>.Links<Child>>();
                children.TryAdd(self.AsLink<Child>());
            }
        }

        public void OnDelete<WT>(World<WT>.Entity self, EntityGID link, HookReason reason) where WT : struct, IWorldType
        {
            if (link.TryUnpack<WT>(out var parent) && parent.Has<World<WT>.Links<Child>>())
            {
                parent.Ref<World<WT>.Links<Child>>().TryRemove(self.AsLink<Child>());
            }
        }
    }
}