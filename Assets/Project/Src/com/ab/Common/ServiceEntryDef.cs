using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "ServiceEntryDef", menuName = "com.ab/common/ServiceEntry")]
    public class ServiceEntryDef : StaticEntrySOParamDef<ServiceEntryDef.Settings>,
        IStaticContextSetDef
    {
        public void SetContext()
        {
            W.SetResource(new AddressableService());
            W.SetResource(new LocalizationService());
            W.SetResource(new AtlasService(Def.AtlasService));
        }

        public UniTask PreInitWait(CancellationToken ct)
        {
            return W.GetResource<LocalizationService>().InitializeAsync();
        }

        [Serializable]
        public class Settings
        {
            public AtlasService.Settings AtlasService;
        }
    }
}