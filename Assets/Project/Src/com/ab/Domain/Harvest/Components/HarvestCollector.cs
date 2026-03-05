using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Harvest
{
    public struct HarvestCollector : IComponent
    {
        public float Radius;
        public Timer Timer;
    }
}