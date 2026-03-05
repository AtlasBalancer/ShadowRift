using com.ab.complexity.core;
using Project.Src.com.ab.Domain.Inventory;
using Sirenix.OdinInspector;

namespace Project.Src.com.ab.Domain.Unit.Items
{
    public class EquipmentEntryDef : StaticEntryParamDef<EquipmentEntryDef.Settings>,
        IStaticRegisterTypeDef, IStaticUpdateDef
    {
        [Searchable]
        public class Settings
        {
        }

        public void RegisterType()
        {
            W.RegisterComponentType<UnEquipCommand>();
            W.RegisterComponentType<EquipCommand>();
            W.RegisterTagType<Equipped>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new EquipSystem());
        }
    }
}