using FFS.Libraries.StaticEcs;

namespace com.ab.domain.equip
{
    public readonly struct EquipSetEvent : IEvent
    {
        public readonly W.Entity Ent;

        public EquipSetEvent(W.Entity ent)
        {
            Ent = ent;
        }
    }
}