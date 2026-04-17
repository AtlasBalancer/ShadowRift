using FFS.Libraries.StaticEcs.Unity.Editor;
using UnityEditor;

namespace com.ab.common.Editor
{
    [CustomEditor(typeof(WTEntityProvider))]
    [CanEditMultipleObjects]
    public class WTEntityProviderEditor : StaticEcsEntityProviderEditor<WT, WTEntityProvider>
    {
    }
}