using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace com.ab.common
{
    [CreateAssetMenu(fileName = "ItemID#Name#", menuName = "com.ab/items/id")]
    public class ItemID : SerializedScriptableObject
    {
        [SerializeField, ReadOnly] 
        private string id;
        public string Id => id;

#if UNITY_EDITOR
        void OnValidate()
        {
            EnsureId();
        }

        [Button]
        void RegenerateId()
        {
            id = GenerateUniqueId();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        void EnsureId()
        {
            if (!string.IsNullOrWhiteSpace(id) && !HasDuplicateId(id, this))
                return;

            id = GenerateUniqueId();
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

        static bool HasDuplicateId(string candidateId, ItemID self)
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(ItemID)}");

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var item = AssetDatabase.LoadAssetAtPath<ItemID>(path);

                if (item == null || item == self)
                    continue;

                if (item.id == candidateId)
                    return true;
            }

            return false;
        }

        [InfoBox("ID is empty", InfoMessageType.Error, nameof(IsIdEmpty))]
        [InfoBox("Duplicate ID detected", InfoMessageType.Error, nameof(HasDuplicate))]
        [ShowInInspector, ReadOnly]
        string DebugId => id;

        bool IsIdEmpty => string.IsNullOrWhiteSpace(id);
        bool HasDuplicate => !string.IsNullOrWhiteSpace(id) && HasDuplicateId(id, this);
#endif
    }
}