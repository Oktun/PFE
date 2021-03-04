using DivinityGaz.CustomEvents.Double;
using DivinityGaz.CustomEvents.Single;
using System;
using UnityEngine;

namespace DivinityGaz.LevelSystem.Level
{
    public class LevelBehaviour : MonoBehaviour
    {
        private int currentXp = 0;
        private int currentLevel = 1;
        [SerializeField] private int xpToReachNextLevel = 200;
        [SerializeField] private float levelXpMultiplier = 1.2f;

        public int CurrentXp { get { return currentXp; } }
        public int CurrentLevel { get { return currentLevel; } }
        public int XpToReachNextLevel { get { return xpToReachNextLevel; } }
        public float LevelXpMultiplier { get { return levelXpMultiplier; } }

        [Space]
        [Header("Events")]
        public Action OnXPAdded = null;
        public Action OnLevelUp = null;
        public Action OnLoad = null;
    
        public void Start ()
        {
            OnLoad?.Invoke();
        }

        private void Update ()
        {
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    AddXP(300);
                }
            }
        }

        public void AddXP (int value)
        {
            currentXp += value;

            while (currentXp >= xpToReachNextLevel)
            {
                currentLevel++;
                currentXp -= xpToReachNextLevel;
                xpToReachNextLevel = (int)(xpToReachNextLevel * levelXpMultiplier);
                OnLevelUp?.Invoke();
            }

            OnXPAdded?.Invoke();
        }
    }
}
