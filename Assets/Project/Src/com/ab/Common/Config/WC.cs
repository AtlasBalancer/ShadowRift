using FFS.Libraries.StaticEcs;

namespace com.ab.complexity.core
{
    public abstract class WC : World<WCT>
    {
        // World for configs 
    }

    public struct WCT : IWorldType
    {
    }
}