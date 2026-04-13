using System;
using com.ab.common;
using com.ab.complexity.core;
using com.ab.core;
using Project.Src.com.ab.Domain.Collect;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.complexity.features.player
{
    [InfoBox("Зависит от (HarvesterEntryDef, InventoryEntryDef)")]
    public class PlayerEntryDef : StaticEntryParamDef<PlayerEntryDef.Settings>,
        IStaticInitDef, IStaticUpdateDef, IStaticLastInitStageDef
    {
        [Serializable]
        public class Settings
        {
            public Transform Root;
            public Transform SpawnPoint;
            public MovementSamePositionMono MainCamera;
            public PlayerMono PlayerPrefab;
        }

        public void RegisterInit()
        {
            // Debug.Log($"{nameof(PlayerEntryDef)}::{nameof(RegisterInit)}");
        }

        public void RegisterUpdate()
        {
            // Debug.Log($"{nameof(PlayerEntryDef)}::{nameof(RegisterUpdate)}");
        }

        public void LastInit()
        {
            var player = Instantiate(Def.PlayerPrefab, Def.Root);
            player.transform.position = Def.SpawnPoint.position;

            var ent = player.Init(true);

            ent.Set(new MovementEntry { Speed = .5f });
            ent.Set(new AnimatorRef { Value = player.Animator });

            ent.Set(new PlacedToInventory() { Radius = 1f, CollectTimer = new Timer { Max = .5f } });
            // Camera
            Def.MainCamera.UpdateTarget(player.transform);
        }
    }
}