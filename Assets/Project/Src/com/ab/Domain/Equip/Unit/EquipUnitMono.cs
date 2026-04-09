using System;
using com.ab.common;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Common.Unity;
using Sirenix.Utilities;

namespace com.ab.domain.equip
{
    [Serializable]
    public class IDBySlot : SerializableMap<ConfigIDEntSo, EquipUnitItemSlotMono>
    {
        public void Init()
        {
            _map.ForEach(item => item.Value.Init());
            
            HideAll();
        }
        
        public void HideAll() =>
            _map.ForEach(item => item.Value.Render.enabled = false);
    }

    public class EquipUnitMono : EntityLink
    {
        public IDBySlot Slots;

        protected override void Register()
        {
            Slots.Init();

            Ent.Apply<EquipTag>(true);
            Ent.Set(new EquipUnitRef(this));
            W.SendEvent(new EquipUnitRegisterEvent(this));
        }
    }
}