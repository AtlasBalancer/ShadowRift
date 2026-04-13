using System;
using com.ab.complexity.core;
using com.ab.core;

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
            Sys.Add(new CraftViewSystem(Def.CraftViewSystem));
        }
    }
}