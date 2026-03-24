using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.domain.equip
{
    public readonly struct EquipUnSetEvent : IEvent
    {
        public readonly W.Entity Ent;

        public EquipUnSetEvent(W.Entity ent) => Ent = ent;
    }
}