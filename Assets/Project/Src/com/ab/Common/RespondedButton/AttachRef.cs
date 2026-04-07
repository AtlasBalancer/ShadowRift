using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public struct AttachRef : IComponent
    {
        public W.Entity Val;

        public AttachRef(World<WT>.Entity ent) => 
            Val = ent;

        public void Update(W.Entity ent) => Val = ent;
    }
}