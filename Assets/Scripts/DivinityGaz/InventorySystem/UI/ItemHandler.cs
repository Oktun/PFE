using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DivinityGaz.InventorySystem
{
    public class ItemHandler : MonoBehaviour
    {
        private InventoryItem itemSelected = null;
        private int itemSlotIndex = -1;
        public bool IsDisplayLocked = false;

        private bool isDisplaying = false;

        [SerializeField] private Inventory playersInventory = null;

        private void Awake ()
        {
            DisableDisplay();
        }

        public void DisplayItem (InventoryItem item, int index)
        {
            if (IsDisplayLocked) { return; }

            itemSelected = item;
            itemSlotIndex = index;

            itemNameText.text = item.Name;
            itemIcon.sprite = item.Icon;
            itemDescriptionText.text = item.GetInfoDisplayString();

            if (item.IsEquipable)
            {
                equipButton.gameObject.SetActive(true);
            } else
            {
                equipButton.gameObject.SetActive(false);
            }

            if (item is ConsumableItem)
            {
                useButton.gameObject.SetActive(true);
            } else
            {
                useButton.gameObject.SetActive(false);
            }

            if (isDisplaying) { return; }

            TweenOnAndOffItemDisplay(true);
            isDisplaying = true;
        }

        public void DisableDisplay (bool isForced = false)
        {
            if (IsDisplayLocked && !isForced) { return; }

            isDisplaying = false;

            TweenOnAndOffItemDisplay(false);
        }

        public void Use ()
        {
            ((ConsumableItem)itemSelected)?.Use();
            if (!playersInventory.RemoveAt(itemSlotIndex, 1))
            {
                DisableDisplay(true);
            }
        }

        public void Drop ()
        {
            playersInventory.RemoveAt(itemSlotIndex);
            DisableDisplay(true);
        }

        #region UI_Confirmation_Screen_Tweening
        [Space]
        [Header("Confirmation Screen Settings")]
        [SerializeField] private Image confirmationScreenBlurImage = null;
        [SerializeField] private Image confirmationWindow = null;

        [Space]
        [SerializeField] private float confirmationScreenTweenDuration = .3f;

        public void TweenOnConfirmationScreen ()
        {
            confirmationScreenBlurImage.gameObject.SetActive(true);
            confirmationWindow.rectTransform.DOScale(1f, confirmationScreenTweenDuration);
        }

        public void TweenOffConfirmationScreen ()
        {
            confirmationWindow.rectTransform.DOScale(0f, confirmationScreenTweenDuration / 3f)
                .OnComplete(() => confirmationScreenBlurImage.gameObject.SetActive(false));
        }

        #endregion

        #region UI_Item_Displayer_Tweening

        [Space]
        [Header("Item Displayer Settings")]
        [SerializeField] private float itemDisplayerTweenDuration = .3f;

        [Space]
        [SerializeField] private TextMeshProUGUI itemNameText = null;
        [SerializeField] private Image itemNameUnderLineImage = null;

        [Space]
        [SerializeField] private Image itemIconHolder = null;
        [SerializeField] private Image itemIcon = null;

        [Space]
        [SerializeField] private TextMeshProUGUI itemDescriptionText = null;

        [Space]
        [SerializeField] private Image dropButton = null;
        [SerializeField] private TextMeshProUGUI dropText = null;

        [Space]
        [SerializeField] private Image equipButton = null;
        [SerializeField] private TextMeshProUGUI equipText = null;

        [Space]
        [SerializeField] private Image useButton = null;
        [SerializeField] private TextMeshProUGUI useText = null;

        public void TweenOnAndOffItemDisplay (bool state)
        {
            float fadeValue = state ? 1f : 0f;
            float fadeDuration = state ? itemDisplayerTweenDuration : itemDisplayerTweenDuration / 2f;

            itemNameText.DOFade(fadeValue, fadeDuration);
            itemNameUnderLineImage.DOFade(fadeValue, fadeDuration);
            itemIconHolder.DOFade(fadeValue, fadeDuration);
            itemIcon.DOFade(fadeValue, fadeDuration);
            itemDescriptionText.DOFade(fadeValue, fadeDuration);
            dropButton.DOFade(fadeValue, fadeDuration);
            dropText.DOFade(fadeValue, fadeDuration);
            equipButton.DOFade(fadeValue, fadeDuration);
            equipText.DOFade(fadeValue, fadeDuration);
            useButton.DOFade(fadeValue, fadeDuration);
            useText.DOFade(fadeValue, fadeDuration);
        }

        #endregion
    }
}