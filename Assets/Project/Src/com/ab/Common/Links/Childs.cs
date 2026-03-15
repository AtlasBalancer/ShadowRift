using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public struct Childs : IEntityLinksComponent<Childs>
    {
        public ROMulti<EntityGID> Links;

        ref ROMulti<EntityGID> IRefProvider<Childs, ROMulti<EntityGID>>.RefValue(ref Childs component) =>
            ref component.Links;

        public override string ToString() => Links.ToString();
    }
}