using com.ab.complexity.core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.common
{
    public class EntityLink : MonoBehaviour
    {
        [ShowInInspector] public uint ID => Ent.Gid().Id;
        public W.Entity Ent { get; private set; }

        protected virtual void RegisterComponentRef(){}

        public virtual W.Entity Init(IDEntSo id, bool initRef = false)
        {
            var ent = Init(initRef);
            ent.Add(new IDRef(id));
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