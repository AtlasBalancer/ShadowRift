using System;
using System.Threading;
using com.ab.common;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.domain.item;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using Object = UnityEngine.Object;
using com.ab.item;

namespace Project.Src.com.ab.Domain.Collect
{
    public class PlacedSpawnSystem : IPreInitLoad, IInitSystem, IUpdateSystem
    {
        public PlacedSpawnSystem(Settings def)
        {
            _def = def;
            _atlas = W.Context<AtlasService>.Get();
        }

        readonly Settings _def;
        readonly AtlasService _atlas;

        public UniTask PreInitLoad(CancellationToken ct) =>
            _atlas.LoadAtlas(_def.AtlasKey);

        public void Init()
        {
        }

        public void Update()
        {
            foreach (var ent in W.Query.Entities<TagAll<PlacedSpawnByDropTable>>())
            {
                var @ref = ent.Ref<Ref>().Val;
                var dropTable = ent.GetConfigTable<DropEntry>().Items;
                
                foreach (var item in dropTable)
                {
                    if (!item.ChanceRange.RandHappen())
                        continue;

                    item.PlaceSo.GetConfig<ItemEntry>(out var itemDef, out _);
                    var link = Object.Instantiate(_def.PlacedPrefab, _def.RootCollectables);
                    var itemEnt = link.Init(item.PlaceSo, true);
                    itemEnt.Add(new Amount { Val = item.AmountRange.Rand() });
                    link.UpdateRender(_atlas.GetSprite(_def.AtlasKey, itemDef.AKSprite));

                    link.Drop(@ref.position);
                }

                ent.ApplyTag<PlacedSpawnByDropTable>(false);
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