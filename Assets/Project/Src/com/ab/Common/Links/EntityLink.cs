using System;
using System.Collections.Generic;
using System.Text;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace com.ab.common
{
    public class EntityLink<TTypeEntity> : EntityLink where TTypeEntity : struct, IEntityType
    {
        public virtual World<WT>.Entity Init(ConfigIDEntSo configID, bool initRef = false)
        {
            var ent = Init<TTypeEntity>(initRef);
            AddConfigID(Ent, configID);
            return ent;
        }
    }

    public class EntityLink : MonoBehaviour, IPooled, IDisposable
    {
        [DoNotSerialize] protected bool _inited;

        [ShowInInspector]
        [ShowIf("@UnityEngine.Application.isPlaying")]
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

        public World<WT>.Entity Ent { get; private set; }

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

        void OnDestroy()
        {
            UnSubscribe();
        }

        public void Dispose()
        {
        }


        public virtual void Reset()
        {
        }

        public virtual void Cleanup()
        {
        }

        protected virtual void Subscribe()
        {
        }

        protected virtual void UnSubscribe()
        {
        }

        protected virtual void Register()
        {
        }

        protected virtual void CollectInitLinks()
        {
            var collector = gameObject.GetComponent<EntityLinkCollectorMono>();

            if (collector == null)
                return;

            collector.Init(this);
        }

        public virtual World<WT>.Entity Init(World<WCT>.Entity entC, bool initRef = false)
        {
            var ent = W.NewEntity<Default>();
            var configID = entC.Ref<ConfigRef>();
            ent.Set(configID);
            Init(ent, initRef);

            return ent;
        }

        public virtual World<WT>.Entity Init<TEntity>(ConfigIDEntSo configID, bool initRef = false)
            where TEntity : struct, IEntityType
        {
            var ent = Init<TEntity>(initRef);
            AddConfigID(Ent, configID);
            return ent;
        }

        public void AddConfigID(World<WT>.Entity ent, ConfigIDEntSo def)
        {
            ent.Set(new ConfigRef(def.RuntimeID, def.ID));
        }

        public virtual World<WT>.Entity Init(bool rootInit = false)
        {
            return Init(W.NewEntity<Default>(), rootInit);
        }

        public virtual World<WT>.Entity Init<TEntity>(bool rootInit = false) where TEntity : struct, IEntityType
        {
            return Init(W.NewEntity<TEntity>(), rootInit);
        }

        public virtual World<WT>.Entity Init(World<WT>.Entity ent, bool rootInit = true)
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

        protected void OnClick()
        {
            Ent.Apply<ClickTag>(true);
        }

        #region REPORTS

        readonly StringBuilder _reportSB = new();


        [Button]
        public void ComponentReport()
        {
            _reportSB.Clear();
            _reportSB.Append($"Entity: {Ent.EntityType}, {Ent.GID.EntityID}\n \tComponents list:");

            List<IComponent> result = new();
            Ent.GetAllComponents(result);
            result.ForEach(item => _reportSB.Append($"\n\t\t{item}"));

            Debug.Log(_reportSB);
        }

        #endregion
    }
}