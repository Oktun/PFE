using DivinityGaz.InventorySystem;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.CraftingSystem
{
    public class CraftingManager : MonoBehaviour
    {
        private class CraftingQueue
        {
            public CraftingRecipe recipe;
            public float timer;

            public CraftingQueue (CraftingRecipe recipe)
            {
                timer = recipe.CraftingDuration;
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
        [SerializeField] private Transform craftableInProgressTemplate = null;
        [SerializeField] private Transform craftableInProgressContainer = null;

        private List<CraftingQueue> craftingQueue = new List<CraftingQueue>();

        public void CraftCompleted (CraftingRecipe recipe)
        {
            storage.AddItem(recipe.FinalItem);
        }

        public void StartCraft (CraftingRecipe recipe)
        {
            Transform spawnedCraftableInProgress = Instantiate(craftableInProgressTemplate, craftableInProgressContainer);

            spawnedCraftableInProgress.GetComponent<CraftingInProgress>().Set(this, recipe, craftingQueue.Count);

            craftingQueue.Add(new CraftingQueue(recipe));

            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                storage.RemoveItem(recipe.Ingredients[i]);
            }
        }

        public void CancelCraft (int index) 
        {
            for (int i = 0; i < craftingQueue[index].recipe.Ingredients.Count; i++)
            {
                storage.AddItem(craftingQueue[index].recipe.Ingredients[i]);
            }

            craftingQueue.RemoveAt(index); 
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
                } else
                {
                    return false;
                }

            }

            return true;
        }
    }
}

