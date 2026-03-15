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

        public static void AddUpdate<T>(T system, short order = 0) where T : IUpdateSystem
        {
            _registered.Add(system);
            Sys.AddUpdate(system, order);
        }

        public static void AddInit<T>(T system) where T : ICallOnceSystem
        {
            _registered.Add(system);
            Sys.AddCallOnce(system);
        }
    }
}