using System;
using System.Linq;
using UnityEngine;
using FFS.Libraries.StaticEcs;
using System.Collections.Generic;
using System.Threading;
using com.ab.common;
using Cysharp.Threading.Tasks;

namespace com.ab.complexity.core
{
    [Serializable]
    public class Startup : MonoBehaviour
    {
        [SerializeField] Settings _def;
        [NonSerialized] bool _started = false;

        async void Awake()
        {
            // == CONFIGS === 
            WC.Create(WorldConfig.Default());
            // RegisterConfigTypes();
            WC.Types().RegisterAll();
            WC.Initialize();

            // ============================================ MAIN INITIALIZATION ======================================================
            W.Create(WorldConfig.Default());
            // RegisterTypes();
            // RegisterTag();
            W.Types().RegisterAll();
            // W.RegisterComponentType<YourComponentType>();
            // W.RegisterTagType<YourTagType>();
            // WEvents.RegisterEventType<YourEventType>();

            W.Initialize();

            // ============================================ CONTEXT INITIALIZATION ====================================================
            W.SetResource(_def);
            SetContext();
            CreateProtoEntities();
            // ============================================ MAIN SYSTEMS INITIALIZATION ===============================================
            Sys.Create();

            RegisterInits();
            RegisterUpdates();
            // UpdateSystems.AddCallOnce(new YourInitOrAndDestroySystem());
            // UpdateSystems.Add(new YourUpdateSystem1(), new YourUpdateSystem2(), new YourUpdateSystem3());

            // === Initialization order === 
            InitializeConfig();
            await WaitPreInitLoads();
            Sys.Initialize();

            CreateLastInitStage();
            _started = true;
        }

        async UniTask WaitPreInitLoads()
        {
            var cts = new CancellationTokenSource();

            await W.GetResource<AtlasService>().PreInitLoad(cts.Token);

            var initLoadList = SysReg.All.OfType<IPreInitLoad>().ToList();
            if (initLoadList.Count == 0) return;
            await UniTask.WhenAll(Enumerable.Select(initLoadList,
                item => item.PreInitLoad(cts.Token)));
        }

        void RegisterConfigTypes()
        {
            foreach (var item in _def.Configs)
                if (item is IStaticRegisterTypeDef def)
                    def.RegisterType();
        }

        void InitializeConfig()
        {
            foreach (var item in _def.Configs)
                if (item is IEcsTable config)
                    config.OpenEcsSession();
        }

        void Update()
        {
            if(!_started)
                return;
            
            Sys.Update();
        }

        void OnDestroy()
        {
            Sys.Destroy();
            W.Destroy();

            foreach (var item in _def.Configs)
                if (item is IEcsTable config)
                    config.CloseEcsSession();

            WC.Destroy();
        }

        void SetContext()
        {
            var features = _def.GetFeatures<IStaticContextSetDef>();
            features.ForEach(item => item.SetContext());

            var modules = _def.Modules
                .OfType<IStaticContextSetDef>()
                .ToList();

            modules.ForEach(item => item.SetContext());
        }

        void RegisterTag()
        {
            var features = _def.GetFeatures<IStaticTagDef>();
            features.ForEach(item => item.RegisterTag());

            var modules = _def.Modules
                .OfType<IStaticTagDef>()
                .ToList();

            modules.ForEach(item => item.RegisterTag());
        }

        void RegisterTypes()
        {
            var modules = _def.Modules
                .OfType<IStaticRegisterTypeDef>()
                .ToList();

            modules.ForEach(item => item.RegisterType());

            var features = _def.GetFeatures<IStaticRegisterTypeDef>();
            features.ForEach(item => item.RegisterType());
        }

        void RegisterInits()
        {
            var modules = _def.Modules
                .OfType<IStaticInitDef>()
                .ToList();

            modules.ForEach(item => item.RegisterInit());

            var features = _def.GetFeatures<IStaticInitDef>();
            features.ForEach(item => item.RegisterInit());
        }

        void RegisterUpdates()
        {
            var modules = _def.Modules
                .OfType<IStaticUpdateDef>()
                .ToList();
            modules.ForEach(item => item.RegisterUpdate());

            var features = _def.GetFeatures<IStaticUpdateDef>();
            features.ForEach(item => item.RegisterUpdate());
        }

        void CreateProtoEntities()
        {
            var modules = _def.Modules
                .OfType<IStaticCreateProtoEntityDef>()
                .ToList();

            modules.ForEach(item => item.CreateProtoEntities());

            var features = _def.GetFeatures<IStaticCreateProtoEntityDef>();
            features.ForEach(item => item.CreateProtoEntities());
        }

        void CreateLastInitStage()
        {
            var modules = _def.Modules
                .OfType<IStaticLastInitStageDef>()
                .ToList();

            modules.ForEach(item => item.LastInit());

            var features = _def.GetFeatures<IStaticLastInitStageDef>();
            features.ForEach(item => item.LastInit());
        }

        [Serializable]
        public class Settings
        {
            public List<ScriptableObject> Modules = new();
            public List<GameObject> Features = new();
            public List<ScriptableObject> Configs = new();

            public List<T> GetFeatures<T>()
            {
                var features = new List<T>();
                foreach (var item in Features)
                {
                    if (!item.activeSelf)
                        continue;

                    if (item.TryGetComponent<T>(out T feature))
                        features.Add(feature);
                }

                return features;
            }
        }
    }
}