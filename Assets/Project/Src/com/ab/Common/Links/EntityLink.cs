using System;
using UnityEngine;
using Sirenix.OdinInspector;
using com.ab.complexity.core;
using Unity.VisualScripting;

namespace com.ab.common
{
    public class EntityLink : MonoBehaviour
    {
        [DoNotSerialize] protected bool _inited;

        [ShowInInspector, ShowIf("@UnityEngine.Application.isPlaying")]
        public uint ID
        {
            get
            {
#if UNITY_EDITOR
                if (!_inited || !Application.isPlaying)
                    return UInt32.MinValue;

                return Ent.Gid().Id;
#else
                return   Ent.Gid().Id;
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

            if (!Ent.HasAllOfTags<ActiveTag>())
                Ent.ApplyTag<ActiveTag>(true);
        }

        public void OnDisable()
        {
            if (!_inited)
                return;

            if(!Ent.IsDestroyed())
            if (Ent.HasAllOfTags<ActiveTag>())
                Ent.ApplyTag<ActiveTag>(false);
        }

        protected virtual void CollectInitLinks()
        {
            var collector = gameObject.GetComponent<EntityLinkCollectorMono>();

            if (collector == null)
                return;

            collector.Init(this);
        }

        public virtual W.Entity Init(WC.Entity entC, bool initRef = false)
        {
            var ent = W.Entity.New();
            ent.Add(new ConfigRef(entC.Gid().Id));
            Init(ent, initRef);

            return ent;
        }

        public virtual W.Entity Init(ConfigIDEntSo configID, bool initRef = false)
        {
            var ent = Init(initRef);
            AddConfigID(Ent, configID);
            return ent;
        }

        public uint GetConfigID() => Ent.Ref<ConfigRef>().Id;

        public void AddConfigID(W.Entity ent, uint id) =>
            ent.Add(new ConfigRef(id));

        public void AddConfigID(W.Entity ent, ConfigIDEntSo def) =>
            ent.Add(new ConfigRef(def.RuntimeID));

        public virtual W.Entity Init(bool rootInit = false) =>
            Init(W.Entity.New(), rootInit);

        public virtual W.Entity Init(W.Entity ent, bool rootInit = true)
        {
            Ent = ent;

            Subscribe();
            Register();

            if (rootInit)
                Ent.Add(new Ref(transform));

            if (rootInit)
                CollectInitLinks();

            _inited = true;
            return ent;
        }

        protected void OnClick() =>
            Ent.ApplyTag<ClickTag>(true);

        void OnDestroy() =>
            UnSubscribe();
    }
}