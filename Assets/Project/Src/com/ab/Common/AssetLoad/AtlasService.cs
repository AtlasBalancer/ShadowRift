using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace com.ab.common
{
    public class AtlasService : IDisposable
    {
        private readonly AddressableService _addressables;
        private readonly IReadOnlyList<string> _atlasKeys;

        public AtlasService(AddressableService addressables, IReadOnlyList<string> atlasKeys)
        {
            _addressables = addressables;
            _atlasKeys = atlasKeys;
            SpriteAtlasManager.atlasRequested += OnAtlasRequested;
        }

        // Вызывай на загрузке сцены — загружает все атласы параллельно
        public async UniTask InitAsync()
        {
            var tasks = new UniTask<SpriteAtlas>[_atlasKeys.Count];
            for (var i = 0; i < _atlasKeys.Count; i++)
                tasks[i] = _addressables.LoadAsync<SpriteAtlas>(_atlasKeys[i]);

            await UniTask.WhenAll(tasks);
        }

        // tag — это SpriteAtlas.tag из инспектора, не Addressable-ключ.
        // Убедись что они совпадают, либо добавь маппинг.
        private void OnAtlasRequested(string tag, Action<SpriteAtlas> callback)
        {
            if (_addressables.TryGet<SpriteAtlas>(tag, out var cached))
            {
                callback(cached);
                return;
            }

            _addressables.LoadAsync<SpriteAtlas>(tag)
                .ContinueWith(callback)
                .Forget();
        }

        public void Dispose()
        {
            SpriteAtlasManager.atlasRequested -= OnAtlasRequested;
        }
    }
}
