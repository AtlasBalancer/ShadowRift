using System;
using UnityEngine;
using System.Text;
using com.ab.common;
using FFS.Libraries.StaticEcs;
using System.Collections.Generic;

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

        public void Report()
        {
            foreach (var ent in W.Query<EntityIs<HarvSpawnerEntity>>().Entities()) Debug.Log("Harv spawn");

            // foreach (var ent in W.Query<EntityIs<HarvEntity>>().Entities())
            foreach (var ent in W.Query<All<HarvRef>>().Entities())
            {
                List<IComponent> result = new();
                ent.GetAllComponents(result);

                Debug.Log("Entity");


                foreach (var item in result) Debug.Log($">>> {item}");

                Debug.Log($"HarvEntity tags: {ent.TagsCount()}, comp: {ent.ComponentsCount()}");

                // var amount = ent.Ref<Amount>().Val;
                // Debug.Log($"HarvEntity {amount}");
            }
        }

        public void Save()
        {
            var sb = new StringBuilder();

            using var writer = World<WT>.Serializer.CreateEntitiesSnapshotWriter();

            foreach (var ent in W.Query<EntityIs<HarvSpawnerEntity>>().Entities())
            {
                writer.Write(ent);

                sb.Clear();
                sb.Append($"Add Entity {ent.ID}");
                if (ent.Has<World<WT>.Links<Child>>())
                    foreach (var childEnt in ent.Ref<World<WT>.Links<Child>>().AsReadOnlySpan)
                    {
                        sb.Append("\n\tHas child");

                        if (childEnt.Value.TryUnpack<WT>(out var child))
                        {
                            var amount = child.Ref<Amount>().Val;
                            sb.Append($"\n\t\tAdd child amount: {amount}");
                            writer.WriteAndUnload(child);
                        }
                    }

                Debug.Log(sb);
                ent.Unload();
            }

            var snapshot = writer.CreateSnapshot();
            _persistent.Save(_def.PersistKey, snapshot);
        }

        public void Load()
        {
            var loadedSnapshot = _persistent.Load(_def.PersistKey);
            World<WT>.Serializer.LoadEntitiesSnapshot(loadedSnapshot);

            foreach (var ent in W.Query<All<World<WT>.Link<Parent>>>().Entities())
            {
                ref readonly var parentLink = ref ent.Read<World<WT>.Link<Parent>>();
                if (parentLink.Value.TryUnpack<WT>(out var parent))
                {
                    ref var children = ref parent.Add<World<WT>.Links<Child>>();
                    children.TryAdd(ent.AsLink<Child>());
                }
            }

            var sb = new StringBuilder();

            foreach (var ent in W.Query<EntityIs<HarvSpawnerEntity>>().Entities())
            {
                sb.Clear();
                sb.Append($"Load entity {ent.ID}");

                if (ent.Has<World<WT>.Links<Child>>())
                    foreach (var childEnt in ent.Ref<World<WT>.Links<Child>>().AsReadOnlySpan)
                    {
                        sb.Append("\n\tHas child");

                        if (!childEnt.Value.TryUnpack<WT>(out var child))
                            sb.Append("\n\t\tCan't load child");
                        else
                            sb.Append($"\n\t\tLoad child {child.ID} amount: {child.Ref<Amount>().Val}");
                    }

                Debug.Log(sb);
            }
        }

        public void Update()
        {
        }
    }
}