using System;
using com.ab.complexity.core;

namespace com.ab.domain.craft
{
    public class CraftEntryDef : StaticEntryParamDef<CraftEntryDef.Settings>, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public CraftViewSystem.Settings CraftViewSystem;
        }

        public void RegisterUpdate()
        {
            SysReg.Add(new CraftViewSystem(Def.CraftViewSystem));
        }
    }
}