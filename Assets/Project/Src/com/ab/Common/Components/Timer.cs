using FFS.Libraries.StaticEcs;

namespace com.ab.complexity.core
{
    public struct Timer : IComponent
    {
        public float Delay;
        public float Max;

        public bool Next(float delta, bool repeat = true)
        {
            Delay += delta;

            if (Delay >= Max)
            {
                Delay = repeat ? Max - Delay : 0;
                return true;
            }

            return false;
        }
    }
}