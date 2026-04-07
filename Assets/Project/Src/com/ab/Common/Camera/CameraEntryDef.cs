using System;
using com.ab.complexity.core;

namespace com.ab.common.Camera
{
    public class CameraEntryDef : StaticEntryParamDef<CameraEntryDef.Settings>, 
        IStaticRegisterTypeDef, IStaticInitDef, IStaticUpdateDef
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
            Sys.AddUpdate(new CameraZoomSystem(Def.CameraZoomSystem));
        }

        public void RegisterType()
        {
            W.RegisterComponentType<CameraRef>();
            W.RegisterComponentType<CameraZoom>();
        }
    }
}