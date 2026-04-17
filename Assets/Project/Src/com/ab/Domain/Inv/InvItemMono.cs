using System.Text;
using com.ab.common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvItemMono : EntityLink
    {
        public Image Icon;
        public TMP_Text AmountLabel;
        public Button Btn;
        public StringBuilder _sb = new();

        protected override void Subscribe()
        {
            Btn.onClick.AddListener(OnClick);
        }

        protected override void UnSubscribe()
        {
            Btn.onClick.RemoveListener(OnClick);
        }

        public void UpdateIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void UpdateAmount(int amount)
        {
            _sb.Clear();
            _sb.Append(amount);

            AmountLabel.SetText(_sb);
        }
    }
}