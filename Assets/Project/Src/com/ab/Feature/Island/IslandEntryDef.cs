using System;
using com.ab.common;

namespace Project.Src.com.ab.Feature.Island
{
    public class IslandEntryDef : StaticEntryParamDef<IslandEntryDef.Settings>, IStaticUpdateDef
    {
        public void RegisterUpdate()
        {
            // W.New
        }

        [Serializable]
        public class Settings
        {
        }
    }
}