using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Equip
{
    public readonly struct EquipUnitRef : IComponent
    {
        public readonly EquipUnitMono Val;

        public EquipUnitRef(EquipUnitMono val) =>
            Val = val;
    }
}