using System;
using com.ab.complexity.core;
using com.ab.core;

namespace com.ab.domain.price
{
    public class PriceEntryDef : StaticEntryParamDef<PriceEntryDef.Settings>, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public PriceRegisterSystem.Settings PriceRegisterSystem;
        }

        public void RegisterUpdate()
        {
            Sys.Add(new PriceRegisterSystem(Def.PriceRegisterSystem));
            Sys.Add(new PriceCheckAvailableSystem());
            Sys.Add(new PriceBuySystem());
        }
    }
}