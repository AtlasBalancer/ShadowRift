using System;
using com.ab.complexity.core;

namespace com.ab.domain.construct
{
    public class ConstructEntryDef : StaticEntryParamDef<ConstructEntryDef.Settings>,
        IStaticRegisterTypeDef, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public ConstructViewSystem.Settings ConstructViewSystem;
        }

        public void RegisterType()
        {
            W.RegisterTagType<ConstructionBuilt>();
            
            W.RegisterComponentType<ConstructionRef>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new ConstructViewSystem(Def.ConstructViewSystem));
        }
    }
}