using FFS.Libraries.StaticEcs;

namespace com.ab.common.Camera
{
    public readonly struct CameraRef : IComponent
    {
        public readonly CameraRefMono Val;

        public CameraRef(CameraRefMono val)
        {
            Val = val;
        }
    }
}