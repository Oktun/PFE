using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.Interactables
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private KeyCode interactionKey = KeyCode.E;
        [SerializeField] private float interactionRange = 8f;
        [SerializeField] private LayerMask interactionMask;
        [Space]
        [SerializeField] private Camera mainCamera = null;

        private IInteractable currentInteractable = null;

        private void Update ()
        {
            CheckInteraction();
            GetInteractable();
        }

        private void CheckInteraction ()
        {
            if (Input.GetKeyDown(interactionKey))
            {
                currentInteractable?.interact(gameObject);
            }
        }

        private void GetInteractable ()
        {
            Ray cameraRay = mainCamera.ViewportPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out RaycastHit hit,interactionRange, interactionMask.value))
            {
                if(hit.transform.TryGetComponent(out IInteractable interactable))
                {
                    if (currentInteractable != interactable)
                    {
                        currentInteractable = interactable;
                        currentInteractable.OnEnterInteract();
                    }
                }
            } else
            {
                currentInteractable?.OnExitInteract();
            }
        }
    }
}