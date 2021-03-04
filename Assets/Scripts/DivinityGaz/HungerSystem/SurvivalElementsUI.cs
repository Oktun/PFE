using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DivinityGaz.HungerSystem
{
    public class SurvivalElementsUI : MonoBehaviour
    {
        [SerializeField] private Image hungerCircle = null;
        [SerializeField] private Image thirstCircle = null;

        public void onStatsChanged (int hungerLevel, int thirstLevel)
        {
            hungerCircle.fillAmount = (float)hungerLevel / 100;
            thirstCircle.fillAmount = (float)thirstLevel / 100;
        }
    }
}