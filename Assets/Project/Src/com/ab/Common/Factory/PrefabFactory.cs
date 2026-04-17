using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.ab.common
{
    public class PrefabFactory<TLink> : IPreInitWait
        where TLink : EntityLink, new()
    {
        protected readonly AddressableService _addresable;
        protected readonly Settings _def;

        protected TLink _prefab;

        public PrefabFactory(Settings def)
        {
            _def = def;
            _addresable = W.GetResource<AddressableService>();
            IPreInitWaitRegistry.AddPreInit(this);
        }

        public virtual UniTask PreInitWait(CancellationToken ct)
        {
            return _addresable.LoadAsync<GameObject>(_def.PrefabKey);
        }

        public virtual void BuildLink(TLink link)
        {
        }

        public TLink CreateLink()
        {
            var item = CrateInstance();
            BuildLink(item);

            return item;
        }

        protected virtual TLink CrateInstance()
        {
            return Object.Instantiate(GetPrefab(), _def.SpawnContainer);
        }

        protected virtual TLink GetPrefab()
        {
            if (_prefab != null)
                return _prefab;

            if (!_addresable.TryGet<GameObject>(_def.PrefabKey, out var prefab))
                throw new TypeLoadException($"{nameof(GetType)}::{nameof(CreateLink)}:" +
                                            $"Can't get prefab key: {_def.PrefabKey} " +
                                            $"from {nameof(AddressableService)}");

            if (!prefab.TryGetComponent<TLink>(out var prefabLink))
                throw new TypeLoadException($"{nameof(GetType)}::{nameof(CreateLink)}:" +
                                            $"Can't find type: {nameof(TLink)} with key: {_def.PrefabKey} " +
                                            $"in prefab: {prefabLink.name}");

            _prefab = prefabLink;
            return prefabLink;
        }

        [Serializable]
        public class Settings
        {
            public string PrefabKey;
            public Transform SpawnContainer;
        }
    }
}