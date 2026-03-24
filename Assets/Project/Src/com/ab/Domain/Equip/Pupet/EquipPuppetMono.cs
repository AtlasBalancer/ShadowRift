using com.ab.domain.craft;
using com.ab.domain.equip;

namespace Project.Src.com.ab.Domain.Equip.Pupet
{
    public class EquipPuppetMono : ViewMono
    {
        protected override void Register()
        {
            Ent.ApplyTag<EquipTag>(true);
            Ent.Add(new EquipPuppetRef(this));
        }
    }
}