using com.ab.complexity.core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvCardItemMono : MonoBehaviour
    {
        public W.Entity Ent;

        public TMP_Text Title;

        public RectTransform IconRoot;
        public Image Icon;
        public TMP_Text Amount;
        public TMP_Text Description;

        public Button Equip;
        public Button UnEquip;

        public Button BackButton;

        public void Init()
        {
            BackButton.onClick.AddListener(Hide);
            Equip.onClick.AddListener(SetEquip);
            UnEquip.onClick.AddListener(SetUnquip);
        }

        void SetEquip()
        {
            Ent.Add<EquipCommand>();
            SetEquipButton(true);
        }

        void SetUnquip()
        {
            Ent.Add<UnEquipCommand>();
            SetEquipButton(false);
        }

        public void Show(W.Entity ent, Image icon, int amount, string title, string description, bool equipped)
        {
            if (Icon != null)
            {
                Destroy(Icon);
                Icon = null;
            }

            Title.SetText(title);
            Icon = Instantiate(icon);
            Icon.transform.SetParent(IconRoot, false);

            Amount.SetText(amount.ToString());
            Description.SetText(description);

            SetEquipButton(equipped);

            Ent = ent;
            gameObject.SetActive(true);
            BackButton.gameObject.SetActive(true);
        }

        void SetEquipButton(bool equipped)
        {
            Equip.gameObject.SetActive(!equipped);
            UnEquip.gameObject.SetActive(equipped);
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);

            BackButton.gameObject.SetActive(false);
        }
    }
}