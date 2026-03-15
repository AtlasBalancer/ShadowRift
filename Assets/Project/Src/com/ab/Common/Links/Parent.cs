using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public struct Parent : IEntityLinkComponent<Parent> {
        public EntityGID Link;

        ref EntityGID IRefProvider<Parent, EntityGID>.RefValue(ref Parent component) => ref component.Link;
        public override string ToString() => Link.ToString();
    }
}