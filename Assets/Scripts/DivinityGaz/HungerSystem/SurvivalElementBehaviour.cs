using System;
using UnityEngine;

namespace DivinityGaz.HungerSystem
{
    public class SurvivalElementBehaviour : MonoBehaviour
    {
        private float currentHungerPercentage = 100f;
        private float currentThirstPercentage = 100f;

        [SerializeField] private Action<SurvivalElementBehaviour> onStatsChange = null;

        [Space]
        [Header("Hunger Settings")]
        [SerializeField] private float hungerDropRate = 1f;
        [SerializeField] private float hungerDropTime = 3f;
        [SerializeField] private float currentHungerEffectiveness = 0f;
        private float hungerDropTimer = 0f;

        [Space]
        [Header("Thirst Settings")]
        [SerializeField] private float thirstDropRate = 1f;
        [SerializeField] private float thirstDropTime = 3f;
        [SerializeField] private float currentThirstEffectiveness = 0f;
        private float thirstDropTimer = 0f;

        private void Update ()
        {
            UpdateStat(ref thirstDropTimer, thirstDropTime, ref currentThirstPercentage, thirstDropRate, currentThirstEffectiveness);
            UpdateStat(ref hungerDropTimer, hungerDropTime, ref currentHungerPercentage, hungerDropRate, currentHungerEffectiveness);
        }

        private void UpdateStat (ref float droptimer, float dropTime, ref float currentPercentage, float dropRate, float currentEffectiveness)
        {
            if (currentPercentage == 0) { return; }

            if (TimeStat(ref droptimer, dropTime))
            {
                currentPercentage -= dropRate + dropRate * currentEffectiveness;

                if (currentPercentage < 0) { currentPercentage = 0; }
                Debug.Log(currentPercentage);

                onStatsChange?.Invoke(this);
            }
        }

        public bool TimeStat (ref float timer, float time)
        {
            timer += Time.deltaTime;

            if (timer >= time)
            {
                timer -= time;
                return true;
            }

            return false;
        }
    }

    public enum SurvivalStatType
    {
        Hunger = 0,
        Thirst = 1,
    }
}

