using UnityEngine;
using UnityEngine.UI;

namespace DivinityGaz.HungerSystem
{
    public class SurvivalElementsUI : MonoBehaviour
    {
        [SerializeField] private SurvivalElementBehaviour survivalElements = null;
        [Space]
        [SerializeField] private Image hungerCircle = null;
        [SerializeField] private Image thirstCircle = null;

        private void OnEnable ()
        {
            survivalElements.OnStatsChange += OnStatsChanged;
        }

        private void OnDisable ()
        {
            survivalElements.OnStatsChange -= OnStatsChanged;
        }

        public void OnStatsChanged (SurvivalElementBehaviour survivalElement)
        {
            hungerCircle.fillAmount = survivalElement.CurrentHungerPercentage / 100;
            thirstCircle.fillAmount = survivalElement.CurrentThirstPercentage / 100;
        }
    }
}