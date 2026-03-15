using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace com.ab.common
{
    public class AddressableService
    {
        private readonly Dictionary<string, AsyncOperationHandle> _handles = new();

        public async UniTask<T> LoadAsync<T>(string key) where T : class
        {
            Debug.Log($"{nameof(AddressableService)}:: load: {key}");
            
            if (_handles.TryGetValue(key, out var cached))
            {
                if (!cached.IsDone)
                    await cached.ToUniTask();

                if (cached.Status != AsyncOperationStatus.Succeeded)
                    throw new Exception($"{nameof(AddressableService)}:: Failed to load '{key}'");

                return (T)cached.Result;
            }

            var handle = Addressables.LoadAssetAsync<T>(key);
            _handles[key] = handle;

            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                _handles.Remove(key);
                throw new Exception($"{nameof(AddressableService)}:: Failed to load '{key}'");
            }

            return handle.Result;
        }

        public bool TryGet<T>(string key, out T asset) where T : class
        {
            if (_handles.TryGetValue(key, out var handle)
                && handle.IsDone
                && handle.Status == AsyncOperationStatus.Succeeded)
            {
                asset = (T)handle.Result;
                return true;
            }

            asset = default;
            return false;
        }

        public void Release(string key)
        {
            if (!_handles.Remove(key, out var handle)) return;
            Addressables.Release(handle);
        }

        public void ReleaseAll()
        {
            foreach (var handle in _handles.Values)
                Addressables.Release(handle);

            _handles.Clear();
        }
    }
}
