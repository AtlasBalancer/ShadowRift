using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
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
    public abstract class GUIEventProvider<TWorld> : UnityEventProvider<TWorld>
        where TWorld : struct, IWorldType
    {
        [SerializeField] Selectable selectable;

        [MethodImpl(AggressiveInlining)]
        protected override bool CanSend()
        {
            return base.CanSend() && (!selectable || selectable.interactable);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, Const.IL2CPPNullChecks)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, Const.IL2CPPArrayBoundsChecks)]
#endif
    public abstract class GUIEntityEventProvider<TWorld> : UnityEntityEventProvider<TWorld>
        where TWorld : struct, IWorldType
    {
        [SerializeField] Selectable selectable;

        [MethodImpl(AggressiveInlining)]
        protected override bool CanSend()
        {
            return base.CanSend() && (!selectable || selectable.interactable);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, Const.IL2CPPNullChecks)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, Const.IL2CPPArrayBoundsChecks)]
#endif
    public abstract class GUIEntityGIDEventProvider<TWorld> : GUIEntityEventProvider<TWorld>
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
    public abstract class GUIEntityRefEventProvider<TWorld, TProvider> : GUIEntityEventProvider<TWorld>
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