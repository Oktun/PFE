using DivinityGaz.InventorySystem;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.CraftingSystem
{
    [CreateAssetMenu(menuName ="Crafting/Recipe")]
    public class CraftingRecipe : ScriptableObject
    {
        [SerializeField] [Range(0, 3600)] private float craftingDuration = 20f;
        [SerializeField] private List<ItemSlot> ingredients = new List<ItemSlot>();
        [SerializeField] private ItemSlot finalItem = new ItemSlot(null, 1);

        public List<ItemSlot> Ingredients { get { return ingredients; } }
        public ItemSlot FinalItem { get { return finalItem; } }
        public float CraftingDuration { get { return craftingDuration; } }
    }
}