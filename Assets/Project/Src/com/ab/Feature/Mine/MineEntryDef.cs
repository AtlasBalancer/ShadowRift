using System;
using com.ab.common;
using com.ab.complexity.core;
using com.ab.domain.harv;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Src.com.ab.Feature.Mine
{
    public class MineEntryDef : StaticEntryParamDef<MineEntryDef.Settings>, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public MineInitLayerSystem.Settings MineInitLayerSystem;
            public MineHoleOpenSystem.Settings MineHoleOpenSystem;
            public MineTransitionSystem.Settings MineTransitionSystem;
        }

        public void RegisterUpdate()
        {
            Sys.Add(new MineInitLayerSystem(Def.MineInitLayerSystem));
            Sys.Add(new MineHoleOpenSystem(Def.MineHoleOpenSystem));
            Sys.Add(new MineTransitionSystem(Def.MineTransitionSystem));
        }
    }

    public readonly struct MineTransitionSystem : ISystem
    {
        [Serializable]
        public class Settings
        {
            public string SceneName;
        }

        readonly Settings _def;
        
        public MineTransitionSystem(Settings def) => _def = def;
        
        public void Update()
        {
            foreach (var ent in W.Query<All<HoleRef, AvailableTag, TriggerEnterTag>>().Entities()) 
                SceneManager.LoadScene(_def.SceneName);
        }
    }

    public readonly struct MineHoleOpenSystem : ISystem
    {
        [Serializable]
        public class Settings
        {
            public HoleMono Hole;
        }

        readonly Settings _def;

        public MineHoleOpenSystem(Settings def) => _def = def;

        public void Init() =>
            _def.Hole.Init();

        public void Update()
        {
            if (_def.Hole.Ent.Has<AvailableTag>())
                return;

            if (W.Query<All<HarvRef>>().EntitiesCount() == 0)
            {
                _def.Hole.Active();
                _def.Hole.Ent.Apply<AvailableTag>(true);
            }
        }
    }

    public class MineInitLayerSystem : ISystem
    {
        [Serializable]
        public class Settings
        {
            public Grid GridRoot;
        }

        readonly Settings _def;
        public MineInitLayerSystem(Settings def) => _def = def;

        public void Init()
        {
        }

        public void Update()
        {
        }
    }
}