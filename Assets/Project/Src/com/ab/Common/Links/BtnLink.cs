using DG.Tweening;
using FFS.Libraries.StaticEcs;
using UnityEngine.UI;

namespace com.ab.common
{
    public readonly struct BtnRef : IComponent
    {
        public readonly BtnLink Val;

        public BtnRef(BtnLink val)
        {
            Val = val;
        }
    }

    public class BtnLink : EntityLink
    {
        public Button Btn;

        protected override void Register()
        {
            Ent.Set(new BtnRef(this));
        }

        protected override void Subscribe()
        {
            Btn.onClick.AddListener(Press);
        }

        protected override void UnSubscribe()
        {
            Btn.onClick.RemoveListener(Press);
        }

        public void Press()
        {
            if (!Btn.interactable)
                return;

            OnClick();

            Btn.interactable = false;
            Btn.transform.DOScale(0.95f, 0.1f)
                .OnComplete(() =>
                {
                    Btn.transform.DOScale(1f, 0.1f);
                    Btn.interactable = true;
                });
        }
    }
}