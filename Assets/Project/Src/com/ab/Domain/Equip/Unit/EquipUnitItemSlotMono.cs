using com.ab.common;
using UnityEngine;

namespace com.ab.domain.equip
{
    public class EquipUnitItemSlotMono : EntityLink
    {
        public SpriteRenderer Render;

        public bool Equiped()
        {
            return Ent.Has<EquipTag>();
        }
    }
}