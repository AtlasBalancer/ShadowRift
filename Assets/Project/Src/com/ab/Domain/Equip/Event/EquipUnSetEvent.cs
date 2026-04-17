using com.ab.common;
using FFS.Libraries.StaticEcs;

namespace com.ab.domain.equip
{
    public readonly struct EquipUnSetEvent : IEvent
    {
        public readonly World<WT>.Entity Ent;

        public EquipUnSetEvent(World<WT>.Entity ent)
        {
            Ent = ent;
        }
    }
}