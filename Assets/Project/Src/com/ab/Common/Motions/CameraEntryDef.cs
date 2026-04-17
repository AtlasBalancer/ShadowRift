using System;
using com.ab.common;
using UnityEngine;

namespace com.ab.domain.camera
{
    public class CameraEntryDef : StaticEntryParamDef<CameraEntryDef.Settings>,
        IStaticContextSetDef, IStaticUpdateDef
    {
        public void SetContext()
        {
            var initialSize = Def.Camera != null
                ? Def.Camera.orthographicSize
                : (Def.RtsSystem.MinZoom + Def.RtsSystem.MaxZoom) * 0.5f;

            W.SetResource<CameraService>(new CameraService
            {
                Target = Def.Target,
                Camera = Def.Camera,
                ViewSize = initialSize
            });
        }

        public void RegisterUpdate()
        {
            Sys.Add(new CameraRtsSystem(Def.RtsSystem));
        }

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
    }
}