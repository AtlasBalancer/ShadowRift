using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Inventory
{
    public readonly struct InvCategoryRef : IComponent
    {
        public readonly InvCategoryView Ref;

        public InvCategoryRef(InvCategoryView @ref) =>
            Ref = @ref;
    }
}