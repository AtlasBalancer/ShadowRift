using System;
using System.Threading;
using com.ab.core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "ServiceEntryDef", menuName = "com.ab/common/ServiceEntry")]
    public class ServiceEntryDef : StaticEntrySOParamDef<ServiceEntryDef.Settings>, 
        IStaticContextSetDef
    {
        
        [Serializable]
        public class Settings
        {
            public AtlasService.Settings AtlasService;
        }
        
        public void SetContext()
        {
            W.SetResource(new AddressableService());
            W.SetResource(new LocalizationService());
            W.SetResource(new AtlasService(Def.AtlasService));
        }
        
        public UniTask PreInitWait(CancellationToken ct) =>
            W.GetResource<LocalizationService>().InitializeAsync();
    }
}