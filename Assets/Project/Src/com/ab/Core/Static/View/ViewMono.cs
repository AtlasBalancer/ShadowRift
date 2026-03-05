using com.ab.core;
using UnityEngine.UI;

namespace com.ab.domain.craft
{
    public abstract class ViewMono : EntityRef
    {
        public Button HideBtn;

        public virtual void Init() { }
        
        public void Active(bool active) =>
            gameObject.SetActive(active);
    }
}