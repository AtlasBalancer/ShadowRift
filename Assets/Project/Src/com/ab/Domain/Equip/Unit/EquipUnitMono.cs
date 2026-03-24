using com.ab.common;
using com.ab.domain.equip;

namespace Project.Src.com.ab.Domain.Equip
{
    public class EquipUnitMono : EntityLink
    {
        protected override void Register()
        {
            Ent.ApplyTag<EquipTag>(true);
            Ent.Add(new EquipUnitRef(this));
        }
    }
}