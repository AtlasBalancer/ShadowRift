using System;
using System.Collections.Generic;
using System.Linq;
using com.ab.common;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.harv
{
    public class HarvSpawnSystem : ISystem
    {
        readonly Settings _def;
        readonly HarvFactory _factory;

        public HarvSpawnSystem(Settings def)
        {
            _def = def;
            _factory = W.GetResource<HarvFactory>();
        }

        public void Init()
        {
            foreach (var spawner in _def.InitSpawners)
                InitLayer(spawner);

            foreach (var spawnerDef in _def.LoopSpawners)
            {
                var ent = _factory.CreateSpawner(spawnerDef, true);

                spawnerDef.Layer.Active(false);
                if (spawnerDef.FillInit)
                    foreach (var _ in Enumerable.Range(0, ent.Ref<HarvAvailablePositions>().Val.Count))
                        SpawnItem(ent);
            }
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var ent in W.Query<
                         EntityIs<HarvEntity>,
                         All<UpdateTag>>().Entities())
            {
                if (ent.Timer(deltaTime))
                    continue;

                SpawnItem(ent);
            }
        }

        void SpawnItem(World<WT>.Entity spawnerEnt)
        {
            var availablePosition = spawnerEnt.Ref<HarvAvailablePositions>().Val;
            if (availablePosition.Count == 0)
                return;

            var gridPosition = availablePosition.RandAndRemove();

            var spawner = spawnerEnt.Ref<HarvSpawnerDef>();
            var position = spawner.Layer.CellToWorld(gridPosition);
            var harvID = spawner.SpawnedItems.Rand();

            _factory.CreateLink(harvID, spawnerEnt, position);
        }


        void InitLayer(HarvSpawnerDef spawnerDef)
        {
            var positions = spawnerDef.Layer.GetPositions();
            var spawnerEnt = _factory.CreateSpawner(spawnerDef);

            foreach (var gridPosition in positions)
            {
                var position = spawnerDef.Layer.CellToWorld(gridPosition);

                var vein = spawnerDef.SpawnedItems.Rand();
                _factory.CreateLink(vein, spawnerEnt, position);
            }

            spawnerDef.Layer.gameObject.SetActive(false);
        }

        [Serializable]
        public class Settings
        {
            public List<HarvSpawnerDef> InitSpawners;
            public List<HarvSpawnerDef> LoopSpawners;
        }
    }
}