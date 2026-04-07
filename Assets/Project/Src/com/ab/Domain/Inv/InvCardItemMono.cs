using com.ab.common;
using com.ab.complexity.core;
using com.ab.domain.equip;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvCardItemMono : MonoBehaviour
    {
        [Header("Configuration")] public bool HasEquip;
        [Header("Refs")] public W.Entity ShowingEnt;

        public TMP_Text Title;

        public Image Icon;
        public TMP_Text Amount;
        public TMP_Text Description;

        public Button Equip;
        public Button UnEquip;

        public Button BackButton;

        public ResponseButtonMono Btn;

        public void Subscribe()
        {
            BackButton.onClick.AddListener(Hide);
            Equip.onClick.AddListener(SetEquip);
            UnEquip.onClick.AddListener(SetUnquip);
        }

        void SetEquip()
        {
            W.Events.Send(new EquipSetEvent(ShowingEnt));
            SetEquipButton(true);
        }

        void SetUnquip()
        {
            W.Events.Send(new EquipUnSetEvent(ShowingEnt));
            SetEquipButton(false);
        }

        public void Show(W.Entity refEnt, bool equip, Sprite sprite, int amount, string title, string description)
        {
            // Btn.Subscribe(ent);

            ShowingEnt = refEnt;
            Title.SetText(title);
            Icon.sprite = sprite;
            Description.SetText(description);
            Amount.SetText(amount.ToString());

            gameObject.SetActive(true);
            BackButton.gameObject.SetActive(true);

            SetEquipButton(equip);
        }

        void SetEquipButton(bool equipped)
        {
            if (!HasEquip)
                return;

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