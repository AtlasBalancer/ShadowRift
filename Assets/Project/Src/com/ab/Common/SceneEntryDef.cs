using UnityEngine;
using com.ab.complexity.core;
using com.ab.complexity.features.player;
using com.ab.core;
using Project.Src.com.ab.Domain.Inventory;
using Renderer = com.ab.complexity.core.Renderer;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "#Name#SceneEntryDef", menuName = "com.ab/scene")]
    public class SceneEntryDef : ScriptableObject,
        IStaticRegisterTypeDef, IStaticInitDef, IStaticUpdateDef, IStaticCreateEntityDef
    {
        public void RegisterType()
        {
            W.RegisterTagType<ViewActive>();
            W.RegisterTagType<ViewPressed>();
            W.RegisterTagType<Delete>();
            
            W.RegisterComponentType<LogicRender>();
            W.RegisterComponentType<Position>();
            W.RegisterComponentType<Direction>();
            W.RegisterComponentType<Velocity>();
            W.RegisterComponentType<Renderer>();
            W.RegisterComponentType<AnimatorRef>();
        }

        public void RegisterInit()
        {
            
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new MovementVelocitySystem());
        }

        public void CreateEntities()
        {
            // var entity = W.Entity.New(
            //     new Velocity(),
            //     new Position { Value = Vector3.zero },
            //     new Direction { Value = Vector3.one }
            // );
        }
    }
}