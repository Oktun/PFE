using DivinityGaz.CustomEvents.Single;
using DivinityGaz.InventorySystem;
using System.Text;
using UnityEngine;

namespace DivinityGaz.Interactables
{
    public class ItemPicker : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemSlot itemToGive;

        [Header("Events")]
        [Space]
        [SerializeField] private StringEvent onEnterInteract = null;
        [SerializeField] private VoidEvent onExitInteract = null;

        public void Interact (GameObject other)
        {
            if (other.TryGetComponent(out IItemContainer itemContainer))
            {
                ItemSlot returnValue = itemContainer.AddItem(itemToGive);

                if (returnValue.Quantity == 0)
                {
                    Destroy(gameObject);
                } else
                {
                    itemToGive = returnValue;
                }
            }
        }

        public void OnEnterInteract ()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Press E to <b>").Append(itemToGive.Item.Name).Append("</b>");

            onEnterInteract?.Invoke(stringBuilder.ToString());
        }

        public void OnExitInteract ()
        {
            onExitInteract?.Invoke();
        }
    }
}