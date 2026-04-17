using FFS.Libraries.StaticEcs;

namespace com.ab.domain.equip
{
    public readonly struct EquipPuppetRef : IComponent
    {
        public readonly EquipPuppetMono Val;

        public EquipPuppetRef(EquipPuppetMono val)
        {
            Val = val;
        }
    }
}