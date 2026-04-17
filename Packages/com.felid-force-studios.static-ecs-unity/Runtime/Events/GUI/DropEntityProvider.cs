using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public abstract class DropEntityProvider<TWorld> : GUIEntityEventProvider<TWorld>, IDropHandler
        where TWorld : struct, IWorldType
    {
        public void OnDrop(PointerEventData data)
        {
            if (!CanSend()) return;
            if (SendEvents) OnSendEvent(data);
        }

        [MethodImpl(AggressiveInlining)]
        protected virtual void OnSendEvent(PointerEventData data)
        {
            World<TWorld>.SendEvent(new DropEntityEvent
            {
                Ref = gameObject,
                EntityGID = EntityGID,
                Button = data.button
            });
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, Const.IL2CPPNullChecks)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, Const.IL2CPPArrayBoundsChecks)]
#endif
    public abstract class DropEntityGIDProvider<TWorld> : DropEntityProvider<TWorld>
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
    public abstract class DropEntityRefProvider<TWorld, TProvider> : DropEntityProvider<TWorld>
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