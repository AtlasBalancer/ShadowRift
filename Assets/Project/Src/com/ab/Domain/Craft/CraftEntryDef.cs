using System;
using com.ab.complexity.core;

namespace com.ab.domain.craft
{
    public class CraftEntryDef : StaticEntryParamDef<CraftEntryDef.Settings>, IStaticRegisterTypeDef, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public CraftViewSystem.Settings CraftViewSystem;
        }

        public void RegisterType()
        {
            W.RegisterComponentType<CraftCommand>();
            W.RegisterComponentType<CraftItemRef>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new CraftViewSystem(Def.CraftViewSystem));
        }
    }
}