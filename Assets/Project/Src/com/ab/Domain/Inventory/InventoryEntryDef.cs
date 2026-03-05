using System;
using com.ab.complexity.core;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryEntryDef : StaticEntryParamDef<InventoryEntryDef.Settings>, IStaticRegisterTypeDef, IStaticContextSetDef,
        IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public InventoryViewSystem.Settings InventoryInitViewSystem;
        }

        public void SetContext()
        {
            W.Context<InventoryService>.Set(new InventoryService());
        }

        public void RegisterType()
        {
            W.RegisterComponentType<InventoryMaterial>();
            W.RegisterComponentType<InventoryItem>();
            W.RegisterComponentType<InventoryAmount>();
            
            W.Events.RegisterEventType<InventoryViewActive>();
            W.Events.RegisterEventType<InventoryAddMaterial>();
            W.Events.RegisterEventType<InventoryAddItem>();
            W.Events.RegisterEventType<InventoryRemoveMaterial>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new InventoryManageSystem());
            Sys.AddUpdate(new InventoryViewSystem(Def.InventoryInitViewSystem));
            Sys.AddUpdate(new InventoryDeleteSystem());
        }
    }
}