using System;
using com.ab.complexity.core;
using com.ab.core;

namespace com.ab.common.Map
{
    public class TilemapEntryDef : StaticEntryParamDef<TilemapEntryDef.Settings>, IStaticLastInitStageDef
    {
        [Serializable]
        public class Settings
        {
            public TilemapOcclusionBakerService.Settings TilemapOcclusionBakerService;
        }

        public void LastInit()
        {
            var baker = new TilemapOcclusionBakerService(Def.TilemapOcclusionBakerService);
            baker.BakeOcclusion();
        }
    }
}