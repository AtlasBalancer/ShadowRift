using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using com.ab.common;
using com.ab.common.ProgressBar;
using com.ab.complexity.core;
using com.ab.core;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace com.ab.domain.harv
{
    public class HarvSpawnSystem : ISystem
    {
        public HarvSpawnSystem(Settings def)
        {
            _def = def;
            _factory = W.GetResource<HarvFactory>();
        }

        readonly Settings _def;
        readonly HarvFactory _factory;

        public void Init()
        {
            foreach (var spawner in _def.InitSpawners)
                InitLayer(spawner);

            foreach (var spawnerDef in _def.LoopSpawners)
            {
                var ent = _factory.CreateSpawner(spawnerDef);
                
                spawnerDef.Layer.Active(false);
                if (spawnerDef.FillInit)
                    foreach (var _ in Enumerable.Range(0, ent.Ref<HarvAvailablePositions>().Val.Count))
                        SpawnItem(ent);
            }
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var ent in W.Query<All<HarvSpawnerDer>>().Entities())
            {
                if (ent.Timer(deltaTime))
                    continue;

                SpawnItem(ent);
            }
        }

        void SpawnItem(W.Entity spawnerEnt)
        {
            List<Vector3Int> availablePosition = spawnerEnt.Ref<HarvAvailablePositions>().Val;
            if (availablePosition.Count == 0)
                return;

            var gridPosition = availablePosition.RandAndRemove();

            var spawner = spawnerEnt.Ref<HarvSpawnerDer>();
            var position = spawner.Layer.CellToWorld(gridPosition);
            var harDef = spawner.ItemTable.Entries.RandVal();

            _factory.CreateLink(harDef.Item1, spawnerEnt, position);
        }


        void InitLayer(HarvestSpawnInitDef spawner)
        {
            var positions = spawner.OreSpawnLayer.GetPositions();
            var spawnerEnt = W.NewEntity<Default>();

            foreach (var gridPosition in positions)
            {
                var position = spawner.OreSpawnLayer.CellToWorld(gridPosition);

                var vein = spawner.ItemTable.Rand();
                _factory.CreateLink(vein, spawnerEnt, position);
            }

            spawner.OreSpawnLayer.gameObject.SetActive(false);
        }

        [Serializable]
        public class Settings
        {
            public List<HarvestSpawnInitDef> InitSpawners;
            public List<HarvSpawnerDer> LoopSpawners;
        }

        [Serializable]
        public struct HarvestSpawnInitDef
        {
            public Tilemap OreSpawnLayer;
            public List<ConfigIDEntSo> ItemTable;
        }
    }
}