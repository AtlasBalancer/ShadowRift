using System;
using System.Linq;
using UnityEngine;
using FFS.Libraries.StaticEcs;
using System.Collections.Generic;
using FFS.Libraries.StaticEcs.Unity;

namespace com.ab.complexity.core
{
    [Serializable]
    public class Startup : MonoBehaviour
    {
        [SerializeField] Settings _def;

        void Awake()
        {
            // ============================================ MAIN INITIALIZATION ======================================================
            W.Create(WorldConfig.Default());

            RegisterCoreTypes();
            RegisterTypes();
            RegisterTag();
            // W.RegisterComponentType<YourComponentType>();
            // W.RegisterTagType<YourTagType>();
            // WEvents.RegisterEventType<YourEventType>();

            EcsDebug<T>.AddWorld();
            AutoRegister<T>.Apply();

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

            Sys.Initialize();
            EcsDebug<T>.AddSystem<SysT>();

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
            public List<GameObject> Features = new();
            public List<ScriptableObject> Modules = new();

            public List<T> GetFeatures<T>()
            {
                var features = new List<T>();
                Features.ForEach(item =>
                {
                    if (item.TryGetComponent<T>(out T feature))
                        features.Add(feature);
                });

                return features;
            }
        }
    }
}