using System;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Collect
{
    public struct CollectorToInventory : IComponent
    {
        public Timer CollectTimer;
        public Timer ToInventoryTimer;
        public float Radius;
    }

    public class CollectToInventorySystem : IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public LayerMask Layer;
            public float FlyDuration = 0.1f;
        }

        readonly Settings _def;

        public CollectToInventorySystem(Settings def)
        {
            _def = def;
        }
        
        public void Update()
        {
            var deltaTime = Time.deltaTime;
            
            foreach (var ent in W.Query.Entities<All<Ref, CollectorToInventory>>())
            {
               ref var collector = ref ent.Ref<CollectorToInventory>();
                
                if(!collector.CollectTimer.Next(deltaTime))
                    continue;
                
                ref var @ref = ref ent.Ref<Ref>();

                var sourcePosition = @ref.Value.position;
                var item = Physics2D.OverlapCircle(sourcePosition, collector.Radius, _def.Layer);

                if (item != null && item.TryGetComponent(out CollectMono collect))
                {
                    collect.FlyTo(sourcePosition, _def.FlyDuration);
                    W.Events.Send(new InventoryAddMaterial { ID = collect.ResourceDefID, Amount = collect.Amount});
                }
            }
        }
    }
}