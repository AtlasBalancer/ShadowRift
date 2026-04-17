using FFS.Libraries.StaticEcs.Unity.Editor;
using UnityEditor;

namespace com.ab.common.Editor
{
    [CustomEditor(typeof(WTEventProvider))]
    [CanEditMultipleObjects]
    public class WTEventProviderEditor : StaticEcsEvenTEntityProviderEditor<WT, WTEventProvider>
    {
    }
}