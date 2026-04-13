using com.ab.common;
using UnityEngine.UI;

namespace com.ab.core
{
    public abstract class ViewMono : EntityLink
    {
        public Button HideBtn;

        public void Active(bool active) =>
            gameObject.SetActive(active);
    }
}