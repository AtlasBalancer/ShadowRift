using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;

namespace com.ab.common
{
    public struct Amount : IComponent
    {
        [StaticEcsEditorTableValue] public int Val;

        public Amount(int val)
        {
            Val = val;
        }

        public void Increase(int value)
        {
            Val += value;
        }
    }

    public static class AmountExtensions
    {
        public static int GetAmount(this World<WT>.Entity source)
        {
            return source.Has<Amount>() ? source.Ref<Amount>().Val : 1;
        }
    }
}