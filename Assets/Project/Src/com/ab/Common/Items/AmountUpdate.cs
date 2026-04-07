using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public readonly struct AmountUpdate : IComponent
    {
        public readonly int Val;
        public AmountUpdate(int val) => Val = val;
    }
}