using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using com.ab.common;
using com.ab.common.ProgressBar;
using com.ab.complexity.core;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace com.ab.domain.harv
{
    public class HarvSpawnSystem : IPreInitLoad, ISystem
    {
        public HarvSpawnSystem(Settings def)
        {
            _def = def;
            _atlas = W.GetResource<AtlasService>();
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

                var ent = W.NewEntity<Default>();
                ent.SetTimer(timerDelay, true);
                ent.Set(new HarvAvailablePositions { Positions = GetAvailablePositions(spawner.Layer) });
                ent.Set(spawner);

                if (spawner.FillInit)
                    foreach (var _ in Enumerable.Range(0, ent.Ref<HarvAvailablePositions>().Positions.Count))
                        SpawnItem(ent);
            }
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var ent in W.Query<All<HarvSpawnLoop>>().Entities())
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

            CreateHarvestMono(harDef.Item1, spawnerEnt, position);
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
            var spawnerEnt = W.NewEntity<Default>();

            foreach (var gridPosition in positions)
            {
                var position = spawner.OreSpawnLayer.CellToWorld(gridPosition);

                var vein = spawner.ItemTable.Rand();
                CreateHarvestMono(vein, spawnerEnt, position);
            }

            spawner.OreSpawnLayer.gameObject.SetActive(false);
        }

        void CreateHarvestMono(ConfigIDEntSo id, W.Entity spawnerEnt, Vector3 position)
        {
            var harvLink = Object.Instantiate(_def.HarvPrefab, _def.SpawnContainer);
            harvLink.transform.position = position;

            id.GetConfig<HarvItemEntry>(out var harvDef, out _);
            var sprite = _atlas.GetSprite(_def.AtlasKey, harvDef.AKSprite);
            harvLink.SetSprite(sprite);

            harvLink.Init<Default>(id, true);

            int amount = harvDef.AmountRange.Rand();
            harvLink.Ent.Set(new Amount(amount));
            harvLink.Ent.Ref<ProgressBarRef>().Val.SetMax(amount);
            // harvLink.Ent.Set(new W.Link<Parrent>spawnerEnt);
            harvLink.ProgressBar.OffsetY(harvDef.ProgressBarOffset);
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
            public List<ConfigIDEntSo> ItemTable;
        }
    }
}