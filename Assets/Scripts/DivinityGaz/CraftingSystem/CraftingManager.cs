using DivinityGaz.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.CraftingSystem 
{
    public class CraftingManager : MonoBehaviour
    {
        private struct CraftingQueue
        {
            public CraftingRecipe recipe;
            public float timer;

            public CraftingQueue (CraftingRecipe recipe)
            {
                timer = 0f;
                this.recipe = recipe;
            }

            public bool IncreaseTimer ()
            {
                timer += Time.deltaTime;
                if (recipe.CraftingDuration <= timer)
                {
                    return true;
                }
                return false;
            }
        }

        [SerializeField] private Inventory storage = null;
        [SerializeField] private List<CraftingQueue> craftingQueue = new List<CraftingQueue>();

        private void Update ()
        {
            for (int i = 0; i < craftingQueue.Count; i++)
            {
                if (craftingQueue[i].IncreaseTimer())
                {
                    CraftCompleted(craftingQueue[i].recipe);
                }
            }
        }

        private void CraftCompleted(CraftingRecipe recipe)
        {
            storage.AddItem(recipe.FinalItem);
        }

        public void StartCraft (CraftingRecipe recipe)
        {
            
        }

        private void CancelCraft (int index)
        {

        }

        public bool IsAbleToCraft (CraftingRecipe recipe)
        {
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                if (storage.HasItem(recipe.Ingredients[i].Item))
                {
                    if (storage.GetTotalQuantity(recipe.Ingredients[i].Item) < recipe.Ingredients[i].Quantity)
                    {
                        return false;
                    }
                } 
                else
                {
                    return false;
                }

            }

            return true;
        }
    }
}

