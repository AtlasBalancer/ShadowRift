using System;
using com.ab.complexity.core;

namespace com.ab.domain.price
{
    public class PriceEntryDef : StaticEntryParamDef<PriceEntryDef.Settings>, IStaticRegisterTypeDef, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public PriceRegisterSystem.Settings PriceRegisterSystem;
        }

        public void RegisterType()
        {
            W.RegisterTagType<PriceAvailable>();
            W.RegisterTagType<PriceRegister>();

            W.RegisterComponentType<PriceRef>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new PriceCheckAvailableSystem());
            Sys.AddUpdate(new PriceRegisterSystem(Def.PriceRegisterSystem));
        }
    }
}