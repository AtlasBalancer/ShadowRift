using System.Collections.Generic;
using System.Linq;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.common
{
    public class EntIDTableSo<TEntry> : SerializedScriptableObject, IEntTable, IStaticRegisterTypeDef
        where TEntry : struct, IComponent
    {
        [SerializeField, ShowInInspector] public Dictionary<IDEntSo, TEntry> Entries = new();

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
            if (!W.Components<TEntry>.Value.IsRegistered())
                W.RegisterComponentType<TEntry>();
        }
    }
}