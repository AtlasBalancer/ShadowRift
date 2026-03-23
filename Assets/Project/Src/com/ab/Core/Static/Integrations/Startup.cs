using System;
using System.Linq;
using UnityEngine;
using FFS.Libraries.StaticEcs;
using System.Collections.Generic;
using System.Threading;
using com.ab.common;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs.Unity;

namespace com.ab.complexity.core
{
    [Serializable]
    public class Startup : MonoBehaviour
    {
        [SerializeField] Settings _def;
        [field: NonSerialized] HashSet<IPreInitLoad> _initLoads = new();

        async void Awake()
        {
            // ============================================ MAIN INITIALIZATION ======================================================
            W.Create(WorldConfig.Default());
            WC.Create(WorldConfig.Default());

            RegisterCoreTypes();
            RegisterTableTypes();
            RegisterTypes();
            RegisterTag();
            // W.RegisterComponentType<YourComponentType>();
            // W.RegisterTagType<YourTagType>();
            // WEvents.RegisterEventType<YourEventType>();

            EcsDebug<WT>.AddWorld();
            AutoRegister<WT>.Apply();

            W.Initialize();

            // ============================================ CONTEXT INITIALIZATION ====================================================
            W.Context<Settings>.Set(_def);
            SetContext();
            CreateEntities();
            // ============================================ MAIN SYSTEMS INITIALIZATION ===============================================
            Sys.Create();

            RegisterInits();
            RegisterUpdates();
            // UpdateSystems.AddCallOnce(new YourInitOrAndDestroySystem());
            // UpdateSystems.AddUpdate(new YourUpdateSystem1(), new YourUpdateSystem2(), new YourUpdateSystem3());

            // === Initialization order === 
            InitializeTables();
            await WaitPreInitLoads();
            Sys.Initialize();
            
            
            EcsDebug<WT>.AddSystem<SysT>();
        }

        async UniTask WaitPreInitLoads()
        {
            var cts = new CancellationTokenSource();

            await W.Context<AtlasService>.Get().PreInitLoad(cts.Token);
            
            var initLoadList = SysReg.All.OfType<IPreInitLoad>().ToList();
            if (initLoadList.Count == 0) return;
            await UniTask.WhenAll(Enumerable.Select(initLoadList, 
                item => item.PreInitLoad(cts.Token)));
        }

        void RegisterTableTypes()
        {
            foreach (var item in _def.Tables)
                if (item is IStaticRegisterTypeDef def)
                    def.RegisterType();
        }

        void InitializeTables()
        {
            foreach (var item in _def.Tables)
                if (item is IEntTable table)
                    table.Init();
        }

        void RegisterCoreTypes()
        {
            W.RegisterComponentType<Ref>();
        }

        void Update()
        {
            Sys.Update();
        }

        void OnDestroy()
        {
            Sys.Destroy();
            W.Destroy();
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

        void CreateEntities()
        {
            var modules = _def.Modules
                .OfType<IStaticCreateEntityDef>()
                .ToList();

            modules.ForEach(item => item.CreateEntities());

            var features = _def.GetFeatures<IStaticCreateEntityDef>();
            features.ForEach(item => item.CreateEntities());
        }

        [Serializable]
        public class Settings
        {
            public List<ScriptableObject> Modules = new();
            public List<GameObject> Features = new();
            public List<ScriptableObject> Tables = new();

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