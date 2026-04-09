using System;
using UnityEngine;
using UnityEngine.UI;
using com.ab.complexity.core;

namespace com.ab.common
{
    public class ResponseButtonMono : EntityLink
    {
        public Button Btn;

        public Action<Button> WaitResponse;
        public Action<Button> Complete;

        protected override void Subscribe()
        {
            Btn.transition = Selectable.Transition.None;
            Btn.onClick.AddListener(OnClick);

            Ent.Set(new ResponseButtonRef(this));
        }

        protected override void UnSubscribe()
        {
            Btn.onClick.RemoveListener(OnClick);
        }

        public void Attach(W.Entity ent)
        {
            if (Ent.Has<AttachRef>())
                Ent.Ref<AttachRef>().Update(ent);
            else
                Ent.Set(new AttachRef(ent));
        }

        public void OnClick()
        {
            Debug.Log($"{nameof(ResponseButtonMono)}:: Click");

            Ent.Apply<ResponseClick>(true);
            Btn.interactable = false;
            WaitResponse?.Invoke(Btn);
        }

        public void OnComplete()
        {
            Debug.Log($"{nameof(ResponseButtonMono)}:: Complete");
            
            Btn.interactable = true;
            Complete?.Invoke(Btn);
        }

        void OnDestroy()
        {
            Btn.onClick.RemoveListener(OnClick);
        }
    }
}