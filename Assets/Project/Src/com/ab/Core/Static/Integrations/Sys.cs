using System.Collections.Generic;
using FFS.Libraries.StaticEcs;

namespace com.ab.complexity.core
{
    public abstract class Sys : W.Systems<SysT>
    {
    }

    public static class SysReg
    {
        static readonly List<ISystem> _registered = new();
        public static IReadOnlyList<ISystem> All => _registered;

        public static void Add<T>(T system, short order = 0) where T : ISystem
        {
            _registered.Add(system);
            Sys.Add(system, order);
        }
    }
}