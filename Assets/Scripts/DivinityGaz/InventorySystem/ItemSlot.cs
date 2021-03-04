using System;

namespace DivinityGaz.InventorySystem
{
    [Serializable]
    public struct ItemSlot
    {
        public InventoryItem Item;
        public int Quantity;

        public int RemainingSpace ()
        {
            if (Item == null) { return -1; }

            return Item.MaxStack - Quantity;
        }

        public ItemSlot (InventoryItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}

