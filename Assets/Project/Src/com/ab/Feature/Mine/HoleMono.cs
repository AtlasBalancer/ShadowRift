using com.ab.common;
using com.ab.complexity.features.player;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Project.Src.com.ab.Feature.Mine
{
    public readonly struct HoleRef : IComponent
    {
        public readonly HoleMono Val;

        public HoleRef(HoleMono val)
        {
            Val = val;
        }
    }

    public class HoleMono : EntityLink
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!Ent.Has<AvailableTag>())
                return;

            if (other.TryGetComponent<PlayerMono>(out _))
                Ent.Apply<TriggerEnterTag>(true);
        }

        protected override void Register()
        {
            Ent.Set(new HoleRef(this));
        }
    }
}