using com.ab.core;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticPack;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.ab.common
{
    // View ref — transient, not persisted. Recreated when view is rebuilt after load.
    public readonly struct ProgressBarRef : IComponent
    {
        public readonly ProgressBarMono Val;

        public ProgressBarRef(ProgressBarMono val)
        {
            Val = val;
        }

        public void Write<TWorld>(ref BinaryPackWriter writer, World<TWorld>.Entity self) where TWorld : struct, IWorldType { }
        public void Read<TWorld>(ref BinaryPackReader reader, World<TWorld>.Entity self, byte version, bool disabled) where TWorld : struct, IWorldType { }
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