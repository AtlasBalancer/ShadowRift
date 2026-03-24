using System;
using com.ab.complexity.core;
using com.ab.core;
using com.ab.domain.equip;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Src.com.ab.Domain.Equip.Pupet
{
    public class EquipPuppetSystem : ViewPresenter<EquipPuppetMono>, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public EquipPuppetMono Prefab { get; set; }
            public Transform Root { get; set; }
        }

        readonly Settings _def;
        readonly EventReceiver<WT, EquipSetEvent> _setReceiver;
        readonly EventReceiver<WT, EquipUnSetEvent> _unSetReceiver;

        public EquipPuppetSystem(Settings def)
        {
            _def = def;

            var finds = Object.FindObjectsByType<EquipPuppetMono>(FindObjectsSortMode.None);

            Debug.Log("ddd");
            
            Init(_def.Prefab, _def.Root);

            _setReceiver = W.Events.RegisterEventReceiver<EquipSetEvent>();
            _unSetReceiver = W.Events.RegisterEventReceiver<EquipUnSetEvent>();
        }

        public void Update()
        {
            foreach (var @event in _setReceiver)
            {
                var ent = @event.Value.Ent;

                // ent.
            }
        }
    }
}