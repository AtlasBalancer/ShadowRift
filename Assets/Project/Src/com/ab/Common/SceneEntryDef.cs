using System;
using com.ab.complexity.core;
using UnityEngine;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "SceneEntryDef", menuName = "com.ab/common/SceneEntry")]
    public class SceneEntryDef : StaticEntrySOParamDef<SceneEntryDef.Settings>,
        IStaticInitDef, IStaticUpdateDef
    {
        public void RegisterInit()
        {
        }

        public void RegisterUpdate()
        {
            Sys.Add(new DestroyLinkSystem());
            Sys.Add(new MovementVelocitySystem());
        }

        [Serializable]
        public class Settings
        {
        }
    }
}