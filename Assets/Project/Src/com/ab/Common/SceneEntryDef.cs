using System;
using com.ab.common.ProgressBar;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.complexity.features.player;
using com.ab.core;
using com.ab.domain.item;
using com.ab.item;
using Renderer = com.ab.complexity.core.Renderer;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "SceneEntryDef", menuName = "com.ab/common/SceneEntry")]
    public class SceneEntryDef : StaticEntrySOParamDef<SceneEntryDef.Settings>,
        IStaticInitDef, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            
        }
        
        public void RegisterInit()
        {
        }

        public void RegisterUpdate()
        {
            Sys.Add(new DestroyLinkSystem());
            Sys.Add(new MovementVelocitySystem());
        }
    }
}