using System;
using com.ab.complexity.core;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvEntryDef : StaticEntryParamDef<InvEntryDef.Settings>, IStaticRegisterTypeDef, IStaticContextSetDef,
        IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public InvViewSystem.Settings InventoryInitViewSystem;
        }

        public void SetContext()
        {
            
        }

        public void RegisterType()
        {
            W.RegisterTagType<InvTag>();
            W.RegisterTagType<InventoryAdd>();
            W.RegisterTagType<InvToUpdateTag>();
            
            W.RegisterComponentType<InventoryItem>();
            W.RegisterComponentType<InvCategoryRef>();
            
            W.RegisterComponentType<InvItemRef>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new InvModelSystem());
            SysReg.AddUpdate(new InvViewSystem(Def.InventoryInitViewSystem));
        }
    }

    public class InventoryInitSystem
    {
    }
}