using UnityEngine;

namespace FFS.Libraries.StaticEcs.Unity.Editor
{
    internal class ComponentDrawerWrapper : ScriptableObject
    {
        static ComponentDrawerWrapper _instance;
        [SerializeReference] public IComponent value;

        public static ComponentDrawerWrapper Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = CreateInstance<ComponentDrawerWrapper>();
                    _instance.hideFlags = HideFlags.DontSave;
                }

                return _instance;
            }
        }
    }

    internal class EventDrawerWrapper : ScriptableObject
    {
        static EventDrawerWrapper _instance;
        [SerializeReference] public IEvent value;

        public static EventDrawerWrapper Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = CreateInstance<EventDrawerWrapper>();
                    _instance.hideFlags = HideFlags.DontSave;
                }

                return _instance;
            }
        }
    }

    internal class ContextDrawerWrapper : ScriptableObject
    {
        static ContextDrawerWrapper _instance;
        [SerializeReference] public object value;

        public static ContextDrawerWrapper Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = CreateInstance<ContextDrawerWrapper>();
                    _instance.hideFlags = HideFlags.DontSave;
                }

                return _instance;
            }
        }
    }

    internal class ContextValueDrawerWrapper<T> : ScriptableObject
    {
        public T value;
    }
}