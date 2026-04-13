using System;
using System.Collections.Generic;
using System.Threading;
using com.ab.complexity.core;
using com.ab.core;
using com.ab.domain.item;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace com.ab.common
{
    public class AtlasService : IDisposable, IPreInitWait
    {
        [Serializable]
        public class Settings
        {
            public List<string> PrelaodAtals;
        }

        readonly Settings _def;
        readonly AddressableService _addressables;

        public AtlasService(Settings def)
        {
            _def = def;
            _addressables = W.GetResource<AddressableService>();
            SpriteAtlasManager.atlasRequested += OnAtlasRequested;
            
            IPreInitWaitRegistry.AddPreInit(this);
        }

        public UniTask LoadAtlas(string atlasKey) =>
            _addressables.LoadAsync<SpriteAtlas>(atlasKey);

        public Sprite GetSprite(string atlas, WC.Entity ent)
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

        public UniTask PreInitWait(CancellationToken ct) =>
            InitAsync();

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

        public void Dispose()
        {
            SpriteAtlasManager.atlasRequested -= OnAtlasRequested;
        }
    }
}