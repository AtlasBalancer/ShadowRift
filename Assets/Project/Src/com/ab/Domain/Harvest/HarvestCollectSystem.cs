using System;
using com.ab.common;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.complexity.domain.harvestable;
using com.ab.complexity.player;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Collect;
using Project.Src.com.ab.Domain.Harvest;

namespace com.ab.complexity.harvestable
{
    public class HarvestCollectSystem : IUpdateSystem
    {
        public HarvestCollectSystem(Settings def) =>
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

            foreach (var ent in W.Query.Entities<All<Ref, HarvestCollector>>())
            {
                var animator = ent.Ref<AnimatorRef>().Value;

                if (ent.HasAllOfTags<Movement>())
                {
                    animator.SetBool(HarvestConst.HARVEST_KEY, false);
                    continue;
                }

                ref var @ref = ref ent.Ref<Ref>();
                ref var harvestrer = ref ent.Ref<HarvestCollector>();

                if (!harvestrer.Timer.Next(delta))
                    continue;

                var item = Physics2D.OverlapCircle(@ref.Val.position, harvestrer.Radius, _def.Layer);
                bool harvest = item != null;

                if (harvest && item.TryGetComponent<HarvMono>(out var harvRef)) 
                    harvRef.Ent.ApplyTag<PlacedSpawnByDropTable>(true);

                bool hasTool = ent.HasAllOf<Tool>();
                
                animator.SetBool(HarvestConst.HAS_TOOL_KEY, hasTool);
                animator.SetBool(HarvestConst.HARVEST_KEY, harvest);
            }
        }
    }
}