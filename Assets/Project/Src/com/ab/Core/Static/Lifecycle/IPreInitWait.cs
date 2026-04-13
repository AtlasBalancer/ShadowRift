using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace com.ab.core
{
    public interface IPreInitWait
    {

        public UniTask PreInitWait(CancellationToken ct);
    }

    public static class IPreInitWaitRegistry
    {
        static List<IPreInitWait> _preInitAll = new List<IPreInitWait>();

        public static void AddPreInit(IPreInitWait item) => 
            _preInitAll.Add(item);

        public static IReadOnlyList<IPreInitWait> EachPreInit() => 
            _preInitAll;
    }
}