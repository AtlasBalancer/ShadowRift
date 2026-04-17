using UnityEngine;
using UnityEngine.UI;

namespace com.ab.common
{
    public class ResponseButtonRefMock : EntityLink
    {
        public Image BtnBg;
        public Color Wait;
        public Color Complete;

        public ResponseButtonMono Btn;

        protected override void Subscribe()
        {
            Btn.WaitResponse += OnWaitResponse;
            Btn.Complete += OnComplete;
            BtnBg.color = Complete;

            Btn.Init();
            Btn.Attach(Ent);
        }

        protected override void UnSubscribe()
        {
            Btn.WaitResponse -= OnWaitResponse;
            Btn.Complete -= OnComplete;
        }

        void OnComplete(Button btn)
        {
            Debug.Log($"{nameof(ResponseButtonRefMock)}:: Complete");
            BtnBg.color = Complete;
        }

        void OnWaitResponse(Button btn)
        {
            Debug.Log($"{nameof(ResponseButtonRefMock)}:: Wait click");
            BtnBg.color = Wait;
        }
    }
}