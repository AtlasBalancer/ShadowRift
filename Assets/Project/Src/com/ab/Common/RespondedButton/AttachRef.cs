using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public struct AttachRef : IComponent
    {
        public World<WT>.Entity Val;

        public AttachRef(World<WT>.Entity ent)
        {
            Val = ent;
        }

        public void Update(World<WT>.Entity ent)
        {
            Val = ent;
        }
    }
}