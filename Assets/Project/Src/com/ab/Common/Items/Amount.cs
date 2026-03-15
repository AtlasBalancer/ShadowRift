using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public struct Amount : IComponent
    {
        public int Value;

        public Amount(int val) => Value = val;

        public void Increase(int value) => 
            Value += value;
    }
    
    public static class AmountExtensions
    {
        public static int GetAmount(this W.Entity source) => 
            source.HasAllOf<Amount>() ? source.Ref<Amount>().Value : 1;
    }
}