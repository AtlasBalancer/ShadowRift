using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Equip.Pupet
{
    public readonly struct EquipPuppetRef : IComponent
    {
        public readonly EquipPuppetMono Val;
        
        public EquipPuppetRef(EquipPuppetMono val) => 
            Val = val;
    }
}