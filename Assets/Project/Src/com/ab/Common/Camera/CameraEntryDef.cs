using System;
using com.ab.complexity.core;
using com.ab.core;

namespace com.ab.common.Camera
{
    public class CameraEntryDef : StaticEntryParamDef<CameraEntryDef.Settings>, IStaticInitDef, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public EntityLinkCollectorMono MainCamera;
            public CameraZoomSystem.Settings CameraZoomSystem;
        }

        public void RegisterInit()
        {
            Def.MainCamera.Init();
        }

        public void RegisterUpdate()
        {
            Sys.Add(new CameraZoomSystem(Def.CameraZoomSystem));
        }
    }
}