using com.ab.common;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.construct
{
    public readonly struct ConstructionRef : IComponent
    {
        public readonly ConstructionMono Val;

        public ConstructionRef(ConstructionMono val)
        {
            Val = val;
        }
    }

    public class ConstructionMono : EntityLink
    {
        public Collider2D Collider;
        public SpriteRenderer Renderer;
        public Canvas ConstructUi;

        protected override void Register()
        {
            Ent.Set(new ConstructionRef(this));
            ActiveUi(Ent.Has<ConstructionBuilt>());
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