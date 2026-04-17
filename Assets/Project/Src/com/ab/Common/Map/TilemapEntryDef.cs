using System;

namespace com.ab.common.Map
{
    public class TilemapEntryDef : StaticEntryParamDef<TilemapEntryDef.Settings>, IStaticLastInitStageDef
    {
        public void LastInit()
        {
            var baker = new TilemapOcclusionBakerService(Def.TilemapOcclusionBakerService);
            baker.BakeOcclusion();
        }

        [Serializable]
        public class Settings
        {
            public TilemapOcclusionBakerService.Settings TilemapOcclusionBakerService;
        }
    }
}