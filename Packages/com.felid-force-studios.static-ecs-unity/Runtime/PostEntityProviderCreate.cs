using System.Collections.Generic;
using UnityEngine;

namespace FFS.Libraries.StaticEcs.Unity
{
    [DefaultExecutionOrder(65536)]
    public class PostEntityProviderCreate : MonoBehaviour
    {
        static PostEntityProviderCreate _instance;
        readonly List<AbstractStaticEcsEntityProvider> _pending = new();
        bool _started;

        public static PostEntityProviderCreate Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[StaticEcs] PostEntityProviderCreate");
                    go.hideFlags = HideFlags.HideInHierarchy;
                    _instance = go.AddComponent<PostEntityProviderCreate>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        void Start()
        {
            _started = true;

            for (var i = 0; i < _pending.Count; i++) _pending[i].ResolveLinks();

            for (var i = 0; i < _pending.Count; i++) _pending[i].InvokeOnCreate();

            _pending.Clear();
        }

        void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }

        public void Register(AbstractStaticEcsEntityProvider provider)
        {
            if (_started)
            {
                provider.ResolveLinks();
                provider.InvokeOnCreate();
                return;
            }

            _pending.Add(provider);
        }
    }
}