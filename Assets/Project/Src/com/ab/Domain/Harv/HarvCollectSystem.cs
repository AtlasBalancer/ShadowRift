using System;
using com.ab.common;
using com.ab.common.ProgressBar;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Collect;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.ab.domain.harv
{
    public class HarvCollectSystem : ISystem
    {
        readonly Settings _def;

        public HarvCollectSystem(Settings def)
        {
            _def = def;
        }

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
                var harvest = item != null;

                if (harvest && item.TryGetComponent<HarvMono>(out var harvRef))
                    harvRef.Ent.Apply<PlacedSpawnByDropTable>(true);

                var hasTool = harvestrer.Ref.WorkingPart.Equiped();

                animator.SetBool(HarvConst.HAS_TOOL_KEY, hasTool);
                animator.SetBool(HarvConst.HARVEST_KEY, harvest);
            }

            foreach (var ent in W.Query<All<HarvRef, ProgressBarRef, AmountUpdate>>().Entities())
            {
                var amountUpdate = ent.Ref<AmountUpdate>().Val;
                ent.Delete<AmountUpdate>();

                var progressBar = ent.Ref<ProgressBarRef>().Val;
                ref var amountRef = ref ent.Ref<Amount>();
                amountRef.Val += amountUpdate;

                if (amountRef.Val <= 0)
                {
                    var refMono = ent.Ref<HarvRef>().Val;
                    Object.Destroy(refMono.gameObject);
                    ent.Destroy();
                    continue;
                }

                progressBar.UpdateAmount(amountRef.Val);
            }
        }

        [Serializable]
        public class Settings
        {
            public LayerMask Layer;
        }
    }
}