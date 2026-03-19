using System.Threading;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.complexity.features.player;
using com.ab.core;
using Cysharp.Threading.Tasks;
using Project.Src.com.ab.Domain.Inventory;
using Renderer = com.ab.complexity.core.Renderer;
using Timer = com.ab.complexity.core.Timer;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "#Name#SceneEntryDef", menuName = "com.ab/scene/common")]
    public class SceneEntryDef : ScriptableObject,
        IStaticRegisterTypeDef, IStaticInitDef, IStaticUpdateDef, IStaticContextSetDef, IPastInitLoad
    {
        public void RegisterType()
        {
            W.RegisterTagType<ViewActive>();
            W.RegisterTagType<ViewPressed>();

            W.RegisterComponentType<Timer>();

            W.RegisterComponentType<IDRef>();
            W.RegisterComponentType<EntRef>();
            W.RegisterComponentType<Destroy>();

            W.RegisterComponentType<LogicRender>();
            W.RegisterComponentType<Position>();
            W.RegisterComponentType<Direction>();
            W.RegisterComponentType<Velocity>();
            W.RegisterComponentType<Renderer>();
            W.RegisterComponentType<AnimatorRef>();

            W.RegisterComponentType<Amount>();

            W.RegisterOneToManyRelationType<Parent, Childs>(defaultComponentCapacity: 4);
        }

        public void RegisterInit()
        {
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new MovementVelocitySystem());
            SysReg.AddUpdate(new DestroyLinkSystem());
        }

        public void SetContext()
        {
            var addresable = new AddressableService();

            W.Context<AddressableService>.Set(addresable);
            W.Context<LocalizationService>.Set(new LocalizationService());
            W.Context<AtlasService>.Set(new AtlasService(addresable, new[] {"MapAtlas"}));
        }

        public UniTask PastInitLoad(CancellationToken ct) => 
            W.Context<LocalizationService>.Get().InitializeAsync();
    }
}