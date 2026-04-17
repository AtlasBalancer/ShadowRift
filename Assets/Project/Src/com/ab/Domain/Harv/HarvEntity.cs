using FFS.Libraries.StaticEcs;

namespace com.ab.domain.harv
{
    public readonly struct HarvSpawnerEntity : IEntityType
    {
        public byte Id()
        {
            return 2;
        }
    }

    public readonly struct HarvEntity : IEntityType
    {
        public byte Id()
        {
            return 1;
        }

        public void OnCreate<TWorld>(World<TWorld>.Entity ent)
            where TWorld : struct, IWorldType
        {
        }

        public void OnDestroy<TWorld>(World<TWorld>.Entity ent, HookReason reason)
            where TWorld : struct, IWorldType
        {
        }
    }
}