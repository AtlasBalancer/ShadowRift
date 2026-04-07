using System.Linq;
using com.ab.complexity.core;
using UnityEngine;
using Sirenix.Utilities;
using Sirenix.OdinInspector;

namespace com.ab.common
{
    public class EntityLinkCollectorMono : MonoBehaviour
    {
        public EntityLink[] Links;

        [Button]
        public void GrabSameLevel() =>
            Links = gameObject.GetComponents<EntityLink>();

        public void Init()
        {
            var ent = W.Entity.New();
            Links.ForEach(item => item.Init(ent, false));
        }

        public void Init(EntityLink @ref)
        {
            Links.Where(item => !item.Equals(@ref))
                .ForEach(item => item.Init(@ref.Ent, false));
        }
    }
}