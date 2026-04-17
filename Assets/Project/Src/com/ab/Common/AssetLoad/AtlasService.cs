using System;
using System.Collections.Generic;
using System.Threading;
using com.ab.complexity.core;
using com.ab.domain.item;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.U2D;

namespace com.ab.common
{
    public class AtlasService : IDisposable, IPreInitWait
    {
        readonly AddressableService _addressables;

        readonly Settings _def;

        public AtlasService(Settings def)
        {
            _def = def;
            _addressables = W.GetResource<AddressableService>();
            SpriteAtlasManager.atlasRequested += OnAtlasRequested;

            IPreInitWaitRegistry.AddPreInit(this);
        }

        public void Dispose()
        {
            SpriteAtlasManager.atlasRequested -= OnAtlasRequested;
        }

        public UniTask PreInitWait(CancellationToken ct)
        {
            return InitAsync();
        }

        public UniTask LoadAtlas(string atlasKey)
        {
            return _addressables.LoadAsync<SpriteAtlas>(atlasKey);
        }

        public Sprite GetSprite(string atlas, World<WCT>.Entity ent)
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

        public async UniTask InitAsync()
        {
            var atlases = _def.PrelaodAtals;

            var tasks = new UniTask<SpriteAtlas>[atlases.Count];
            for (var i = 0; i < atlases.Count; i++)
                tasks[i] = _addressables.LoadAsync<SpriteAtlas>(atlases[i]);

            await UniTask.WhenAll(tasks);
        }

        void OnAtlasRequested(string tag, Action<SpriteAtlas> callback)
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

        [Serializable]
        public class Settings
        {
            public List<string> PrelaodAtals;
        }
    }
}