using System;
using System.Linq;
using com.ab.core;
using com.ab.common;
using UnityEngine.UI;
using com.ab.domain.item;
using Sirenix.OdinInspector;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using System.Collections.Generic;

namespace com.ab.domain.equip
{
    public class EquipPuppetSystem : ViewPresenter<EquipPuppetMono>, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public Button ShowButton;
            public EquipPuppetMono ViewRef;

            public List<EquipSlotPair> EquipSlots;
            [ReadOnly] public string ItemAtlas = "ItemsAtlas";
        }

        readonly Settings _def;
        readonly AtlasService _atlas;
        readonly EventReceiver<WT, EquipSetEvent> _setReceiver;
        readonly EventReceiver<WT, EquipUnSetEvent> _unSetReceiver;
        readonly Dictionary<ConfigIDEntSo, EquipSlotPair> _slots;

        public EquipPuppetSystem(Settings def)
        {
            _def = def;
            _atlas = W.Context<AtlasService>.Get();
            Register(_def.ViewRef, _def.ShowButton);

            _setReceiver = W.Events.RegisterEventReceiver<EquipSetEvent>();
            _unSetReceiver = W.Events.RegisterEventReceiver<EquipUnSetEvent>();
            _slots = _def.EquipSlots.ToDictionary(item => item.Type, item => item);
        }

        public void Update()
        {
            foreach (var @event in _setReceiver)
            {
                var ent = @event.Value.Ent;
                var itemEntry = ent.GetConfigTable<ItemEntry>();

                var pair = GetSlot(ent);
                pair.InvSlot.Image.sprite = _atlas.GetSprite(_def.ItemAtlas, itemEntry.AKSprite);
                pair.InvSlot.Image.enabled = true;
                
                pair.PuppetSlot.Image.sprite = _atlas.GetSprite(_def.ItemAtlas, itemEntry.AKSprite);
                pair.PuppetSlot.Image.enabled = true;

                ent.ApplyTag<EquipTag>(true);
            }

            foreach (var @event in _setReceiver)
            {
                var ent = @event.Value.Ent;

                var pair = GetSlot(ent);
                pair.InvSlot.Image.sprite = null;
                pair.InvSlot.Image.enabled = false;

                pair.PuppetSlot.Image.sprite = null;
                pair.PuppetSlot.Image.enabled = false;
                
                ent.ApplyTag<EquipTag>(false);
            }
        }

        EquipSlotPair GetSlot(World<WT>.Entity ent)
        {
            var equip = ent.GetConfigTable<EquipEntry>().Type;
            return _slots[equip];
        }
        
        [Serializable]
        public class EquipSlotPair
        {
            public ConfigIDEntSo Type;
            public EquipPuppetSlotMono InvSlot;
            public EquipPuppetSlotMono PuppetSlot;
        }
    }
}