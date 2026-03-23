using System;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "ID#Name#", menuName = "com.ab/id")]
    public class ConfigIDEntSo : SerializedScriptableObject
    {
        [SerializeField, ReadOnly] string _id;
        public string ID => _id;

        [field: System.NonSerialized] public WC.Entity RuntimeID { get; private set; }
        [field: System.NonSerialized] bool _inited { get; set; }

        [field: System.NonSerialized] public EntityGID Gid => RuntimeID.Gid();
        
        public WC.Entity Init()
        {
            if (_inited)
                return RuntimeID;

            _inited = true;
            RuntimeID = WC.Entity.New();
            RuntimeID.Add(new ConfigRef(RuntimeID));
            return RuntimeID;
        }

        
#if UNITY_EDITOR
        void OnValidate() => EnsureId();

        [Button]
        void RegenerateId()
        {
            _id = GenerateUniqueId();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        void EnsureId()
        {
            if (!string.IsNullOrWhiteSpace(_id) && !HasDuplicateId(_id, this))
                return;

            _id = GenerateUniqueId();
            EditorUtility.SetDirty(this);
        }

        static string GenerateUniqueId()
        {
            string newId;

            do
            {
                newId = Guid.NewGuid().ToString("N");
            } while (HasDuplicateId(newId, null));

            return newId;
        }

        static bool HasDuplicateId(string candidateId, ConfigIDEntSo self)
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(common.ConfigIDEntSo)}");

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var item = AssetDatabase.LoadAssetAtPath<ConfigIDEntSo>(path);

                if (item == null || item == self)
                    continue;

                if (item._id == candidateId)
                    return true;
            }

            return false;
        }

        [InfoBox("ID is empty", InfoMessageType.Error, nameof(IsIdEmpty))]
        [InfoBox("Duplicate ID detected", InfoMessageType.Error, nameof(HasDuplicate))]
        [ShowInInspector, Sirenix.OdinInspector.ReadOnly]
        string DebugID =>
            _id;

        bool IsIdEmpty => string.IsNullOrWhiteSpace(_id);
        bool HasDuplicate => !string.IsNullOrWhiteSpace(_id) && HasDuplicateId(_id, this);
#endif
    }
}