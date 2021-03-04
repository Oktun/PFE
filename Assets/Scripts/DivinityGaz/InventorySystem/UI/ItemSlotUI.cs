using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace DivinityGaz.InventorySystem
{
    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] protected Image itemIcon = null;
        public int SlotIndex { get; private set; }
        public abstract Item SlotItem { get; set; }

        protected virtual void Start ()
        {
            SlotIndex = transform.GetSiblingIndex();
            UpdateSlotUI();
        }

        private void OnEnable () => UpdateSlotUI();

        protected virtual void EnableSlotUI (bool state) => itemIcon.enabled = state;

        public abstract void UpdateSlotUI ();
        public abstract void OnDrop (PointerEventData eventData);
    }
}
