using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public struct Amount : IComponent
    {
        public int Val;

        public Amount(int val) => Val = val;

        public void Increase(int value) => 
            Val += value;
    }
    
    public static class AmountExtensions
    {
        public static int GetAmount(this W.Entity source) => 
            source.Has<Amount>() ? source.Ref<Amount>().Val : 1;
    }
}