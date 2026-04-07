using com.ab.common;
using com.ab.domain.equip;
using UnityEngine;

namespace com.ab.domain.harv
{
    public class HarvCollectorMono : EntityLink
    {
        public float Radius;
        public float Delay;
        public Transform InteractionPoint;
        public EquipUnitItemSlotMono WorkingPart;
        
        protected override void Register()
        {
            var @ref = new HarvCollectorRef
            {
                Ref = this,
                Radius = Radius,
                Timer = new Timer { Max = Delay }
            };

            Ent.Add(@ref);
        }
    }
}