using Boo.Lang;
using DivinityGaz.Interactables;
using DivinityGaz.InventorySystem;
using System.Collections;
using System.Text;
using UnityEngine;

namespace DivinityGaz.MiningSystem
{
    public class Minable : MonoBehaviour, IInteractable
    {
        private MiningManager miningManager = null;
        public bool IsAbleToMine { get; set; } = true;

        [SerializeField] private List<InventoryItem> itemsToGive = new List<InventoryItem>();
        public List<InventoryItem> ItemsToGive => itemsToGive;

        [Space]
        [SerializeField] private string animationToCast = string.Empty;
        [SerializeField] private string InteractionText = string.Empty;

        public string AnimationToCast { get { return animationToCast; } }

        private void Awake () => miningManager = FindObjectOfType<MiningManager>();

        public void Interact (GameObject other)
        {
            miningManager.StartMining(this, animationToCast);
        }

        public void OnEnterInteract ()
        {
            var textToDisplay = new StringBuilder();
            textToDisplay.Append(InteractionText);

            miningManager.OnEnterInteraction(textToDisplay.ToString());
        }

        public void OnExitInteract () => miningManager.OnExitInteraction();
    }
}