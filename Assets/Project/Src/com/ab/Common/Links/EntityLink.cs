using System;
using com.ab.complexity.core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.common
{
    public class EntityLink : MonoBehaviour
    {
        [ShowInInspector, ShowIf("@UnityEngine.Application.isPlaying")]
        public uint ID
        {
            get
            {
#if UNITY_EDITOR
                return Application.isPlaying ? Ent.Gid().Id : UInt32.MinValue;
#else
                return   Ent.Gid().Id;
#endif
            }
        }

        public W.Entity Ent { get; private set; }

        protected virtual void RegisterComponentRef()
        {
        }

        public virtual W.Entity Init(WC.Entity entC, bool initRef = false)
        {
            var ent = Init(initRef);
            ent.Add(new ConfigRef(entC.Gid().Id));
            return ent;
        }

        public virtual W.Entity Init(ConfigIDEntSo configID, bool initRef = false)
        {
            var ent = Init(initRef);
            ent.Add(new ConfigRef(configID.RuntimeID));
            return ent;
        }

        public virtual W.Entity Init(bool initRef = false) =>
            Init(W.Entity.New(), initRef);

        public virtual W.Entity Init(W.Entity ent, bool initRef = false)
        {
            Ent = ent;
            RegisterComponentRef();
            if (initRef)
                Ent.Add(new Ref(transform));

            return ent;
        }

        protected void OnClick() =>
            Ent.ApplyTag<Click>(true);
    }
}