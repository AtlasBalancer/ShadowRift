using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public struct Destroy : IComponent
    {
        public Timer Timer;

        public Destroy(float delay = 0)
        {
            Timer = new Timer(delay);
        }
    }

    public static class DestroyExtensions
    {
        public static void Destr(this World<WT>.Entity source, float delay = 0)
        {
            source.Set(new Destroy(delay));
        }
    }
}