using UnityEngine;

namespace DivinityGaz.InventorySystem
{
    public class Hotbar : MonoBehaviour
    {
        [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[10];
        [SerializeField] private InputSystem inputSystem = null;

        public void Add (Item itemToAdd)
        {
            foreach (HotbarSlot slot in hotbarSlots)
            {
                if (slot.AddItem(itemToAdd)) { return; }
            }
        }

        private void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UseHotbarSlot(0);
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UseHotbarSlot(1);
            } else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UseHotbarSlot(2);
            } else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                UseHotbarSlot(3);
            }
        }

        private void UseHotbarSlot (int index)
        {
            IHotbarItem itemUsed = hotbarSlots[index].SlotItem as IHotbarItem;

            itemUsed?.Use(inputSystem);
        }
    }
}
