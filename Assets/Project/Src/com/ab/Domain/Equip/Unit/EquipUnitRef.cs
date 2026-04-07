using FFS.Libraries.StaticEcs;

namespace com.ab.domain.equip
{
    public readonly struct EquipUnitRef : IComponent
    {
        public readonly EquipUnitMono Val;

        public EquipUnitRef(EquipUnitMono val) =>
            Val = val;
    }
}