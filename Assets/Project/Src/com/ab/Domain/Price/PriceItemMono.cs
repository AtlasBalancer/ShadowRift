using System.Text;
using com.ab.common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.ab.domain.price
{
    public class PriceItemMono : EntityLink
    {
        public Image Icon;
        public TMP_Text Amount;

        readonly StringBuilder _sb = new();

        public void UpdateData(Sprite sprite, int amount, Transform parent)
        {
            _sb.Clear();
            _sb.Append(amount);
            Amount.SetText(_sb);
            Icon.sprite = sprite;

            transform.SetParent(parent, false);
        }
    }
}