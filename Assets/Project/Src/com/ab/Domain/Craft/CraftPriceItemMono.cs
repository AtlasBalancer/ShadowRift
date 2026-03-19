using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.ab.domain.craft
{
    public class CraftPriceItemMono : MonoBehaviour
    {
        public Image Icon;
        public TMP_Text Amount;

        private StringBuilder _sb = new ();
        public void UpdateData(Sprite sprite, int amount)
        {
            _sb.Clear();
            _sb.Append(amount);
            Amount.SetText(_sb);

            Icon.sprite = sprite;
        }
    }
}