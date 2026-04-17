#if FFS_ECS_PHYSICS2D
using System.Runtime.CompilerServices;
using UnityEngine;
using static System.Runtime.CompilerServices.MethodImplOptions;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace FFS.Libraries.StaticEcs.Unity
{
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, Const.IL2CPPNullChecks)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, Const.IL2CPPArrayBoundsChecks)]
#endif
    public abstract class Trigger2DEntityProvider<TWorld> : UnityEntityEventProvider<TWorld>
        where TWorld : struct, IWorldType
    {
        void OnTriggerEnter2D(Collider2D data)
        {
            if (!CanSend()) return;
            if (SendEvents) OnSendEnterEvent(data);
            if (ManageComponents) OnAddComponent(data);
        }

        void OnTriggerExit2D(Collider2D data)
        {
            if (!CanSend()) return;
            if (SendEvents) OnSendExitEvent(data);
            if (ManageComponents) OnRemoveComponent();
        }

        [MethodImpl(AggressiveInlining)]
        protected virtual void OnSendEnterEvent(Collider2D data)
        {
            World<TWorld>.SendEvent(new TriggerEnter2DEntityEvent
            {
                Ref = gameObject,
                EntityGID = EntityGID,
                Collider = data
            });
        }

        [MethodImpl(AggressiveInlining)]
        protected virtual void OnSendExitEvent(Collider2D data)
        {
            World<TWorld>.SendEvent(new TriggerExit2DEntityEvent
            {
                Ref = gameObject,
                EntityGID = EntityGID,
                Collider = data
            });
        }

        [MethodImpl(AggressiveInlining)]
        protected virtual void OnAddComponent(Collider2D data)
        {
            SetComponentOnEntity(new Trigger2DState
            {
                Collider = data
            });
        }

        [MethodImpl(AggressiveInlining)]
        protected virtual void OnRemoveComponent()
        {
            DeleteComponentFromEntity<Trigger2DState>();
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, Const.IL2CPPNullChecks)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, Const.IL2CPPArrayBoundsChecks)]
#endif
    public abstract class Trigger2DEntityGIDProvider<TWorld> : Trigger2DEntityProvider<TWorld>
        where TWorld : struct, IWorldType
    {
        [SerializeField] EntityGID entityGid;

        protected override EntityGID EntityGID
        {
            [MethodImpl(AggressiveInlining)] get => entityGid;
        }

        [MethodImpl(AggressiveInlining)]
        public void SetEntityGID(EntityGID gid)
        {
            entityGid = gid;
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, Const.IL2CPPNullChecks)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, Const.IL2CPPArrayBoundsChecks)]
#endif
    public abstract class Trigger2DEntityRefProvider<TWorld, TProvider> : Trigger2DEntityProvider<TWorld>
        where TWorld : struct, IWorldType
        where TProvider : StaticEcsEntityProvider<TWorld>
    {
        [SerializeField] TProvider entityProvider;

        protected override EntityGID EntityGID
        {
            [MethodImpl(AggressiveInlining)] get => entityProvider != null ? entityProvider.EntityGid : default;
        }

#if UNITY_EDITOR
        protected void Reset()
        {
            if (entityProvider == null) entityProvider = GetComponent<TProvider>();
        }
#endif

        [MethodImpl(AggressiveInlining)]
        public void SetEntityProvider(TProvider provider)
        {
            entityProvider = provider;
        }
    }
}
#endif