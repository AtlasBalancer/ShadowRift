using System;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "ID#Name#", menuName = "com.ab/id")]
    public class ConfigIDEntSo : SerializedScriptableObject
    {
        [SerializeField] [ReadOnly] string _id;
        public string ID => _id;

        [field: NonSerialized] public World<WCT>.Entity RuntimeID { get; private set; }
        [field: NonSerialized] bool _inited { get; set; }

        [field: NonSerialized] public EntityGID Gid => RuntimeID.GID;

        public World<WCT>.Entity Init()
        {
            if (_inited)
                return RuntimeID;

            _inited = true;
            RuntimeID = WC.NewEntity<Default>();
            RuntimeID.Set(new ConfigRef(RuntimeID, ID));
            return RuntimeID;
        }

        public void End()
        {
            _inited = false;
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            EnsureId();
        }

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
            var guids = AssetDatabase.FindAssets($"t:{nameof(ConfigIDEntSo)}");

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
        [ShowInInspector]
        [ReadOnly]
        string DebugID =>
            _id;

        bool IsIdEmpty => string.IsNullOrWhiteSpace(_id);
        bool HasDuplicate => !string.IsNullOrWhiteSpace(_id) && HasDuplicateId(_id, this);
#endif
    }
}