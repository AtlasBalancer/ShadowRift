using System.Threading;
using Cysharp.Threading.Tasks;

namespace com.ab.complexity.core
{
    public interface IPreInitLoad
    {
        public UniTask PreInitLoad(CancellationToken ct);
    }
}