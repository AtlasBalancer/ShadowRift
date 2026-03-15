using System;
using com.ab.complexity.core;
using com.ab.domain.inventory;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryEntryDef : StaticEntryParamDef<InventoryEntryDef.Settings>, IStaticRegisterTypeDef, IStaticContextSetDef,
        IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public InvCategoryTable CategoryTable;
            public InvItemTable ItemTable;
            public InventoryViewSystem.Settings InventoryInitViewSystem;
        }


        public void SetContext()
        {
            // W.Context<InventoryService>.Set(new InventoryService(Def.ItemTable));
        }

        public void RegisterType()
        {
            W.RegisterTagType<Inventory>();
            W.RegisterTagType<InventoryAdd>();
            W.RegisterTagType<InventoryToUpdate>();
            
            W.RegisterComponentType<InventoryMaterial>();
            W.RegisterComponentType<InventoryItem>();
            W.RegisterComponentType<InventoryAmount>();
            W.RegisterComponentType<InvCategoryRef>();
            
            W.RegisterComponentType<InvItemRef>();
            
            W.Events.RegisterEventType<InventoryViewActive>();
            W.Events.RegisterEventType<InventoryRemoveMaterial>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new InventoryManageSystem());
            SysReg.AddUpdate(new InventoryViewSystem(Def.InventoryInitViewSystem));
        }
    }

    public class InventoryInitSystem
    {
    }
}