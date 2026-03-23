using System;
using com.ab.common;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.complexity.player;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Collect;

namespace com.ab.domain.harv
{
    public class HarvCollectSystem : IUpdateSystem
    {
        public HarvCollectSystem(Settings def) =>
            _def = def;

        [Serializable]
        public class Settings
        {
            public LayerMask Layer;
        }

        readonly Settings _def;

        public void Update()
        {
            var delta = Time.deltaTime;

            foreach (var ent in W.Query.Entities<All<Ref, HarvCollector>>())
            {
                var animator = ent.Ref<AnimatorRef>().Value;

                if (ent.HasAllOfTags<Movement>())
                {
                    animator.SetBool(HarvConst.HARVEST_KEY, false);
                    continue;
                }

                ref var @ref = ref ent.Ref<Ref>();
                ref var harvestrer = ref ent.Ref<HarvCollector>();

                if (!harvestrer.Timer.Next(delta))
                    continue;

                var item = Physics2D.OverlapCircle(@ref.Val.position, harvestrer.Radius, _def.Layer);
                bool harvest = item != null;

                if (harvest && item.TryGetComponent<HarvMono>(out var harvRef)) 
                    harvRef.Ent.ApplyTag<PlacedSpawnByDropTable>(true);

                bool hasTool = ent.HasAllOf<Tool>();
                
                animator.SetBool(HarvConst.HAS_TOOL_KEY, hasTool);
                animator.SetBool(HarvConst.HARVEST_KEY, harvest);
            }
        }
    }
}