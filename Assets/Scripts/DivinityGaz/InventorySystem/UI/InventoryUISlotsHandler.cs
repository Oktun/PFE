using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.InventorySystem
{
    public class InventoryUISlotsHandler : MonoBehaviour
    {
        [Header("Logic Input")]
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private GameObject inventoryPanel = null;

        [Space]
        [Header("UI Settings")]
        [SerializeField] private Transform slotsContainer = null;
        [SerializeField] private List<InventorySlot> inventorySlotsUI = new List<InventorySlot>();

        [SerializeField] private ItemHandler itemHandler = null;
        public ItemHandler ItemHandler { get { return itemHandler; } }
        
        public Inventory Inventory { get => inventory; }

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
                inventoryPanel?.SetActive(!inventoryPanel.activeSelf);
            }
        }
    }
}