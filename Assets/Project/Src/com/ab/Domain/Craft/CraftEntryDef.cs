using System;
using com.ab.common;

namespace com.ab.domain.craft
{
    public class CraftEntryDef : StaticEntryParamDef<CraftEntryDef.Settings>, IStaticUpdateDef
    {
        public void RegisterUpdate()
        {
            Sys.Add(new CraftViewSystem(Def.CraftViewSystem));
        }

        [Serializable]
        public class Settings
        {
            public CraftViewSystem.Settings CraftViewSystem;
        }
    }
}