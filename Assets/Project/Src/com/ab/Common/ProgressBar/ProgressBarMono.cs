using com.ab.core;
using FFS.Libraries.StaticEcs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.ab.common.ProgressBar
{
    public readonly struct ProgressBarRef : IComponent
    {
        public readonly ProgressBarMono Val;

        public ProgressBarRef(ProgressBarMono val)
        {
            Val = val;
        }
    }

    public class ProgressBarMono : EntityLink
    {
        public Transform Root;

        public Scrollbar Scroll;
        public TMP_Text Amount;
        readonly string AMOUNT_PATTERN = "{0}/{1}";
        int _maxAmount;

        protected override void Register()
        {
            Ent.Set(new ProgressBarRef(this));
        }

        public void SetMax(int max)
        {
            _maxAmount = max;
        }

        public void UpdateAmount(int amount)
        {
            Amount.SetText(AMOUNT_PATTERN, amount, _maxAmount);
            Scroll.size = amount / (float)_maxAmount;

            Root.Active();
        }

        public void OffsetY(float offsetY)
        {
            transform.localPosition =
                new Vector3(transform.localPosition.x, offsetY, transform.localPosition.z);
        }
    }
}