using FFS.Libraries.StaticEcs;

namespace com.ab.domain.equip
{
    public readonly struct EquipUnitRegisterEvent : IEvent
    {
        public readonly EquipUnitMono Val;

        public EquipUnitRegisterEvent(EquipUnitMono val) => Val = val;
    }
}