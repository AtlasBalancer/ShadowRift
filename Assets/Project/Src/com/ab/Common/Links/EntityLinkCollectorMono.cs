using System.Linq;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace com.ab.common
{
    public class EntityLinkCollectorMono : MonoBehaviour
    {
        public EntityLink[] Links;

        [Button]
        public void GrabSameLevel()
        {
            Links = gameObject.GetComponents<EntityLink>();
        }

        public void Init()
        {
            var ent = W.NewEntity<Default>();
            Links.ForEach(item => item.Init(ent, false));
        }

        public void Init(EntityLink @ref)
        {
            Links.Where(item => !item.Equals(@ref))
                .ForEach(item => item.Init(@ref.Ent, false));
        }
    }
}