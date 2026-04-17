using System;
using com.ab.common;

namespace com.ab.domain.construct
{
    public class ConstructEntryDef : StaticEntryParamDef<ConstructEntryDef.Settings>, IStaticUpdateDef
    {
        public void RegisterUpdate()
        {
            Sys.Add(new ConstructViewSystem(Def.ConstructViewSystem));
        }

        [Serializable]
        public class Settings
        {
            public ConstructViewSystem.Settings ConstructViewSystem;
        }
    }
}