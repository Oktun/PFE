using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DivinityGaz.InventorySystem
{
    public class HotbarSlot : ItemSlotUI, IDropHandler
    {
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;

        private Item slotItem = null;
        public override Item SlotItem
        {
            get { return slotItem; }
            set { slotItem = value; UpdateSlotUI(); }
        }

        public bool AddItem (Item itemToAdd)
        {
            if (slotItem != null) { return false; }

            slotItem = itemToAdd;
            return true;
        }

        public void UseSlot (int index)
        {
            if (index != SlotIndex) { return; }

            //Use item
        }

        public override void OnDrop (PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            if (itemDragHandler == null) { return; }

            if ((itemDragHandler.ItemSlotUI.SlotItem is IHotbarItem) == false) { return; }

            if (itemDragHandler.ItemSlotUI is HotbarSlot hotbarSlot)
            {
                Item oldItem = SlotItem;
                SlotItem = hotbarSlot.SlotItem;
                hotbarSlot.SlotItem = oldItem;
            } else if (itemDragHandler.ItemSlotUI is InventorySlot inventorySlot)
            {
                SlotItem = inventorySlot.ItemSlot.Item;
            }
        }

        public override void UpdateSlotUI ()
        {
            if(SlotItem == null)
            {
                EnableSlotUI(false);
                return;
            }

            itemIcon.sprite = SlotItem.Icon;
            EnableSlotUI(true);
            SetItemQuantityUI();
        }

        private void SetItemQuantityUI ()
        {
            if (SlotItem is InventoryItem inventoryItem)
            {
                if (inventory.HasItem(inventoryItem))
                {
                    /*
                    int itemCount = inventory.ItemContainer.GetTotalQuantity(inventoryItem);
                    itemQuantityText.text = itemCount > 1 ? itemCount.ToString() : string.Empty;
                    */
                } else
                {
                    SlotItem = null;
                }
            } else
            {
                itemQuantityText.enabled = false;
            }
        }

        protected override void EnableSlotUI (bool state)
        {
            base.EnableSlotUI(state);
            itemQuantityText.enabled = false;
        }
    }
}
