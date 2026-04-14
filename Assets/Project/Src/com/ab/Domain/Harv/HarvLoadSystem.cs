using System;
using com.ab.common;
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
            _factory = W.GetResource<HarvFactory>();
            _persistent = W.GetResource<PersistentService>();
        }

        public void Init()
        {
        }

        public void Load()
        {
            byte[] loadedSnapshot = _persistent.Load(_def.PersistKey);
            W.Serializer.LoadEntitiesSnapshot(loadedSnapshot, entitiesAsNew: false);

            foreach (var ent in W.Query<EntityIs<HarvSpawnerEntity>>().Entities())
            {
                Debug.Log("load Entity");

                if (ent.Has<W.Links<Child>>())
                {
                    foreach (var childEnt in ent.Ref<W.Links<Child>>().AsReadOnlySpan)
                    {
                        Debug.Log(">>> load child");
                    }
                }
            }
        }

        public void Save()
        {
            using var writer = W.Serializer.CreateEntitiesSnapshotWriter();

            foreach (var ent in W.Query<EntityIs<HarvSpawnerEntity>>().Entities())
            {
                writer.Write(ent);

                Debug.Log("Add Entity");
                if (ent.Has<W.Links<Child>>())
                {
                    foreach (var childEnt in ent.Ref<W.Links<Child>>().AsReadOnlySpan)
                    {
                        if (childEnt.Value.TryUnpack<WT>(out var child))
                            writer.WriteAndUnload(child);

                        Debug.Log(">>> Add child");
                    }
                }

                ent.Unload();
            }

            byte[] snapshot = writer.CreateSnapshot();
            _persistent.Save(_def.PersistKey, snapshot);

            return;
            /*
            // var ent = W.NewEntity<HarvEntity>();

            var mono = Object.Instantiate(_def.Prefab);
            mono.transform.position = _def.PosPoint.position;
            // ent.Set<HarvRef>(new HarvRef(mono));
            ent.Set<Position>(new Position(_def.PosPoint.position));

            // using var writer = W.Serializer.CreateEntitiesSnapshotWriter();

            // writer.Write(ent);
            writer.WriteAndUnload(ent);

            byte[] saveSnapshot = writer.CreateSnapshot();
            _persistent.Save(_def.PersistKey, saveSnapshot);
            */
        }

        public void Update()
        {
        }
    }
}