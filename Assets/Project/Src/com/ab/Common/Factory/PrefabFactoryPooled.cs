using System;
using UnityEngine.Pool;

namespace com.ab.common
{
    public class PrefabFactoryPooled<TLink> : PrefabFactory<TLink>
        where TLink : EntityLink, new()
    {
        [Serializable]
        public class Settings : PrefabFactory<TLink>.Settings
        {
            public int MaxSize;
        }

        readonly ObjectPool<TLink> _pool;
        readonly protected Settings _def;

        public PrefabFactoryPooled(Settings def) : base(def)
        {
            _def = def;
            _pool = new ObjectPool<TLink>(
                createFunc: CreateForPool,
                actionOnGet: obj => obj.Reset(),
                actionOnRelease: obj => obj.Cleanup(),
                actionOnDestroy: obj => obj.Dispose(),
                maxSize: _def.MaxSize
            );
        }

        TLink CreateForPool() => 
            UnityEngine.Object.Instantiate(GetPrefab());

        protected override TLink CrateInstance() => 
            _pool.Get();
    }
}