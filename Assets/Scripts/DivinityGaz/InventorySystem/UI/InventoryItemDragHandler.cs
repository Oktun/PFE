using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DivinityGaz.InventorySystem
{
    [RequireComponent(typeof(LayoutElement))]
    public class InventoryItemDragHandler : ItemDragHandler
    {
        private void Reset () => GetComponent<LayoutElement>().ignoreLayout = true;

        public override void OnPointerUp (PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (eventData.hovered.Count == 0)
                {
                    // DropItem
                }
            }
        }

        public override void OnPointerEnter (PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            ((InventorySlot)ItemSlotUI).Handler.ItemHandler.DisplayItem(itemSlotUI.SlotItem, ItemSlotUI.SlotIndex);
        }
    }
}

