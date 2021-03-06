using UnityEngine;
using UnityEngine.UI;

namespace DivinityGaz.InventorySystem
{
    public class ItemHandler : MonoBehaviour
    {
        private Item itemSelected = null;
        private int itemSlotIndex = -1;

        public void DisplayItem (Item item, int index)
        {
            itemSelected = item;


        }

        public void Drop ()
        {

        }

        #region UI_Related
        [SerializeField] private Image image = null;

        #endregion
    }
}