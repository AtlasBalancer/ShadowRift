using System;
using com.ab.complexity.core;

namespace com.ab.common
{
    public class RespondedEntryDef : StaticEntryParamDef<RespondedEntryDef.Settings>,
        IStaticUpdateDef, IStaticRegisterTypeDef, IStaticCreateProtoEntityDef
    {
        [Serializable]
        public class Settings
        {
            public ResponseButtonRefMock Ref;
        }


        public void RegisterType()
        {
            W.RegisterTagType<ResponseClick>();
            W.RegisterComponentType<ResponseButtonRef>();
            W.RegisterComponentType<AttachRef>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new ResponseButtonSystem());
        }

        public void CreateProtoEntities()
        {
          Def.Ref.Init();
        }
    }
}