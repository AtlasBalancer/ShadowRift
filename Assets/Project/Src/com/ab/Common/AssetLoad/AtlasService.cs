using System;
using System.Collections.Generic;
using System.Threading;
using com.ab.complexity.core;
using com.ab.domain.item;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace com.ab.common
{
    public class AtlasService : IDisposable, IPreInitLoad
    {
        readonly AddressableService _addressables;
        readonly IReadOnlyList<string> _atlasKeys;

        public AtlasService(AddressableService addressables, IReadOnlyList<string> atlasKeys)
        {
            _addressables = addressables;
            _atlasKeys = atlasKeys;
            SpriteAtlasManager.atlasRequested += OnAtlasRequested;
        }

        public UniTask LoadAtlas(string atlasKey) =>
            _addressables.LoadAsync<SpriteAtlas>(atlasKey);

        public Sprite GetSprite(string atlas, W.Entity ent)
        {
            var spriteKey = ent.Ref<ItemEntry>().AKSprite;
            return GetSprite(atlas, spriteKey);
        }

        public Sprite GetSprite(string atlasKey, string spriteName)
        {
            if (_addressables.TryGet<SpriteAtlas>(atlasKey, out var atlas))
                return atlas.GetSprite(spriteName);
            return null;
        }

        public UniTask PreInitLoad(CancellationToken ct) =>
            InitAsync();

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