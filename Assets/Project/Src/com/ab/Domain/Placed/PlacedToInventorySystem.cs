using System;
using com.ab.common;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Collect
{
    public struct PlacedToInventory : IComponent
    {
        public Timer CollectTimer;
        public float Radius;
    }

    public class PlacedToInventorySystem : IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public LayerMask Layer;
            public float FlyDuration = 0.1f;
        }

        readonly Settings _def;

        public PlacedToInventorySystem(Settings def) =>
            _def = def;

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var ent in W.Query.Entities<All<Ref, PlacedToInventory>>())
            {
                ref var collector = ref ent.Ref<PlacedToInventory>();

                if (!collector.CollectTimer.Next(deltaTime))
                    continue;

                ref var @ref = ref ent.Ref<Ref>();

                var sourcePosition = @ref.Val.position;
                var item = Physics2D.OverlapCircle(sourcePosition, collector.Radius, _def.Layer);

                if (item != null && item.TryGetComponent(out PlacedMono collect))
                {
                    collect.FlyTo(sourcePosition, _def.FlyDuration);
                    collect.Ent.ApplyTag<InventoryAdd>(true);
                    collect.Ent.Destr(_def.FlyDuration);
                }
            }
        }
    }
}