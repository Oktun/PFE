using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.InventorySystem
{
    public interface IItemContainer
    {
        bool HasItem (InventoryItem item);
        int GetTotalQuantity (InventoryItem item);

        ItemSlot AddItem (ItemSlot itemSlot);
        void RemoveItem (ItemSlot itemSlot);
        void RemoveAt (int index);
        void SwapItems (int indexOne, int indexTwo);
    }
}

