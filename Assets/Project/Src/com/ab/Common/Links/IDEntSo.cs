using System;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using com.ab.complexity.core;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "ID#Name#", menuName = "com.ab/id")]
    public class IDEntSo : SerializedScriptableObject
    {
        [SerializeField, ReadOnly] string _id;
        public string ID => _id;

        [field: System.NonSerialized] public W.Entity RuntimeID { get; private set; }
        [field: System.NonSerialized] bool _inited { get; set; }

        public W.Entity Init()
        {
            if (_inited)
                return RuntimeID;

            _inited = true;
            RuntimeID = W.Entity.New();
            RuntimeID.Add(new IDRef(this));
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

        static bool HasDuplicateId(string candidateId, IDEntSo self)
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(common.IDEntSo)}");

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var item = AssetDatabase.LoadAssetAtPath<IDEntSo>(path);

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