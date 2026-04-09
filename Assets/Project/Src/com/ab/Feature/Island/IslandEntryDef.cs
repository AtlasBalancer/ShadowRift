using System;
using com.ab.complexity.core;

namespace Project.Src.com.ab.Feature.Island
{
    public class IslandEntryDef : StaticEntryParamDef<IslandEntryDef.Settings>, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            
        }

        public void RegisterUpdate()
        {
            // W.New
        }
    }
}