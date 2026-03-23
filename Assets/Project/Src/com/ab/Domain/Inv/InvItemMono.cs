using TMPro;
using System.Text;
using com.ab.common;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvItemMono : EntityLink
    {
        public Image Icon;
        public StringBuilder _sb = new();
        public TMP_Text AmountLabel;
        public Button Btn;

        // void Press() => 
        // Link.Ent.ApplyTag<ViewPressed>(true);

        public void UpdateIcon(Sprite icon) =>
            Icon.sprite = icon;

        public void UpdateAmount(int amount)
        {
            _sb.Clear();
            _sb.Append(amount);

            AmountLabel.SetText(_sb);
        }
    }
}