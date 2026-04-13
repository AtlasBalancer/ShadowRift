using System;
using com.ab.common.Persistent;
using com.ab.complexity.core;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.ab.domain.harv
{
    public readonly struct HarvLoadSystem : ISystem
    {
        [Serializable]
        public class Settings
        {
            public string PersistKey = "Harv";
            public HarvMono Prefab;
            public Transform PosPoint;
        }

        readonly Settings _def;
        readonly PersistentService _persistent;
        readonly HarvFactory _factory;

        public HarvLoadSystem(Settings def)
        {
            _def = def;
            _persistent = W.GetResource<PersistentService>();
            _factory = W.GetResource<HarvFactory>();
        }

        public void Init()
        {
            var ent = W.NewEntity<HarvEntity>();

            var mono = Object.Instantiate(_def.Prefab);
            mono.transform.position = _def.PosPoint.position;
            // ent.Set<HarvRef>(new HarvRef(mono));
            ent.Set<Position>(new Position(_def.PosPoint.position));

            using var writer = W.Serializer.CreateEntitiesSnapshotWriter();

            // writer.Write(ent);
            writer.WriteAndUnload(ent);

            byte[] saveSnapshot = writer.CreateSnapshot();
            _persistent.Save(_def.PersistKey, saveSnapshot);

            byte[] loadedSnapshot = _persistent.Load(_def.PersistKey);
            W.Serializer.LoadEntitiesSnapshot(loadedSnapshot, entitiesAsNew: true);
            
            foreach (var entity in W.Query<All<Position>>().Entities())
            {
                Debug.Log("WIN");
            }
        }

        public void Update()
        {
        }
    }
}