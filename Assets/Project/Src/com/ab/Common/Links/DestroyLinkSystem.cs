using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public readonly struct DestroyLinkSystem : IUpdateSystem
    {
        public void Update()
        {
            float delta = Time.deltaTime;

            foreach (var ent in W.Query.Entities<All<Destroy>>())
            {
                ref var item = ref ent.Ref<Destroy>();

                if (!item.Timer.Next(delta))
                    continue;

                if (ent.HasAllOf<Ref>())
                {
                    Object.Destroy(ent.Ref<Ref>().Val.gameObject);
                }

                ent.Destroy();
            }
        }
    }
}