using System;
using System.Collections.Generic;
using System.Linq;
using com.ab.common;
using com.ab.domain.item;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace com.ab.domain.equip
{
    public class EquipPuppetSystem : ViewPresenter<EquipPuppetMono>, ISystem
    {
        readonly AtlasService _atlas;

        readonly Settings _def;
        readonly EventReceiver<WT, EquipSetEvent> _setReceiver;
        readonly Dictionary<ConfigIDEntSo, EquipSlotPair> _slots;
        readonly EventReceiver<WT, EquipUnSetEvent> _unSetReceiver;

        public EquipPuppetSystem(Settings def)
        {
            _def = def;
            _atlas = W.GetResource<AtlasService>();
            Register(_def.ViewRef, _def.ShowButton);

            _setReceiver = W.RegisterEventReceiver<EquipSetEvent>();
            _unSetReceiver = W.RegisterEventReceiver<EquipUnSetEvent>();
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

                ent.Apply<EquipTag>(true);
            }

            foreach (var @event in _setReceiver)
            {
                var ent = @event.Value.Ent;

                var pair = GetSlot(ent);
                pair.InvSlot.Image.sprite = null;
                pair.InvSlot.Image.enabled = false;

                pair.PuppetSlot.Image.sprite = null;
                pair.PuppetSlot.Image.enabled = false;

                ent.Apply<EquipTag>(false);
            }
        }

        EquipSlotPair GetSlot(World<WT>.Entity ent)
        {
            var equip = ent.GetConfigTable<EquipEntry>().Type;
            return _slots[equip];
        }

        [Serializable]
        public class Settings
        {
            public Button ShowButton;

            public List<EquipSlotPair> EquipSlots;
            [ReadOnly] public string ItemAtlas = "ItemsAtlas";
            public EquipPuppetMono ViewRef;
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