using System.Collections.Generic;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace com.ab.common
{
    public class ConfigTableSo<TEntry> : SerializedScriptableObject, IEcsTable
        where TEntry : struct, IComponent
    {
        [SerializeField] [ShowInInspector] public Dictionary<ConfigIDEntSo, TEntry> Entries = new();

        public void OpenEcsSession()
        {
            foreach (var item in Entries)
            {
                var ent = item.Key.Init();
                ent.Set(item.Value);
            }
        }

        public void CloseEcsSession()
        {
            Entries.ForEach(item => item.Key.End());
        }
    }
}