using System;
using com.ab.common;
using com.ab.common.ProgressBar;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.domain.harv
{
    public class HarvFactory : PrefabFactoryPooled<HarvMono>
    {
        protected AtlasService _atlas;

        protected Settings _def;

        public HarvFactory(Settings def) : base(def)
        {
            _def = def;
            _atlas = W.GetResource<AtlasService>();
        }

        public World<WT>.Entity CreateSpawner(HarvSpawnerDef def, bool loop = false)
        {
            var ent = W.NewEntity<HarvSpawnerEntity>();

            ent.Set(def);
            ent.SetTimer(def.DelayRange, true);
            ent.Set(new HarvAvailablePositions(def.Layer.GetPositions()));

            if (loop)
                ent.Set<UpdateTag>();

            return ent;
        }


        public HarvMono CreateLink(ConfigIDEntSo id, World<WT>.Entity spawnerEnt, Vector3 position)
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
            var amount = harvDef.AmountRange.Rand();
            link.Ent.Set(new Amount(amount));
            link.Ent.Ref<ProgressBarRef>().Val.SetMax(amount);
            link.Ent.Set(new World<WT>.Link<Parent>(spawnerEnt));

            link.ProgressBar.OffsetY(harvDef.ProgressBarOffset);

            return link;
        }

        public override void BuildLink(HarvMono link)
        {
            link.Init<HarvEntity>(true);
        }

        [Serializable]
        public class Settings : PrefabFactoryPooled<HarvMono>.Settings
        {
            [ReadOnly] public string AtlasKey = "HarvAtlas";
        }
    }
}