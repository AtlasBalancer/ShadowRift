using com.ab.common;
using FFS.Libraries.StaticEcs;


namespace com.ab.domain.harv
{
    public struct HarvCollectorRef : IComponent
    {
        public float Radius;
        public Timer Timer;
        public HarvCollectorMono Ref;
    }
}