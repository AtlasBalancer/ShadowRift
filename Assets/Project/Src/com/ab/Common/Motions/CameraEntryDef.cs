using System;
using com.ab.complexity.core;
using UnityEngine;

namespace com.ab.domain.camera
{
    public class CameraEntryDef : StaticEntryParamDef<CameraEntryDef.Settings>,
        IStaticContextSetDef, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            [Tooltip("Любой Transform: камера, фон, карта и т.д.")]
            public Transform Target;

            [Tooltip("Опционально. Если указана — zoom меняет orthographicSize. " +
                     "Начальный ViewSize берётся из Camera.orthographicSize.")]
            public Camera Camera;

            public CameraRtsSystem.Settings RtsSystem;
        }

        public void SetContext()
        {
            var initialSize = Def.Camera != null
                ? Def.Camera.orthographicSize
                : (Def.RtsSystem.MinZoom + Def.RtsSystem.MaxZoom) * 0.5f;

            W.Context<CameraService>.Set(new CameraService
            {
                Target   = Def.Target,
                Camera   = Def.Camera,
                ViewSize = initialSize,
            });
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new CameraRtsSystem(Def.RtsSystem));
        }
    }
}
