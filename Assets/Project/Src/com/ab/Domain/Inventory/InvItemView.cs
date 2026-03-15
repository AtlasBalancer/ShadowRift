using TMPro;
using System.Text;
using com.ab.common;
using com.ab.complexity.core;
using Project.Src.com.ab.Complexity.Features.Topdown;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvItemView : EntityLink
    {
        public StringBuilder _sb = new();
        public TMP_Text AmountLabel;
        public Button Btn;

        // void Press() => 
            // Link.Ent.ApplyTag<ViewPressed>(true);

        public void UpdateAmount(int amount)
        {
            _sb.Clear();
            _sb.Append(amount);

            AmountLabel.SetText(_sb);
        }
    }
}