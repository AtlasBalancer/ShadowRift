using System;
using com.ab.common;

namespace com.ab.domain.price
{
    public class PriceEntryDef : StaticEntryParamDef<PriceEntryDef.Settings>, IStaticUpdateDef
    {
        public void RegisterUpdate()
        {
            Sys.Add(new PriceRegisterSystem(Def.PriceRegisterSystem));
            Sys.Add(new PriceCheckAvailableSystem());
            Sys.Add(new PriceBuySystem());
        }

        [Serializable]
        public class Settings
        {
            public PriceRegisterSystem.Settings PriceRegisterSystem;
        }
    }
}