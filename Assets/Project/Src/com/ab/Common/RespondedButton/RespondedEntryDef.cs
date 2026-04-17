using System;

namespace com.ab.common
{
    public class RespondedEntryDef : StaticEntryParamDef<RespondedEntryDef.Settings>,
        IStaticUpdateDef, IStaticCreateProtoEntityDef
    {
        public void CreateProtoEntities()
        {
            Def.Ref.Init();
        }

        public void RegisterUpdate()
        {
            Sys.Add(new ResponseButtonSystem());
        }

        [Serializable]
        public class Settings
        {
            public ResponseButtonRefMock Ref;
        }
    }
}