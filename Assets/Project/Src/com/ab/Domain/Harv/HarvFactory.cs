using System;
using UnityEngine;
using com.ab.common;
using com.ab.common.ProgressBar;
using com.ab.core;
using Sirenix.OdinInspector;

namespace com.ab.domain.harv
{
    public class HarvFactory : PrefabFactoryPooled<HarvMono>
    {
        [Serializable]
        public class Settings : PrefabFactoryPooled<HarvMono>.Settings
        {
            [ReadOnly] public string AtlasKey = "HarvAtlas";
        }

        protected Settings _def;
        protected AtlasService _atlas;

        public HarvFactory(Settings def) : base(def)
        {
            _def = def;
            _atlas = W.GetResource<AtlasService>();
        }

        public W.Entity CreateSpawner(HarvSpawnerDef def, bool loop = false)
        {
            var ent = W.NewEntity<HarvSpawnerEntity>();

            ent.Set(def);
            ent.SetTimer(def.DelayRange, true);
            ent.Set(new HarvAvailablePositions(def.Layer.GetPositions()));
            
            if (loop)
                ent.Set<UpdateTag>();
            
            return ent;
        }


        public HarvMono CreateLink(ConfigIDEntSo id, W.Entity spawnerEnt, Vector3 position)
        {
            if (!id.GetConfig<HarvItemEntry>(out var harvDef, out _))
                throw new ArgumentException($"{nameof(HarvFactory)}::{nameof(CreateLink)}: " +
                                            $"Can't find def from {nameof(HarvItemEntry)}");

            var link = base.CreateLink();
            link.transform.SetParent(_def.SpawnContainer);
            link.transform.position = position;

            var sprite = _atlas.GetSprite(_def.AtlasKey, harvDef.AKSprite);
            link.SetSprite(sprite);

            link.Init(id, true);
            int amount = harvDef.AmountRange.Rand();
            link.Ent.Set(new Amount(amount));
            link.Ent.Ref<ProgressBarRef>().Val.SetMax(amount);
            link.Ent.Set(new W.Link<Parent>(spawnerEnt));
            
            link.ProgressBar.OffsetY(harvDef.ProgressBarOffset);

            return link;
        }

        public override void BuildLink(HarvMono link)
        {
            link.Init<HarvEntity>(true);
        }
    }
}