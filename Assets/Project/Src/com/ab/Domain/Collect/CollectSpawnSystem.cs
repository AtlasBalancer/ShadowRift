using System;
using UnityEngine;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.ItemTable;
using Object = UnityEngine.Object;

namespace Project.Src.com.ab.Domain.Collect
{
    public class CollectSpawnSystem : IInitSystem, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public Transform RootCollectables;
        }

        public CollectSpawnSystem(Settings def) => _def = def;

        Settings _def;
        ItemTableService _itemTable;
        EventReceiver<T, CollectSpawn> _spawnReceiver;

        public void Init()
        {
            _itemTable = W.Context<ItemTableService>.Get();
            _spawnReceiver = W.Events.RegisterEventReceiver<CollectSpawn>();
        }

        public void Update()
        {
            foreach (var @event in _spawnReceiver)
            {
                var id = @event.Value.ID;
                var position = @event.Value.Position;

                var dropTableId = _itemTable.Def.Harvest.Items[id].DropTableID;
                var dropTable = _itemTable.Def.DropTable.Table[dropTableId];

                foreach (var item in dropTable.Items)
                {
                    if (!item.ChanceRange.RandHappen())
                        continue;

                    var prefab = _itemTable.Def.Collect.Items[item.ResourceDefID].Prefab;

                    var collect = Object.Instantiate(prefab, _def.RootCollectables);
                    collect.ResourceDefID = item.ResourceDefID;
                    collect.Amount = item.AmountRange.Rand();
                    collect.Drop(position);
                }
            }
        }
    }
}