using System;
using com.ab.complexity.core;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace com.ab.common
{
    public class EntityLink<TTypeEntity> : EntityLink where TTypeEntity : struct, IEntityType
    {
        public virtual W.Entity Init(ConfigIDEntSo configID, bool initRef = false)
        {
            var ent = Init<TTypeEntity>(initRef);
            AddConfigID(Ent, configID);
            return ent;
        }
    }

    public class EntityLink : MonoBehaviour, IPooled, IDisposable
    {
        [DoNotSerialize] protected bool _inited;

        [ShowInInspector, ShowIf("@UnityEngine.Application.isPlaying")]
        public EntityGID ID
        {
            get
            {
#if UNITY_EDITOR
                if (!_inited || !Application.isPlaying)
                    return new EntityGID();

                return Ent.GID;
#else
                return   Ent.GID;
#endif
            }
        }

        public W.Entity Ent { get; private set; }

        protected virtual void Subscribe()
        {
        }

        protected virtual void UnSubscribe()
        {
        }

        protected virtual void Register()
        {
        }

        public void OnEnable()
        {
            if (!_inited)
                return;

            if (!Ent.Has<ActiveTag>())
                Ent.Apply<ActiveTag>(true);
        }

        public void OnDisable()
        {
            if (!_inited)
                return;

            if (!Ent.IsDestroyed)
                if (Ent.Has<ActiveTag>())
                    Ent.Apply<ActiveTag>(false);
        }

        protected virtual void CollectInitLinks()
        {
            var collector = gameObject.GetComponent<EntityLinkCollectorMono>();

            if (collector == null)
                return;

            collector.Init(this);
        }

        public virtual W.Entity Init(World<WCT>.Entity entC, bool initRef = false)
        {
            var ent = W.NewEntity<Default>();
            var configID = entC.Ref<ConfigRef>();
            ent.Set(configID);
            Init(ent, initRef);

            return ent;
        }

        public virtual W.Entity Init<TEntity>(ConfigIDEntSo configID, bool initRef = false)
            where TEntity : struct, IEntityType
        {
            var ent = Init<TEntity>(initRef);
            AddConfigID(Ent, configID);
            return ent;
        }

        public void AddConfigID(W.Entity ent, ConfigIDEntSo def) =>
            ent.Set(new ConfigRef(def.RuntimeID, def.ID));

        public virtual W.Entity Init(bool rootInit = false) =>
            Init(W.NewEntity<Default>(), rootInit);

        public virtual W.Entity Init<TEntity>(bool rootInit = false) where TEntity : struct, IEntityType =>
            Init(W.NewEntity<TEntity>(), rootInit);

        public virtual W.Entity Init(W.Entity ent, bool rootInit = true)
        {
            Ent = ent;

            Subscribe();
            Register();

            if (rootInit)
                Ent.Set(new Ref(transform));

            if (rootInit)
                CollectInitLinks();

            _inited = true;
            return ent;
        }

        protected void OnClick() =>
            Ent.Apply<ClickTag>(true);

        void OnDestroy() =>
            UnSubscribe();

        public void Dispose()
        {
            
        }

        public virtual void Reset() { }

        public virtual void Cleanup() { }
    }
}