using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public readonly struct LinkRef : IComponent 
    {
        public readonly EntityLink Ref;

        public LinkRef(EntityLink link) => Ref = link;
    }

    public class EntityLink : MonoBehaviour
    {
        public W.Entity Ent { get; private set; }

        public W.Entity Init(W.Entity ent)
        {
            Ent = ent;
            return ent;
        }
        
        public W.Entity Init()
        {
            Ent = W.Entity.New();
            Ent.Add(new LinkRef(this));
            return Ent;
        }
    }
}