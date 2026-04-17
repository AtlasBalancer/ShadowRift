using System;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace com.ab.common
{
    public class PrefabFactoryPooled<TLink> : PrefabFactory<TLink>
        where TLink : EntityLink, new()
    {
        protected readonly Settings _def;

        readonly ObjectPool<TLink> _pool;

        public PrefabFactoryPooled(Settings def) : base(def)
        {
            _def = def;
            _pool = new ObjectPool<TLink>(
                CreateForPool,
                obj => obj.Reset(),
                obj => obj.Cleanup(),
                obj => obj.Dispose(),
                maxSize: _def.MaxSize
            );
        }

        TLink CreateForPool()
        {
            return Object.Instantiate(GetPrefab());
        }

        protected override TLink CrateInstance()
        {
            return _pool.Get();
        }

        [Serializable]
        public class Settings : PrefabFactory<TLink>.Settings
        {
            public int MaxSize;
        }
    }
}