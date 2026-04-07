using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Src.com.ab.Common.Unity
{
    [Serializable] public class TransformByString : SerializableMap<string, Transform> { }
    [Serializable] public class GameObjectByString : SerializableMap<string, GameObject> { }   
    
    [Serializable]
    public abstract class SerializableMap<TKey, TValue> : ISerializationCallbackReceiver
    {
        [Serializable]
        public struct Entry
        {
            public TKey Key;
            public TValue Value;
        }

        [TableList(ShowIndexLabels = false)] [SerializeField]
        private List<Entry> _entries = new();

        protected Dictionary<TKey, TValue> _map { get; private set; }

        public TValue Get(TKey key) => _map[key];
        public bool TryGet(TKey key, out TValue value) => _map.TryGetValue(key, out value);

        public virtual void OnAfterDeserialize()
        {
            _map = new(_entries.Count);
            foreach (var e in _entries)
                if (e.Key != null)
                    _map[e.Key] = e.Value;
        }

        public virtual void OnBeforeSerialize() { }
    }
}