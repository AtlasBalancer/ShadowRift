using UnityEngine;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace FFS.Libraries.StaticEcs.Unity
{
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [DefaultExecutionOrder(short.MinValue)]
    public abstract class StaticEcsNamedContextReference<TWorld> : MonoBehaviour where TWorld : struct, IWorldType
    {
        [SerializeField] string _key;

        [SerializeField] RegistrationType _registrationType = RegistrationType.OnAwake;

        void Awake()
        {
            if (_registrationType == RegistrationType.OnAwake) World<TWorld>.SetResource(_key, gameObject);
        }

        void OnEnable()
        {
            if (_registrationType == RegistrationType.OnEnable) World<TWorld>.SetResource(_key, gameObject);
        }

        void OnDisable()
        {
            if (_registrationType == RegistrationType.OnEnable) World<TWorld>.RemoveResource(_key);
        }

        void OnDestroy()
        {
            if (_registrationType == RegistrationType.OnAwake) World<TWorld>.RemoveResource(_key);
        }

        public string Key()
        {
            return _key;
        }

        enum RegistrationType
        {
            OnAwake,
            OnEnable
        }
    }
}