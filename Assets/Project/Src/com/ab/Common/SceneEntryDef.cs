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
        IStaticRegisterTypeDef, IStaticInitDef, IStaticUpdateDef, IStaticContextSetDef, IPreInitLoad
    {
        public void RegisterType()
        {
            W.RegisterTagType<ViewActive>();
            W.RegisterTagType<Click>();
            W.RegisterTagType<ActiveTag>();
            W.RegisterTagType<AvailableTag>();
            W.RegisterTagType<TriggerEnterTag>();

            W.RegisterComponentType<ConfigRef>();
            W.RegisterComponentType<Timer>();

            W.RegisterComponentType<EntRef>();
            W.RegisterComponentType<Destroy>();

            W.RegisterComponentType<LogicRender>();
            W.RegisterComponentType<Position>();
            W.RegisterComponentType<Direction>();
            W.RegisterComponentType<Velocity>();
            W.RegisterComponentType<Renderer>();
            W.RegisterComponentType<AnimatorRef>();

            W.RegisterComponentType<Amount>();
            W.RegisterComponentType<AmountUpdate>();
            W.RegisterComponentType<ProgressBarRef>();

            W.RegisterOneToManyRelationType<Parent, Childs>(defaultComponentCapacity: 4);
        }

        public void RegisterInit()
        {
            
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new DestroyLinkSystem());
            SysReg.AddUpdate(new MovementVelocitySystem());
        }

        public void SetContext()
        {
            var addresable = new AddressableService();

            W.Context<AddressableService>.Set(addresable);
            W.Context<LocalizationService>.Set(new LocalizationService());
            W.Context<AtlasService>.Set(new AtlasService(addresable, new[] {"MapAtlas"}));
            W.Context<ItemService>.Set(new ItemService(Def.ItemTable, Def.DropTable));
        }

        public UniTask PreInitLoad(CancellationToken ct) => 
            W.Context<LocalizationService>.Get().InitializeAsync();

        [Serializable]
        public class Settings
        {
            public ItemTable[] ItemTable;
            public DropTable[] DropTable;
        }
    }
}