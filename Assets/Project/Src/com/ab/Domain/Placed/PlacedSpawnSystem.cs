using System;
using System.Buffers;
using System.Threading;
using com.ab.common;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.domain.placed;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.ItemTable;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Project.Src.com.ab.Domain.Collect
{
    public class PlacedSpawnSystem : IPastInitLoad, IInitSystem, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public Transform RootCollectables;
            public PlacedMono PlacedPrefab;
            public Vector3 DropOffset = new(0, 0.5f, 0);
            public string AtlasKey = "ItemsAtlas";
        }

        public PlacedSpawnSystem(Settings def) => _def = def;

        Settings _def;
        DropTableService _dropTable;
        global::com.ab.domain.placed.ItemTable _itemTable;
        AtlasService _atlas;

        public void Init()
        {
            _atlas = W.Context<AtlasService>.Get();
            _dropTable = W.Context<DropTableService>.Get();
            _itemTable = W.Context<global::com.ab.domain.placed.ItemTable>.Get();
        }

        public void Update()
        {
            foreach (var ent in W.Query.Entities<TagAll<PlacedSpawnByDropTable>>())
            {
                var @ref = ent.Ref<Ref>().Val;
                var idEntry = ent.Ref<IDRef>().ID;
                
                var dropTable = idEntry.RuntimeID.Ref<DropEntry>().Items;
                foreach (var item in dropTable)
                {
                    if (!item.ChanceRange.RandHappen())
                        continue;

                    if (!_itemTable.Entries.TryGetValue(item.PlaceSo, out var placedDef))
                        throw new ArgumentException(
                            $"{nameof(PlacedSpawnSystem)}:: Can't find {item.PlaceSo} in PlaceItemTable");

                    var link = Object.Instantiate(_def.PlacedPrefab, _def.RootCollectables);
                    var itemEnt = link.Init(item.PlaceSo);
                    itemEnt.Add(new Amount { Value = item.AmountRange.Rand() });
                    link.UpdateRender(_atlas.GetSprite(_def.AtlasKey, placedDef.LKSprite));
                    
                    link.Drop(@ref.position + _def.DropOffset);
                }

                ent.ApplyTag<PlacedSpawnByDropTable>(false);
            }
        }

        public UniTask PastInitLoad(CancellationToken ct) => 
            _atlas.LoadAtlas(_def.AtlasKey);
    }
}