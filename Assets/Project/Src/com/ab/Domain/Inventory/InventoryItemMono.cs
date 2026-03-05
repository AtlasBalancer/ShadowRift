using TMPro;
using System.Text;
using com.ab.complexity.core;
using Project.Src.com.ab.Complexity.Features.Topdown;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryItemMono : ResourceMono
    {
        public InventoryItemLink Link;
        
        public StringBuilder _sb = new();
        public TMP_Text AmountLabel;
        public Button Btn;

        public void Init(InventoryItemLink link)
        {
            Link = link;
            Btn.onClick.AddListener(Press);
        }

        void Press() => 
            Link.Ent.ApplyTag<ViewPressed>(true);

        public void UpdateAmount(int amount)
        {
            _sb.Clear();
            _sb.Append(amount);

            AmountLabel.SetText(_sb);
        }
    }
}