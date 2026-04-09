using System;
using System.Threading;
using com.ab.common.ProgressBar;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.complexity.features.player;
using com.ab.core;
using com.ab.domain.item;
using com.ab.item;
using Cysharp.Threading.Tasks;
using Renderer = com.ab.complexity.core.Renderer;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "#Name#SceneEntryDef", menuName = "com.ab/scene/common")]
    public class SceneEntryDef : StaticEntrySOParamDef<SceneEntryDef.Settings>, 
        IStaticInitDef, IStaticUpdateDef, IStaticContextSetDef, IPreInitLoad
    {
        public void RegisterInit()
        {
        }

        public void RegisterUpdate()
        {
            SysReg.Add(new DestroyLinkSystem());
            SysReg.Add(new MovementVelocitySystem());
        }

        public void SetContext()
        {
            var addresable = new AddressableService();

            W.SetResource(addresable);
            W.SetResource(new LocalizationService());
            W.SetResource(new AtlasService(addresable, new[] { "MapAtlas" }));
            W.SetResource(new ItemService(Def.ItemTable, Def.DropTable));
        }

        public UniTask PreInitLoad(CancellationToken ct) =>
            W.GetResource<LocalizationService>().InitializeAsync();

        [Serializable]
        public class Settings
        {
            public ItemTable[] ItemTable;
            public DropTable[] DropTable;
        }
    }
}