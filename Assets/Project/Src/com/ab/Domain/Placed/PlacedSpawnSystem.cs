using System;
using System.Buffers;
using com.ab.common;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.domain.placed;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.ItemTable;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Project.Src.com.ab.Domain.Collect
{
    public class PlacedSpawnSystem : IInitSystem, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public Transform RootCollectables;
            public PlacedMono PlacedPrefab;
            public Vector3 DropOffset = new(0, 0.5f, 0);
        }

        public PlacedSpawnSystem(Settings def) => _def = def;

        Settings _def;
        DropTableService _dropTable;
        PlacedItemTable _itemTable;

        public void Init()
        {
            _dropTable = W.Context<DropTableService>.Get();
            _itemTable = W.Context<PlacedItemTable>.Get();
        }

        public void Update()
        {
            foreach (var ent in W.Query.Entities<TagAll<PlacedSpawnByDropTable>>())
            {
                var id = ent.Ref<EntRef>();
                var @ref = ent.Ref<Ref>().Value;
                var dropTable = _dropTable.GetDrop(id.ID);

                foreach (var item in dropTable)
                {
                    if (!item.ChanceRange.RandHappen())
                        continue;

                    if (!_itemTable.Entries.TryGetValue(item.PlaceSo, out var placedDef))
                        throw new ArgumentException(
                            $"{nameof(PlacedSpawnSystem)}:: Can't find {item.PlaceSo} in PlaceItemTable");

                    var link = Object.Instantiate(_def.PlacedPrefab, _def.RootCollectables);
                    var renderer = Object.Instantiate(placedDef.SinglePrefab);
                    renderer.transform.SetParent(link.transform, false);

                    var itemEnt = link.Init();
                    // itemEnt.Add(new IDRef(item.PlaceIDSo.RuntimeID));
                    itemEnt.Add(new Amount { Value = item.AmountRange.Rand() });
                    
                    link.Drop(@ref.position + _def.DropOffset);
                }

                ent.ApplyTag<PlacedSpawnByDropTable>(false);
            }
        }
    }
}