using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public readonly struct ResponseButtonRef : IComponent
    {
        public readonly ResponseButtonMono Val;

        public ResponseButtonRef(ResponseButtonMono val) => Val = val;
    }
}