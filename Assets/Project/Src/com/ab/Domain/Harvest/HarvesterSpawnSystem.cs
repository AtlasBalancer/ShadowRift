using System;
using System.Collections.Generic;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Src.com.ab.Domain.Harvest
{
    public class HarvesterSpawnSystem : IInitSystem, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public float SpawnDelay;
            public Tilemap OreSpawnLayer;
            public List<HarvestableMono> HarstablePrafabs;
            public Transform SpawnContainer;
        }

        public HarvesterSpawnSystem(Settings def)
        {
            _def = def;
            _availablePositions = GetAvailablePositions(def.OreSpawnLayer);
        }

        readonly Settings _def;
        readonly List<Vector3Int> _availablePositions = default;

        public void Init()
        {
            W.Entity.New(new HarvesterSpawner { Timer = new Timer { Delay = _def.SpawnDelay, Max = _def.SpawnDelay } });
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            if (_availablePositions.Count == 0)
                return;

            foreach (var ent in W.Query.Entities<All<HarvesterSpawner>>())
            {
                ref var spawner = ref ent.Ref<HarvesterSpawner>();

                if (!spawner.Timer.Next(deltaTime))
                    continue;

                int index = Random.Range(0, _availablePositions.Count);
                var gridPosition = _availablePositions[index];
                _availablePositions.RemoveAt(index);

                var position = _def.OreSpawnLayer.CellToWorld(gridPosition);
                var harvestable =
                    Object.Instantiate(_def.HarstablePrafabs[Random.Range(0, _def.HarstablePrafabs.Count)],
                        _def.SpawnContainer);
                harvestable.transform.position = position;

                W.Entity.New(new Harvestable { Ref = harvestable });
            }
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
    }

    public class SpawnLayerDef : SerializedScriptableObject
    {
    }
}