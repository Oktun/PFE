using UnityEngine;
using UnityEngine.EventSystems;

namespace DivinityGaz.InventorySystem
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected ItemSlotUI itemSlotUI = null;

        private CanvasGroup canvasGroupReference = null;
        private Transform originalParentTransform = null;
        private bool isHovering = false;

        public ItemSlotUI ItemSlotUI => itemSlotUI;

        private void Start () => canvasGroupReference = GetComponent<CanvasGroup>();

        public virtual void OnDrag (PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = Input.mousePosition;
            }
        }

        public virtual void OnPointerDown (PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // trigger event

                originalParentTransform = transform.parent;
                transform.SetParent(transform.parent.parent);
                canvasGroupReference.blocksRaycasts = false;
            }
        }

        public virtual void OnPointerUp (PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.SetParent(originalParentTransform);
                transform.localPosition = Vector3.zero;
                canvasGroupReference.blocksRaycasts = true;
            }
        }

        public virtual void OnPointerEnter (PointerEventData eventData)
        {
            // event
            isHovering = true;
        }

        public virtual void OnPointerExit (PointerEventData eventData)
        {
            // event
            isHovering = false;
        }

        private void OnDisable ()
        {
            if (isHovering)
            {
                // trigger event

                isHovering = false;
            }
        }
    }
}

