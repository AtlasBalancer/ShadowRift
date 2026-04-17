using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public abstract class WC : World<WCT>
    {
        // World for configs 
    }

    public struct WCT : IWorldType
    {
    }
}