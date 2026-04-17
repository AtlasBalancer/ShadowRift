using System;

namespace com.ab.common.Camera
{
    public class CameraEntryDef : StaticEntryParamDef<CameraEntryDef.Settings>, IStaticInitDef, IStaticUpdateDef
    {
        public void RegisterInit()
        {
            Def.MainCamera.Init();
        }

        public void RegisterUpdate()
        {
            Sys.Add(new CameraZoomSystem(Def.CameraZoomSystem));
        }

        [Serializable]
        public class Settings
        {
            public EntityLinkCollectorMono MainCamera;
            public CameraZoomSystem.Settings CameraZoomSystem;
        }
    }
}