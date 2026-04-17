using System;
using System.IO;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.ab.common.Persistent
{
    public enum PersistentProfile
    {
        Dev = 10,
        Test = 20,
        Release = 30
    }

    public class PersistentEntryDef : StaticEntryParamDef<PersistentEntryDef.Settings>,
        IStaticContextSetDef
    {
        public void SetContext()
        {
            Def.PersistentService.ActiveScene = SceneManager.GetActiveScene().name;
            W.SetResource(new PersistentService(Def.PersistentService));
        }

        [Serializable]
        public class Settings
        {
            public PersistentService.Settings PersistentService;
        }
    }

    public class PersistentService
    {
        const string SEPRATOR = "_";

        readonly Settings _def;
        readonly StringBuilder _sb = new();

        public PersistentService(Settings def)
        {
            _def = def;

            Debug.Log(Application.persistentDataPath);
        }

        public void Save(string key, byte[] data, bool sceneDependency = false)
        {
            GetKey(key, sceneDependency);
            var path = Path.Combine(Application.persistentDataPath, _sb.ToString());
            File.WriteAllBytes(path, data);

            if (_def.Debug)
                Debug.Log($"{nameof(PersistentService)}::{nameof(Save)}:: With key: {key} " +
                          $"to path: {path}");
        }

        public byte[] Load(string key, bool sceneDependency = false)
        {
            GetKey(key, sceneDependency);
            var path = Path.Combine(Application.persistentDataPath, _sb.ToString());

            if (_def.Debug)
                Debug.Log($"{nameof(PersistentService)}::{nameof(Load)}:: With key: {key} " +
                          $"to path: {path}");

            return File.ReadAllBytes(path);
        }

        void GetKey(string key, bool sceneDependency)
        {
            _sb.Clear();
            UpdateAfix(_sb, sceneDependency);
            _sb.Append(key);
        }

        void UpdateAfix(StringBuilder sb, bool sceneDependency)
        {
            _sb.Append(_def.Profile);
            _sb.Append(SEPRATOR);
            _sb.Append(_def.PersistentVersion);

            if (sceneDependency)
            {
                _sb.Append(SEPRATOR);
                _sb.Append(_def.ActiveScene);
            }

            _sb.Append(SEPRATOR);
        }

        [Serializable]
        public class Settings
        {
            public bool Debug;
            [ReadOnly] public string ActiveScene;
            public string PersistentVersion;
            public PersistentProfile Profile;
        }
    }
}