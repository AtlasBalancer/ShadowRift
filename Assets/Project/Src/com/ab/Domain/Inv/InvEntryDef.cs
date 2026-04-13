using System;
using com.ab.complexity.core;
using com.ab.core;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvEntryDef : StaticEntryParamDef<InvEntryDef.Settings>, IStaticContextSetDef,
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

        public void RegisterUpdate()
        {
            Sys.Add(new InvModelSystem());
            Sys.Add(new InvViewSystem(Def.InventoryInitViewSystem));
        }
    }

    public class InventoryInitSystem
    {
    }
}