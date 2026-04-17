using System;
using System.Threading;
using com.ab.common;
using com.ab.core;
using com.ab.domain.item;
using com.ab.item;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Src.com.ab.Domain.Collect
{
    public class PlacedSpawnSystem : IPreInitWait, ISystem
    {
        readonly AtlasService _atlas;

        readonly Settings _def;

        public PlacedSpawnSystem(Settings def)
        {
            _def = def;
            _atlas = W.GetResource<AtlasService>();

            IPreInitWaitRegistry.AddPreInit(this);
        }

        public UniTask PreInitWait(CancellationToken ct)
        {
            return _atlas.LoadAtlas(_def.AtlasKey);
        }

        public void Init()
        {
        }

        public void Update()
        {
            foreach (var ent in W.Query<All<PlacedSpawnByDropTable>>().Entities())
            {
                var @ref = ent.Ref<Ref>().Val;
                var amount = ent.Ref<Amount>().Val;
                var dropTable = ent.GetConfigTable<DropEntry>().Items;

                foreach (var item in dropTable)
                {
                    if (!item.ChanceRange.RandHappen())
                        continue;

                    item.PlaceSo.GetConfig<ItemEntry>(out var itemDef, out _);
                    var link = Object.Instantiate(_def.PlacedPrefab, _def.RootCollectables);
                    var itemEnt = link.Init<Default>(item.PlaceSo, true);


                    var dropAmount = item.AmountRange.Rand();

                    if (amount < dropAmount)
                        dropAmount = amount;

                    itemEnt.Set(new Amount { Val = dropAmount });
                    ent.Set(new AmountUpdate(-dropAmount));

                    link.UpdateRender(_atlas.GetSprite(_def.AtlasKey, itemDef.AKSprite));

                    link.Drop(@ref.position);
                }

                ent.Apply<PlacedSpawnByDropTable>(false);
            }
        }

        [Serializable]
        public class Settings
        {
            public Transform RootCollectables;
            public PlacedMono PlacedPrefab;
            public Vector3 DropOffset = new(0, 0.5f, 0);
            public string AtlasKey = "ItemsAtlas";
        }
    }
}