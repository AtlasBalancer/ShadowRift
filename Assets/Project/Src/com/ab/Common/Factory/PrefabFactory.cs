using System;
using System.Threading;
using com.ab.core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace com.ab.common
{
    public class PrefabFactory<TLink> : IPreInitWait
        where TLink : EntityLink, new()
    {
        [Serializable]
        public class Settings
        {
            public string PrefabKey;
            public Transform SpawnContainer;
        }

        readonly protected Settings _def;
        readonly protected AddressableService _addresable;

        public PrefabFactory(Settings def)
        {
            _def = def;
            _addresable = W.GetResource<AddressableService>();
            IPreInitWaitRegistry.AddPreInit(this);
        }

        public virtual void BuildLink(TLink link)
        {
        }

        public virtual UniTask PreInitWait(CancellationToken ct) =>
            _addresable.LoadPrefabAsync<TLink>(_def.PrefabKey);

        public TLink CreateLink()
        {
            var item = CrateInstance();
            BuildLink(item);

            return item;
        }

        protected virtual TLink CrateInstance() => 
            UnityEngine.Object.Instantiate(GetPrefab(), _def.SpawnContainer);

        protected virtual TLink GetPrefab()
        {
            if (!_addresable.TryGet<TLink>(_def.PrefabKey, out var prefab))
            {
                throw new TypeLoadException($"{nameof(this.GetType)}::{nameof(CreateLink)}:" +
                                            $"Can't create prefab: {_def.PrefabKey}. " +
                                            $"Can't get from {nameof(AddressableService)}");
            }

            return prefab;
        }
    }
}