using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using com.ab.common;
using com.ab.complexity.core;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Src.com.ab.Domain.Harvest
{
    public class HarvesterSpawnSystem : IPastInitLoad, IInitSystem, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public HarvMono HarvPrefab;
            public Vector3 TileOffset;

            public Transform SpawnContainer;

            public List<HarvestSpawnInitDef> InitSpawners;
            public List<HarvestSpawnLoopDef> LoopSpawners;
        }

        [Serializable]
        public struct HarvestSpawnInitDef
        {
            public Tilemap OreSpawnLayer;
            public HarvItemTable ItemTable;
        }

        public HarvesterSpawnSystem(Settings def) => _def = def;
        readonly Settings _def;
        AddressableService _addressable;

        public void Init()
        {
            _addressable = W.Context<AddressableService>.Get();

            foreach (var spawner in _def.InitSpawners)
                InitLayer(spawner);

            foreach (var spawner in _def.LoopSpawners)
            {
                spawner.Layer.Active(false);
                var timerDelay = spawner.DelayRange.Rand();

                var ent = W.Entity.New();
                ent.SetTimer(timerDelay, true);
                ent.Add(new HarvAvailablePositions { Positions = GetAvailablePositions(spawner.Layer) });
                ent.Add(spawner);

                if (spawner.FillInit)
                    foreach (var _ in Enumerable.Range(0, ent.Ref<HarvAvailablePositions>().Positions.Count))
                        SpawnItem(ent);
            }
        }

        public async UniTask PastInitLoad(CancellationToken ct)
        {
            foreach (var item in _def.InitSpawners)
            foreach (var itemTableEntry in item.ItemTable.Entries) 
                    await _addressable.LoadAsync<GameObject>(itemTableEntry.Value.AKSprite);
            
            foreach (var item in _def.LoopSpawners)
            foreach (var itemTableEntry in item.ItemTable.Entries) 
                    await _addressable.LoadAsync<GameObject>(itemTableEntry.Value.AKSprite);
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var ent in W.Query.Entities<All<HarvestSpawnLoopDef>>())
            {
                if (ent.Timer(deltaTime))
                    continue;

                SpawnItem(ent);
            }
        }

        void SpawnItem(World<WT>.Entity parentEnt)
        {
            List<Vector3Int> availablePosition = parentEnt.Ref<HarvAvailablePositions>().Positions;
            if (availablePosition.Count == 0)
                return;

            var gridPosition = availablePosition.RandAndRemove();

            var spawner = parentEnt.Ref<HarvestSpawnLoopDef>();
            var position = spawner.Layer.CellToWorld(gridPosition);
            // var harDef = spawner.HarvTable.Entries.RandVal();

            // CreateHarvestItem(parentEnt, harDef.Item1, position, harDef.Item2.Prefab);
        }

        void CreateHarvestItem(
            World<WT>.Entity parentEnt,
            IDEntSo idEntSo,
            Vector3 position,
            SpriteRenderer prefab)
        {
            var item = CreateHarvestMono(prefab, position);
            var ent = item.Ent;
            ent.Add(new IDRef(idEntSo));
            ent.Add(new Ref { Value = item.transform });
            ent.Add(new HarvestSpawnItemRef { Ref = item });
            ent.SetLink<Parent>(parentEnt);
        }

        List<Vector3Int> GetAvailablePositions(Tilemap map)
        {
            List<Vector3Int> positions = new List<Vector3Int>();
            foreach (var pos in map.cellBounds.allPositionsWithin)
            {
                if (map.HasTile(pos))
                    positions.Add(pos);
            }

            return positions;
        }

        void InitLayer(HarvestSpawnInitDef spawner)
        {
            var positions = GetAvailablePositions(spawner.OreSpawnLayer);
            var spawnerEnt = W.Entity.New();

            foreach (var gridPosition in positions)
            {
                var position = spawner.OreSpawnLayer.CellToWorld(gridPosition);

                // var veins = spawner.Veins.Entries;

                // var key = veins.Keys.ElementAt(Random.Range(0, veins.Count));
                // var prefab =  _addressable.TryGet(veins[key].AKSprite);

                // CreateHarvestItem(spawnerEnt, key, position, prefab);
            }

            spawner.OreSpawnLayer.gameObject.SetActive(false);
        }

        HarvMono CreateHarvestMono(SpriteRenderer prefab, Vector3 position)
        {
            var harvLink = Object.Instantiate(_def.HarvPrefab, _def.SpawnContainer);
            harvLink.transform.position = position;

            var item = Object.Instantiate(prefab);
            item.transform.SetParent(harvLink.transform, false);
            item.transform.localPosition = _def.TileOffset;

            harvLink.Init();
            return harvLink;
        }
    }
}