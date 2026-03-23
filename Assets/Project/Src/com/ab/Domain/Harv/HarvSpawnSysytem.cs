using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using com.ab.common;
using com.ab.complexity.core;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace com.ab.domain.harv
{
    public class HarvSpawnSysytem : IPreInitLoad, IInitSystem, IUpdateSystem
    {
        public HarvSpawnSysytem(Settings def)
        {
            _def = def;
            _atlas = W.Context<AtlasService>.Get();
        }

        readonly Settings _def;
        readonly AtlasService _atlas;

        public async UniTask PreInitLoad(CancellationToken ct) =>
            await _atlas.LoadAtlas(_def.AtlasKey);

        public void Init()
        {
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

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var ent in W.Query.Entities<All<HarvSpawnLoop>>())
            {
                if (ent.Timer(deltaTime))
                    continue;

                SpawnItem(ent);
            }
        }

        void SpawnItem(W.Entity spawnerEnt)
        {
            List<Vector3Int> availablePosition = spawnerEnt.Ref<HarvAvailablePositions>().Positions;
            if (availablePosition.Count == 0)
                return;

            var gridPosition = availablePosition.RandAndRemove();

            var spawner = spawnerEnt.Ref<HarvSpawnLoop>();
            var position = spawner.Layer.CellToWorld(gridPosition);
            var harDef = spawner.ItemTable.Entries.RandVal();
            var sprite = _atlas.GetSprite(_def.AtlasKey, harDef.Item2.AKSprite);

            CreateHarvestMono(harDef.Item1, spawnerEnt, sprite, position);
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

                var veins = spawner.ItemTable.Entries.RandVal();
                var sprite = _atlas.GetSprite(_def.AtlasKey, veins.Item2.AKSprite);
                CreateHarvestMono(veins.Item1, spawnerEnt, sprite, position);
            }

            spawner.OreSpawnLayer.gameObject.SetActive(false);
        }

        void CreateHarvestMono(ConfigIDEntSo configID, W.Entity spawnerEnt, Sprite sprite, Vector3 position)
        {
            var harvLink = Object.Instantiate(_def.HarvPrefab, _def.SpawnContainer);
            harvLink.transform.position = position;
            harvLink.SetSprite(sprite);

            harvLink.Init(configID, true);
            harvLink.Ent.SetLink<Parent>(spawnerEnt);
        }

        [Serializable]
        public class Settings
        {
            public HarvMono HarvPrefab;
            public Vector3 TileOffset;

            public Transform SpawnContainer;
            public string AtlasKey;

            public List<HarvestSpawnInitDef> InitSpawners;
            public List<HarvSpawnLoop> LoopSpawners;
        }

        [Serializable]
        public struct HarvestSpawnInitDef
        {
            public Tilemap OreSpawnLayer;
            public HarvItemTable ItemTable;
        }
    }
}