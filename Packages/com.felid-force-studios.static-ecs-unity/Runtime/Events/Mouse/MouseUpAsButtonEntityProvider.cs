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
    public abstract class MouseUpAsButtonEntityProvider<TWorld> : UnityEntityEventProvider<TWorld>
        where TWorld : struct, IWorldType
    {
        void OnMouseUpAsButton()
        {
            if (!CanSend()) return;
            OnMouseUpAsButtonEvent();
        }

        [MethodImpl(AggressiveInlining)]
        protected virtual void OnMouseUpAsButtonEvent()
        {
            World<TWorld>.SendEvent(new MouseUpAsButtonEntityEvent
            {
                Ref = gameObject,
                EntityGID = EntityGID
            });
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, Const.IL2CPPNullChecks)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, Const.IL2CPPArrayBoundsChecks)]
#endif
    public abstract class MouseUpAsButtonEntityGIDProvider<TWorld> : MouseUpAsButtonEntityProvider<TWorld>
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
    public abstract class MouseUpAsButtonEntityRefProvider<TWorld, TProvider> : MouseUpAsButtonEntityProvider<TWorld>
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