using System;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using UnityEngine.SceneManagement;

namespace com.ab.common.LevelTransition
{
    public class LevelTransitionEntryDef : StaticEntryParamDef<LevelTransitionEntryDef.Settings>, IStaticUpdateDef,
        IStaticRegisterTypeDef
    {
        [Serializable]
        public class Settings
        {
            public LevelTransitionSystem.Settings LevelTransitionSystem;
        }

        public void RegisterType()
        {
            W.RegisterTagType<LevelTransitionAvailableTag>();
            W.RegisterTagType<LevelTransitionTag>();

            W.RegisterComponentType<LevelTransitionRef>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new LevelTransitionSystem(Def.LevelTransitionSystem));
        }
    }

    public readonly struct LevelTransitionSystem : IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
        }

        readonly Settings _def;

        public LevelTransitionSystem(Settings def) => _def = def;

        public void Update()
        {
            foreach (var ent in W.Query.Entities<All<LevelTransitionRef>,
                         TagAll<LevelTransitionAvailableTag, LevelTransitionTag>>())
            {
                var leveTransition = ent.Ref<LevelTransitionRef>().Val;
                SceneManager.LoadScene(leveTransition.LevelName);
                
                ent.ApplyTag<LevelTransitionTag>(false);
            }
        }
    }

    public readonly struct LevelTransitionTag : ITag
    {
    }

    public readonly struct LevelTransitionAvailableTag : ITag
    {
    }
}