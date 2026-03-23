using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Inventory
{
    public struct InvItemRef : IComponent
    {
        public InvItemMono Ref;

        public InvItemRef(InvItemMono @ref) =>
            Ref = @ref;
    }
}