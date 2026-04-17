using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Src.com.ab.Common.Unity
{
    [Serializable]
    public class TransformByString : SerializableMap<string, Transform>
    {
    }

    [Serializable]
    public class GameObjectByString : SerializableMap<string, GameObject>
    {
    }

    [Serializable]
    public abstract class SerializableMap<TKey, TValue> : ISerializationCallbackReceiver
    {
        [TableList(ShowIndexLabels = false)] [SerializeField]
        List<Entry> _entries = new();

        protected Dictionary<TKey, TValue> _map { get; private set; }

        public virtual void OnAfterDeserialize()
        {
            _map = new Dictionary<TKey, TValue>(_entries.Count);
            foreach (var e in _entries)
                if (e.Key != null)
                    _map[e.Key] = e.Value;
        }

        public virtual void OnBeforeSerialize()
        {
        }

        public TValue Get(TKey key)
        {
            return _map[key];
        }

        public bool TryGet(TKey key, out TValue value)
        {
            return _map.TryGetValue(key, out value);
        }

        [Serializable]
        public struct Entry
        {
            public TKey Key;
            public TValue Value;
        }
    }
}