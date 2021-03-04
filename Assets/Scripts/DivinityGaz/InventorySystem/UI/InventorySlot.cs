using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DivinityGaz.InventorySystem
{
    public class InventorySlot : ItemSlotUI, IDropHandler
    {
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;
        
        public InventoryUISlotsHandler Handler = null;

        public override Item SlotItem { get { return ItemSlot.Item; } set { } }
        public ItemSlot ItemSlot => Handler.Inventory.GetSlotByIndex(SlotIndex);

        public override void OnDrop (PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler != null)
            {
                if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
                {
                    Handler.Inventory.SwapItems(itemDragHandler.ItemSlotUI.SlotIndex, this.SlotIndex);
                }
            }
        }

        protected override void EnableSlotUI (bool state)
        {
            base.EnableSlotUI(state);
            itemQuantityText.enabled = state;
        }

        public override void UpdateSlotUI ()
        {
            if (ItemSlot.Item == null)
            {
                EnableSlotUI(false);
            } else
            {
                EnableSlotUI(true);
                itemIcon.sprite = ItemSlot.Item.Icon;
                itemQuantityText.text = ItemSlot.Quantity > 1 ? ItemSlot.Quantity.ToString() : string.Empty;
            }
        }
    }
}

