using System;
using com.ab.complexity.core;

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
            SysReg.Add(new InvModelSystem());
            SysReg.Add(new InvViewSystem(Def.InventoryInitViewSystem));
        }
    }

    public class InventoryInitSystem
    {
    }
}