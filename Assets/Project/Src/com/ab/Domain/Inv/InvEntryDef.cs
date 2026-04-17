using System;
using com.ab.common;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvEntryDef : StaticEntryParamDef<InvEntryDef.Settings>, IStaticContextSetDef,
        IStaticUpdateDef
    {
        public void SetContext()
        {
        }

        public void RegisterUpdate()
        {
            Sys.Add(new InvModelSystem());
            Sys.Add(new InvViewSystem(Def.InventoryInitViewSystem));
        }

        [Serializable]
        public class Settings
        {
            public InvViewSystem.Settings InventoryInitViewSystem;
        }
    }

    public class InventoryInitSystem
    {
    }
}