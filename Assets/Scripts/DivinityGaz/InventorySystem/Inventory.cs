using DivinityGaz.CustomEvents.Single;
using UnityEngine;
using UnityEngine.Events;

namespace DivinityGaz.InventorySystem
{
    public class Inventory : MonoBehaviour, IItemContainer
    {
        [SerializeField] private int size = 12;
        [Space]
        [SerializeField] private UnityEvent onInventoryItemsUpdate = null;
        [SerializeField] private ItemSlot[] itemSlots = new ItemSlot[12];

        public void SetSize (int slotNumber) => itemSlots = new ItemSlot[slotNumber];

        public ItemSlot AddItem (ItemSlot itemSlot)
        {
            // We will look if the Item exists and Add it
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == itemSlot.Item)
                {
                    if (itemSlots[i].RemainingSpace() >= itemSlot.Quantity)
                    {
                        itemSlots[i].Quantity += itemSlot.Quantity;

                        itemSlot.Quantity = 0;

                        onInventoryItemsUpdate?.Invoke();

                        return itemSlot;
                    } else
                    {
                        itemSlot.Quantity -= itemSlots[i].RemainingSpace();

                        itemSlots[i].Quantity = itemSlots[i].Item.MaxStack;
                    }
                }
            }

            onInventoryItemsUpdate?.Invoke();
            // If we reach this then There is Items Remaining to Add to an empty slot
            // So we Will look for an empty one
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == null)
                {
                    if (itemSlot.Quantity > itemSlot.Item.MaxStack)
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.Item, itemSlot.Item.MaxStack);

                        itemSlot.Quantity -= itemSlot.Item.MaxStack;

                    } else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.Item, itemSlot.Quantity);

                        itemSlot.Quantity = 0;

                        onInventoryItemsUpdate?.Invoke();

                        return itemSlot;
                    }
                }
            }

            onInventoryItemsUpdate?.Invoke();

            return itemSlot;
        }

        public ItemSlot GetSlotByIndex (int index)
        {
            return itemSlots[index];
        }

        public int GetTotalQuantity (InventoryItem item)
        {
            int totalQuantityInInventory = 0;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.Item == null) { continue; }
                if (itemSlot.Item != item) { continue; }

                totalQuantityInInventory += itemSlot.Quantity;
            }

            return totalQuantityInInventory;
        }

        public bool HasItem (InventoryItem item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.Item == null) { continue; }
                if (itemSlot.Item != item) { continue; }

                return true;
            }

            return false;
        }

        public void RemoveAt (int index)
        {
            itemSlots[index] = new ItemSlot();

            onInventoryItemsUpdate?.Invoke();
        }

        public void RemoveItem (ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlot.Item != itemSlots[i].Item) { continue; }

                if (itemSlots[i].Quantity <= itemSlot.Quantity)
                {
                    itemSlot.Quantity -= itemSlots[i].Quantity;

                    itemSlots[i] = new ItemSlot();
                } else
                {
                    itemSlots[i].Quantity -= itemSlot.Quantity;
                }

                if (itemSlot.Quantity == 0)
                {
                    break;
                }
            }

            onInventoryItemsUpdate?.Invoke();
        }

        public void SwapItems (int indexOne, int indexTwo)
        {
            if (indexOne.Equals(indexTwo)) { return; }

            if (itemSlots[indexTwo].Item == null)
            {
                ItemSlot auxSlot = itemSlots[indexOne];
                itemSlots[indexOne] = itemSlots[indexTwo];
                itemSlots[indexTwo] = auxSlot;
            } else
            {
                if (itemSlots[indexOne].Item == itemSlots[indexTwo].Item)
                {
                    if (itemSlots[indexOne].Quantity <= itemSlots[indexTwo].RemainingSpace())
                    {
                        itemSlots[indexTwo] = new ItemSlot(itemSlots[indexOne].Item, itemSlots[indexOne].Quantity + itemSlots[indexTwo].Quantity);

                        itemSlots[indexOne] = new ItemSlot();
                    } else
                    {
                        ItemSlot auxSlot = itemSlots[indexOne];
                        itemSlots[indexOne] = itemSlots[indexTwo];
                        itemSlots[indexTwo] = auxSlot;
                    }
                }
            }

            onInventoryItemsUpdate?.Invoke();
        }
    }
}

