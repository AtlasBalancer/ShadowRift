using System;
using com.ab.common;
using com.ab.common.ProgressBar;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.complexity.player;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Collect;

namespace com.ab.domain.harv
{
    public class HarvCollectSystem : ISystem
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

            foreach (var ent in W.Query<All<Ref, HarvCollectorRef>>().Entities())
            {
                var animator = ent.Ref<AnimatorRef>().Value;

                if (ent.Has<Movement>())
                {
                    animator.SetBool(HarvConst.HARVEST_KEY, false);
                    continue;
                }

                ref var @ref = ref ent.Ref<Ref>();
                ref var harvestrer = ref ent.Ref<HarvCollectorRef>();

                if (!harvestrer.Timer.Next(delta))
                    continue;
                
                var interactionPosition = harvestrer.Ref.InteractionPoint.position;
                var item = Physics2D.OverlapCircle(interactionPosition, harvestrer.Radius, _def.Layer);
                bool harvest = item != null;

                if (harvest && item.TryGetComponent<HarvMono>(out var harvRef))
                {
                    harvRef.Ent.Apply<PlacedSpawnByDropTable>(true);
                }

                bool hasTool = harvestrer.Ref.WorkingPart.Equiped();

                animator.SetBool(HarvConst.HAS_TOOL_KEY, hasTool);
                animator.SetBool(HarvConst.HARVEST_KEY, harvest);
            }

            foreach (var ent in W.Query<All<HarvRef, ProgressBarRef, AmountUpdate>>().Entities())
            {
                int amountUpdate = ent.Ref<AmountUpdate>().Val;
                ent.Delete<AmountUpdate>();
                
                var progressBar = ent.Ref<ProgressBarRef>().Val;
                ref var amountRef = ref ent.Ref<Amount>();
                amountRef.Val += amountUpdate;

                if (amountRef.Val <= 0)
                {
                    var refMono = ent.Ref<HarvRef>().Val;
                    UnityEngine.Object.Destroy(refMono.gameObject);
                    ent.Destroy();
                    continue;
                }
                
                progressBar.UpdateAmount(amountRef.Val);
            }
        }
    }
}