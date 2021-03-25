using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DivinityGaz.CraftingSystem
{
    public class CraftingInProgress : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public CraftingManager CraftingManager { get; set; } = null;
        public float Timer { get; set; } = 100f;
        public float CraftLimit { get; set; }
        public int Index { get; set; }

        public CraftingRecipe Recipe { get; set; }

        [SerializeField] private Button cancelButton = null;
        [SerializeField] private Image itemIcon = null;
        [SerializeField] private TextMeshProUGUI timerText = null;
        [SerializeField] private TextMeshProUGUI quantityText = null;

        private void OnCancelButton () 
        { 
            CraftingManager.CancelCraft(this.Index);
            Destroy(gameObject);
        }

        private void OnEnable () => cancelButton.onClick.AddListener(OnCancelButton);

        private void OnDisable () => cancelButton.onClick.RemoveListener(OnCancelButton);

        public void Set (CraftingManager craftingManager, CraftingRecipe recipe, int index)
        {
            CraftingManager = craftingManager;
            this.Recipe = recipe;
            CraftLimit = recipe.CraftingDuration;
            Timer = CraftLimit;

            this.Index = index;

            itemIcon.sprite = Recipe.FinalItem.Item.Icon;
            quantityText.text = Recipe.FinalItem.Quantity.ToString();
            timerText.text = ((int)Timer).ToString("D") + "s";

            gameObject.SetActive(true);
        }

        private void Update ()
        {
            if (Timer <= 0f)
            {
                CraftingManager.CraftCompleted(this.Recipe);
                Destroy(gameObject);
            } else
            {
                Timer -= Time.deltaTime;
            }
            timerText.text = ((int)Timer).ToString("D") + "s";
        }

        void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData)
        {
            cancelButton.gameObject.SetActive(true);
            quantityText.gameObject.SetActive(false);
        }

        void IPointerExitHandler.OnPointerExit (PointerEventData eventData)
        {
            cancelButton.gameObject.SetActive(false);
            quantityText.gameObject.SetActive(true);
        }
    }
}