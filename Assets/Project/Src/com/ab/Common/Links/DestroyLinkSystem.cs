using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public readonly struct DestroyLinkSystem : ISystem
    {
        public void Update()
        {
            var delta = Time.deltaTime;

            foreach (var ent in W.Query<All<Destroy>>().Entities())
            {
                ref var item = ref ent.Ref<Destroy>();

                if (!item.Timer.Next(delta))
                    continue;

                if (ent.Has<Ref>()) Object.Destroy(ent.Ref<Ref>().Val.gameObject);

                ent.Destroy();
            }
        }
    }
}