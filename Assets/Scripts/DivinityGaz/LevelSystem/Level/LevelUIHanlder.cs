using DivinityGaz.LevelSystem.Level;
using System;
using UnityEngine;

namespace DivinityGaz.LevelSystem
{
    public class LevelUIHanlder : MonoBehaviour
    {
        [SerializeField] private LevelBehaviour levelBehaviour = null;
        [SerializeField] private PlayerLevelUI playerLevelUI = null;

        // Values needed to tween
        private int currentLevel = 1;
        private int currentXp = 0;
        private int currentLevelMaxXP = 100;

        public int CurrentLevel => currentLevel;
        public int CurrentXp => currentXp;
        public int CurrentLevelMaxXP => currentLevelMaxXP;

        private bool isFirstTime = true;
        private bool isAnimating = false;

        private float disableTimer = 0f;
        [Space]
        [SerializeField] float hideUIAfter = 1.5f;

        private float updateTimer = 0f;
        private float updateTimerMax = 0.01f;

        private void Update ()
        {
            if (isAnimating)
            {
                if (isFirstTime)
                {
                    isFirstTime = false;
                    playerLevelUI.EnableTween();
                }

                if (playerLevelUI.IsFading) { return; }

                // The update timer to make sure it's frame independent
                updateTimer += Time.deltaTime;
                while (updateTimer >= updateTimerMax)
                {
                    updateTimer -= updateTimerMax;
                    if (currentLevel < levelBehaviour.CurrentLevel)
                    {
                        AddXP();
                    } else
                    {
                        if (currentXp < levelBehaviour.CurrentXp)
                        {
                            AddXP();
                        } else
                        {
                            isAnimating = false;
                        }
                    }
                }
            } 
            else if (disableTimer < hideUIAfter)
            {
                disableTimer += Time.deltaTime;
                if (disableTimer >= hideUIAfter)
                {
                    playerLevelUI.DisableTween();
                    isFirstTime = true;
                }
            }
        }

        private void AddXP ()
        {
            disableTimer = 0;
            currentXp++;
            if (currentXp >= currentLevelMaxXP)
            {
                currentLevel++;
                currentXp = 0;
                currentLevelMaxXP = (int)(currentLevelMaxXP * levelBehaviour.LevelXpMultiplier);
            } 
            
            playerLevelUI.UpdateUI(this);
        }

        private void OnLoad ()
        {
            currentLevel = levelBehaviour.CurrentLevel;
            currentXp = levelBehaviour.CurrentXp;
            currentLevelMaxXP = levelBehaviour.XpToReachNextLevel;
            playerLevelUI.UpdateUI(this);
        }

        private void Awake ()
        {
            if (levelBehaviour)
            {
                levelBehaviour.OnLoad += OnLoad;
                levelBehaviour.OnXPAdded += OnXPAdded;
                levelBehaviour.OnLevelUp += OnLeveledUp;
            } 
            else
            {
                Debug.Log("Level UI Hanlder is missing level behaviour : " + gameObject.name);
            }
        }

        private void OnDisable ()
        {
            levelBehaviour.OnLoad -= OnLoad;
            levelBehaviour.OnXPAdded -= OnXPAdded;
            levelBehaviour.OnLevelUp -= OnLeveledUp;
        }

        private void OnXPAdded ()
        {
            isAnimating = true;
        }

        private void OnLeveledUp ()
        {
            isAnimating = true;
        }
    }
}