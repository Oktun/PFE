using UnityEngine;
using UnityEngine.UI;

namespace DivinityGaz.InventorySystem
{
    public class ItemHandler : MonoBehaviour
    {
        private InventoryItem itemSelected = null;
        private int itemSlotIndex = -1;
        public bool IsDisplayLocked = false;

        [SerializeField] private Inventory playersInventory = null;

        public void DisplayItem (InventoryItem item, int index, bool isClick = false)
        {
            if (IsDisplayLocked && !isClick) { return; }

            itemSelected = item;
            itemSlotIndex = index;

            TweenOnItemDisplay();
        }

        public void DisableDisplay (bool isClick = false)
        {
            if (IsDisplayLocked && !isClick) { return; }

            TweenOffItemDisplay();
        }

        public void Drop ()
        {
            playersInventory.RemoveAt(itemSlotIndex);
        }

        #region UI_Related
        [SerializeField] private Image image = null;

        private void TweenOnConfirmationScreen ()
        {

        }

        private void TweenOffConfirmationScreen ()
        {

        }

        private void TweenOnItemDisplay ()
        {

        }

        private void TweenOffItemDisplay ()
        {

        }

        #endregion
    }
}