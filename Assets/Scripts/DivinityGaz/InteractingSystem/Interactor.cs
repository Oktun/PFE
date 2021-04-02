using DivinityGaz.InventorySystem;
using UnityEngine;

namespace DivinityGaz.Interactables
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private GameObject playersGameObject = null;
        private IInteractable currentInteractable = null;

        public bool isInteracting = false;

        private void Update () => CheckInteraction();

        private void CheckInteraction ()
        {
            if (Input.GetKeyDown(InputManager.IM.interactionKey))
            {
                currentInteractable?.Interact(playersGameObject);
            }
        }

        private void OnTriggerEnter (Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                currentInteractable = interactable;
                currentInteractable.OnEnterInteract();
            }
        }

        private void OnTriggerStay (Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable) && currentInteractable != null && currentInteractable != interactable)
            {
                currentInteractable = interactable;
                currentInteractable.OnEnterInteract();
            }
        }

        private void OnTriggerExit (Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                currentInteractable.OnExitInteract();
                currentInteractable = null;
            }
        }
    }
}