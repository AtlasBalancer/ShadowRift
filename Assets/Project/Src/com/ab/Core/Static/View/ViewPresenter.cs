using com.ab.domain.craft;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.UI;

namespace com.ab.core
{
    public abstract class ViewPresenter<TView>
        where TView : ViewMono
    {
        protected TView View;

        protected void Init(TView viewPrefab, Transform root, Button activeButton = null)
        {
            View = Object.Instantiate(viewPrefab, root);
            View.gameObject.SetActive(false);
            View.Init();

            if (activeButton != null)
                Bind(activeButton);
            
            Bind(View.HideBtn);
        }
        
        protected void Register(TView viewRef, Button activeButton = null)
        {
            View = viewRef;
            View.gameObject.SetActive(false);
            View.Init();

            if (activeButton != null)
                Bind(activeButton);
            
            if(View.HideBtn != null)
              Bind(View.HideBtn);
        }
        
        protected void Bind(Button activateButton) =>
            activateButton.onClick.AddListener(ViewActive);

        protected void ViewActive()
        {
            bool active = View.Ent.Toggle<ViewActive>();
            View.Active(active);

            if (active)
                Show();
            else
                Hide();
        }

        protected virtual void Show() { }

        protected virtual void Hide() { }

        protected bool IsActive() =>
            View.Ent.Has<ViewActive>();
    }
}