using UnityEngine;
using com.ab.complexity.core;
using com.ab.complexity.features.player;
using com.ab.core;
using Project.Src.com.ab.Domain.Inventory;
using Renderer = com.ab.complexity.core.Renderer;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "#Name#SceneEntryDef", menuName = "com.ab/scene/common")]
    public class SceneEntryDef : ScriptableObject,
        IStaticRegisterTypeDef, IStaticInitDef, IStaticUpdateDef, IStaticContextSetDef
    {
        public void RegisterType()
        {
            W.RegisterTagType<ViewActive>();
            W.RegisterTagType<ViewPressed>();

            W.RegisterComponentType<Timer>();

            W.RegisterComponentType<IDRef>();
            W.RegisterComponentType<EntRef>();
            W.RegisterComponentType<LinkRef>();
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
            W.Context<AddressableService>.Set(new AddressableService());
            W.Context<LocalizationService>.Set(new LocalizationService());
        }
    }
    
}