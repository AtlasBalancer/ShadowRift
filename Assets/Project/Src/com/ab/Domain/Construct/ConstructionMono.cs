using UnityEngine;
using com.ab.common;
using com.ab.complexity.core;
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
        public Collider2D Collider;
        public SpriteRenderer Renderer;
        public Canvas ConstructUi;

        protected override void Register()
        {
            Ent.Add(new ConstructionRef(this));
            ActiveUi(Ent.HasAllOfTags<ConstructionBuilt>());
        }

        public void ActiveConstruction(bool active)
        {
            Collider.enabled = active;
            Renderer.Active(active);
        }
        
        public void ActiveUi(bool active)
        {
            ConstructUi.Active(active);
        }
    }
}