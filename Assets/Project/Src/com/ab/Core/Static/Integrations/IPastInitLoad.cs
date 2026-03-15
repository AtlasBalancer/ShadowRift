using System.Threading;
using Cysharp.Threading.Tasks;

namespace com.ab.complexity.core
{
    public interface IPastInitLoad
    {
        public UniTask PastInitLoad(CancellationToken ct);
    }
}