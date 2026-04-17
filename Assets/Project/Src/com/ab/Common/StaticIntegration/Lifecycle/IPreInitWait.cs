using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace com.ab.common
{
    public interface IPreInitWait
    {
        public UniTask PreInitWait(CancellationToken ct);
    }

    public static class IPreInitWaitRegistry
    {
        static readonly List<IPreInitWait> _preInitAll = new();

        public static void AddPreInit(IPreInitWait item)
        {
            _preInitAll.Add(item);
        }

        public static IReadOnlyList<IPreInitWait> EachPreInit()
        {
            return _preInitAll;
        }
    }
}