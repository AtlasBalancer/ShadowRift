using com.ab.complexity.core;
using com.ab.domain.equip;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvCardItemMono : MonoBehaviour
    {
        public W.Entity Ent;

        public TMP_Text Title;

        public Image Icon;
        public TMP_Text Amount;
        public TMP_Text Description;

        public Button Equip;
        public Button UnEquip;

        public Button BackButton;

        public void Subscribe()
        {
            BackButton.onClick.AddListener(Hide);
            Equip.onClick.AddListener(SetEquip);
            UnEquip.onClick.AddListener(SetUnquip);
        }

        void SetEquip()
        {
            W.Events.Send(new EquipSetEvent(Ent));
            SetEquipButton(true);
        }

        void SetUnquip()
        {
            W.Events.Send(new EquipUnSetEvent(Ent));
            SetEquipButton(false);
        }

        public void Show(W.Entity ent, Sprite sprite, int amount, string title, string description)
        {
            Ent = ent;
            Title.SetText(title);
            Icon.sprite = sprite;
            Description.SetText(description);
            Amount.SetText(amount.ToString());

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