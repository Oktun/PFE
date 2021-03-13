using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.InventorySystem
{
    public abstract class Item : ScriptableObject
    {
        [Header("Basic Information")]
        [SerializeField] private new string name = string.Empty;
        [SerializeField] private Sprite icon = null;
        [SerializeField] private bool isEquipable = false;

        public string Name { get { return name; } }
        public Sprite Icon { get { return icon; } }
        public abstract string ColoredName { get; } 
        public bool IsEquipable { get { return isEquipable; } }

        public abstract string GetInfoDisplayString ();
    }
}
