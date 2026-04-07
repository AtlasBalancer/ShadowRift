using UnityEngine;
using com.ab.common;
using com.ab.complexity.core;
using com.ab.domain.price;
using FFS.Libraries.StaticEcs;

namespace com.ab.domain.construct
{
    public readonly struct ConstructionRef : IComponent
    {
        public readonly ConstructionMono Val;

        public ConstructionRef(ConstructionMono val) => Val = val;
    }
    
    public class ConstructionMono : EntityLink
    {
        public SpriteRenderer Renderer;
        public Canvas ConstructUi;

        protected override void Register()
        {
            ActiveUi(Ent.HasAllOfTags<ConstructionBuilt>());
        }

        public void ActiveUi(bool active)
        {
            ConstructUi.Active(active);
        }
    }
}