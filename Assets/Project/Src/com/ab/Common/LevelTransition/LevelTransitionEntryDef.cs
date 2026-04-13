using System;
using com.ab.complexity.core;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using UnityEngine.SceneManagement;

namespace com.ab.common.LevelTransition
{
    public class LevelTransitionEntryDef : StaticEntryParamDef<LevelTransitionEntryDef.Settings>, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public LevelTransitionSystem.Settings LevelTransitionSystem;
        }

        public void RegisterUpdate()
        {
            Sys.Add(new LevelTransitionSystem(Def.LevelTransitionSystem));
        }
    }

    public readonly struct LevelTransitionSystem : ISystem
    {
        [Serializable]
        public class Settings
        {
        }

        readonly Settings _def;

        public LevelTransitionSystem(Settings def) => _def = def;

        public void Update()
        {
            foreach (var ent in W.Query<All<
                             LevelTransitionRef, 
                             LevelTransitionAvailableTag, 
                             LevelTransitionTag>>()
                         .Entities())
            {
                var leveTransition = ent.Ref<LevelTransitionRef>().Val;
                SceneManager.LoadScene(leveTransition.LevelName);

                ent.Apply<LevelTransitionTag>(false);
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