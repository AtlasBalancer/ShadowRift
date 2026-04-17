using com.ab.common;
using FFS.Libraries.StaticEcs;

namespace com.ab.domain.equip
{
    public readonly struct EquipSetEvent : IEvent
    {
        public readonly World<WT>.Entity Ent;

        public EquipSetEvent(World<WT>.Entity ent)
        {
            Ent = ent;
        }
    }
}