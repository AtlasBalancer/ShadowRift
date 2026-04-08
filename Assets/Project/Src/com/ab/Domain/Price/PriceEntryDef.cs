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
            W.RegisterTagType<PriceAvailableTag>();
            W.RegisterTagType<PriceRegisterTag>();
            W.RegisterTagType<PriceBuyTag>();

            W.RegisterComponentType<PriceRef>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new PriceRegisterSystem(Def.PriceRegisterSystem));
            Sys.AddUpdate(new PriceCheckAvailableSystem());
            Sys.AddUpdate(new PriceBuySystem());
        }
    }
}