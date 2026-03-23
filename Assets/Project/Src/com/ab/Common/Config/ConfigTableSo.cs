using System.Collections.Generic;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.common
{
    public class ConfigTableSo<TEntry> : SerializedScriptableObject, IEntConfig, IStaticRegisterTypeDef
        where TEntry : struct, IComponent
    {
        [SerializeField, ShowInInspector] public Dictionary<ConfigIDEntSo, TEntry> Entries = new();

        public void Init()
        {
            foreach (var item in Entries)
            {
                var ent = item.Key.Init();
                ent.Add(item.Value);
            }
        }

        public void RegisterType()
        {
            if (!WC.Components<TEntry>.Value.IsRegistered())
                WC.RegisterComponentType<TEntry>();
        }
    }
}