using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.InventorySystem
{
    public abstract class InventoryItem : Item
    {
        [SerializeField] private Transform dropObject = null;
        public Transform DropObject { get { return dropObject; } }

        [Header("Item Information")]
        [SerializeField, Min(1)] private int maxStack = 1;

        public int MaxStack { get { return maxStack; } }
        public override string ColoredName
        {
            get { return Name; }
        }
    }
}

