using UnityEngine.UI;

namespace com.ab.common
{
    public abstract class ViewMono : EntityLink
    {
        public Button HideBtn;

        public void Active(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}