using System;
using com.ab.complexity.core;
using com.ab.core;

namespace com.ab.domain.construct
{
    public class ConstructEntryDef : StaticEntryParamDef<ConstructEntryDef.Settings>, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public ConstructViewSystem.Settings ConstructViewSystem;
        }

        public void RegisterUpdate()
        {
            Sys.Add(new ConstructViewSystem(Def.ConstructViewSystem));
        }
    }
}