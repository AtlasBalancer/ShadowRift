using System;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.complexity.player;
using Project.Src.com.ab.Complexity.Core.Static.Mono;
using Project.Src.com.ab.Domain.Collect;
using Project.Src.com.ab.Domain.Harvest;
using Sirenix.OdinInspector;

namespace com.ab.complexity.features.player
{
    [InfoBox("Зависит от (HarvesterEntryDef, InventoryEntryDef)")]
    public class PlayerEntryDef : StaticEntryParamDef<PlayerEntryDef.Settings>,
        IStaticTagDef, IStaticRegisterTypeDef, IStaticInitDef, IStaticUpdateDef, IStaticCreateEntityDef
    {
        [Serializable]
        public class Settings
        {
            public Transform Root;
            public Camera MainCamera;
            public PlayerMono PlayerPrefab;
        }

        public void RegisterTag()
        {
            W.RegisterComponentType<PlayerRef>();
            W.RegisterTagType<PlayerTag>();
        }

        public void RegisterType()
        {
            W.RegisterComponentType<Tool>();
        }

        public void RegisterInit()
        {
            // Debug.Log($"{nameof(PlayerEntryDef)}::{nameof(RegisterInit)}");
        }

        public void RegisterUpdate()
        {
            // Debug.Log($"{nameof(PlayerEntryDef)}::{nameof(RegisterUpdate)}");
        }

        public void CreateEntities()
        {
            var player = Instantiate(Def.PlayerPrefab, Def.Root);

            var logicRenderer = player.GetComponent<LogicRendererMono>();
            var playerRef = player.transform;
            var cameraRef = Def.MainCamera.transform;
            var harvester = player.Harvester;

            var ent = W.Entity.New();
            player.Ent = ent;

            ent.SetTag<PlayerTag>();
            ent.SetTag<JoystickEnable>();

            ent.Add(new PlayerRef { Ref = player });
            ent.Add(new Ref { Value = playerRef });
            ent.Add(new MovementEntry { Speed = .005f });
            ent.Add(new LogicRender(logicRenderer.Renderer));
            ent.Add(new AnimatorRef { Value = player.Animator });
            ent.Add(new HarvestCollector
            {
                Radius = harvester.Radius,
                Timer = new Timer { Max = harvester.Delay }
            });

            ent.Add(new PlacedToInventory() { Radius = 1f, CollectTimer = new Timer { Max = .5f } });
            // Camera
            ent.Add(new MovementSamePosition { PositionSource = playerRef, UpdateSource = cameraRef });
        }
    }
}