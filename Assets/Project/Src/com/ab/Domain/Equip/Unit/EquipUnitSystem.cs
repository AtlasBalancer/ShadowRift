using System;
using com.ab.common;
using com.ab.domain.item;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;

namespace com.ab.domain.equip
{
    public class EquipUnitSystem : ISystem
    {
        readonly AtlasService _atlas;

        readonly Settings _def;
        readonly EventReceiver<WT, EquipUnitRegisterEvent> _registerReceiver;

        readonly EventReceiver<WT, EquipSetEvent> _setReceiver;
        readonly EventReceiver<WT, EquipUnSetEvent> _unSetReceiver;

        EquipUnitMono _view;

        public EquipUnitSystem(Settings def)
        {
            _def = def;
            _atlas = W.GetResource<AtlasService>();

            _setReceiver = W.RegisterEventReceiver<EquipSetEvent>();
            _unSetReceiver = W.RegisterEventReceiver<EquipUnSetEvent>();
            _registerReceiver = W.RegisterEventReceiver<EquipUnitRegisterEvent>();
        }

        public void Update()
        {
            foreach (var @event in _registerReceiver)
                _view = @event.Value.Val;

            foreach (var @event in _setReceiver)
            {
                var ent = @event.Value.Ent;
                var itemDef = ent.GetConfigTable<ItemEntry>();

                var slot = GetSlot(ent);
                slot.Render.sprite = _atlas.GetSprite(_def.ItemAtlas, itemDef.AKSprite);
                slot.Render.enabled = true;

                slot.Ent.Apply<EquipTag>(true);
            }

            foreach (var @event in _unSetReceiver)
            {
                var ent = @event.Value.Ent;

                var slot = GetSlot(ent);
                slot.Render.sprite = null;
                slot.Render.enabled = false;

                slot.Ent.Apply<EquipTag>(false);
            }
        }

        EquipUnitItemSlotMono GetSlot(World<WT>.Entity ent)
        {
            var equip = ent.GetConfigTable<EquipEntry>().Type;
            return _view.Slots.Get(equip);
        }

        [Serializable]
        public class Settings
        {
            [ReadOnly] public string ItemAtlas = "ItemsAtlas";
        }
    }
}