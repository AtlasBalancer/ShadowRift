using System.Runtime.CompilerServices;
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
    public abstract class MouseEnterExitProvider<TWorld> : UnityEventProvider<TWorld>
        where TWorld : struct, IWorldType
    {
        void OnMouseEnter()
        {
            if (!CanSend()) return;
            OnSendEnterEvent();
        }

        void OnMouseExit()
        {
            if (!CanSend()) return;
            OnSendExitEvent();
        }

        [MethodImpl(AggressiveInlining)]
        protected virtual void OnSendEnterEvent()
        {
            World<TWorld>.SendEvent(new MouseEnterEvent
            {
                Ref = gameObject
            });
        }

        [MethodImpl(AggressiveInlining)]
        protected virtual void OnSendExitEvent()
        {
            World<TWorld>.SendEvent(new MouseExitEvent
            {
                Ref = gameObject
            });
        }
    }
}