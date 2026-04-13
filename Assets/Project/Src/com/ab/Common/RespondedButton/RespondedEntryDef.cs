using System;
using com.ab.core;

namespace com.ab.common
{
    public class RespondedEntryDef : StaticEntryParamDef<RespondedEntryDef.Settings>,
        IStaticUpdateDef, IStaticCreateProtoEntityDef
    {
        [Serializable]
        public class Settings
        {
            public ResponseButtonRefMock Ref;
        }

        public void RegisterUpdate()
        {
            Sys.Add(new ResponseButtonSystem());
        }

        public void CreateProtoEntities()
        {
          Def.Ref.Init();
        }
    }
}