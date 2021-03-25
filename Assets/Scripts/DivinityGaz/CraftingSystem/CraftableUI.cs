using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DivinityGaz.CraftingSystem
{
    public class CraftableUI : MonoBehaviour
    {
        [SerializeField] private CraftingManager craftingManager = null;
        [SerializeField] private CraftingRecipe recipe = null;

        [Space]
        [SerializeField] private Image FinalItemIcon = null;
        [SerializeField] private TextMeshProUGUI FinalItemNameText = null;

        [Space]
        [SerializeField] private Image ingr1Img, ingr2Img, ingr3Img;
        [SerializeField] private TextMeshProUGUI ingr1Text, ingr2Text, ingr3Text;

        [Space]
        [SerializeField] private Button craftButton = null;

        public void Start ()
        {
            FinalItemIcon.sprite = recipe.FinalItem.Item.Icon;
            FinalItemNameText.text = recipe.FinalItem.Item.Name;

            ingr1Img.sprite = recipe.Ingredients[0].Item.Icon;
            ingr1Text.text = recipe.Ingredients[0].Quantity.ToString();

            ingr2Img.sprite = recipe.Ingredients[1].Item.Icon;
            ingr2Text.text = recipe.Ingredients[1].Quantity.ToString();

            if(recipe.Ingredients.Count == 2) {
                ingr3Img.transform.parent.gameObject.SetActive(false);
                return; 
            }

            ingr3Img.transform.parent.gameObject.SetActive(true);
            ingr3Img.sprite = recipe.Ingredients[2].Item.Icon;
            ingr3Text.text = recipe.Ingredients[2].Quantity.ToString();
        }

        public void OnItemsUpdated ()
        {
            craftButton.interactable = craftingManager.IsAbleToCraft(this.recipe);
        }

        private void OnEnable () => craftButton.onClick.AddListener(OnClick);
        private void OnDisable () => craftButton.onClick.RemoveListener(OnClick);

        public void OnClick () => craftingManager.StartCraft(this.recipe);
    }
}