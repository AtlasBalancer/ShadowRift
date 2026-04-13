using UnityEngine;

namespace com.ab.core
{
    public static class ComponentExtensions
    {
        public static void Active(this Component source, bool active = true) => 
            source.gameObject.SetActive(active);
    }
}