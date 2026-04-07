using System;
using com.ab.common;
using com.ab.complexity.core;
using com.ab.domain.harv;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Src.com.ab.Feature.Mine
{
    public class MineEntryDef : StaticEntryParamDef<MineEntryDef.Settings>, IStaticUpdateDef, IStaticRegisterTypeDef
    {
        [Serializable]
        public class Settings
        {
            public MineInitLayerSystem.Settings MineInitLayerSystem;
            public MineHoleOpenSystem.Settings MineHoleOpenSystem;
            public MineTransitionSystem.Settings MineTransitionSystem;
        }

        public void RegisterType()
        {
            W.RegisterComponentType<HoleRef>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new MineInitLayerSystem(Def.MineInitLayerSystem));
            Sys.AddUpdate(new MineHoleOpenSystem(Def.MineHoleOpenSystem));
            Sys.AddUpdate(new MineTransitionSystem(Def.MineTransitionSystem));
        }
    }

    public readonly struct MineTransitionSystem : IUpdateSystem
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
            foreach (var ent in W.Query.Entities<All<HoleRef>, TagAll<AvailableTag, TriggerEnterTag>>()) 
                SceneManager.LoadScene(_def.SceneName);
        }
    }

    public readonly struct MineHoleOpenSystem : IInitSystem, IUpdateSystem
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
            if (_def.Hole.Ent.HasAllOfTags<AvailableTag>())
                return;

            if (W.Query.Entities<All<HarvRef>>().EntitiesCount() == 0)
            {
                _def.Hole.Active();
                _def.Hole.Ent.ApplyTag<AvailableTag>(true);
            }
        }
    }

    public class MineInitLayerSystem : IInitSystem, IUpdateSystem
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