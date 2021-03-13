using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DivinityGaz.InventorySystem
{
    public class InventoryUISlotsHandler : MonoBehaviour
    {
        [Header("Logic Input")]
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private GameObject inventoryPanel = null;

        private bool isInventoryUIDisplayed = false;
        [Space]
        [SerializeField] private float tweenDuration = .3f;

        [Space]
        [Header("UI Settings")]
        [SerializeField] private Transform slotsContainer = null;
        [SerializeField] private List<InventorySlot> inventorySlotsUI = new List<InventorySlot>();

        [SerializeField] private ItemHandler itemHandler = null;
        public ItemHandler ItemHandler { get { return itemHandler; } }
        
        public Inventory Inventory { get => inventory; }

        private void Awake ()
        {
            inventoryPanel.transform.DOScaleX(0f, 0f);
        }

        public void UpdateSlots ()
        {
            for (int i = 0; i < inventorySlotsUI.Count; i++)
            {
                inventorySlotsUI[i].UpdateSlotUI();
            }
        }

        private void Update ()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                isInventoryUIDisplayed = !isInventoryUIDisplayed;
                TweenOnOffInventoryUI(isInventoryUIDisplayed);
                if (isInventoryUIDisplayed == false) { itemHandler.DisableDisplay(); }
            }
        }

        private void TweenOnOffInventoryUI (bool state)
        {
            float scaleValue = state ? 1f : 0f;

            inventoryPanel.transform.DOScale(scaleValue, tweenDuration);
        }
    }
}