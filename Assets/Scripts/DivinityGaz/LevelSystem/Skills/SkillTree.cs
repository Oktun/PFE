using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.LevelSystem
{
    public class SkillTree : MonoBehaviour
    {
        private int skillPointsAvailable = 0;
        private int totalSkillPointsSpent = 0;

        public Action OnSkillPointChanged = null;

        public void OnLevelUp ()
        {
            skillPointsAvailable++;
            OnSkillPointChanged?.Invoke();
        }

        public void UnlockSkill ()
        {

        }
    }
}