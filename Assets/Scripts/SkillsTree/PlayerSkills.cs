using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSkills : MonoBehaviour
{
    public Action<SkillType> OnSkillUnlocked;
    public Action<int> OnSkillChange;

    public enum SkillType
    {
        None,
        Thrisd_1,
        Thrisd_2,
        Damage_R_1,
        Damage_R_2,
        Damage_I_1,
        Damage_I_2,
        Hunger_1,
        Hunger_2,
        AnimalNightVision,
    }

    [SerializeField] private List<SkillType> unlockSkillTypeList = new List<SkillType>();
    [SerializeField] private int skillPoint = 3;

    private void Start()
    {
        OnSkillChange?.Invoke(skillPoint);
    }

    private void UnlockSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType))
        {

            unlockSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(skillType);
        }
    }

    public void AddSkill()
    {
        skillPoint++;
        OnSkillChange?.Invoke(skillPoint);
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockSkillTypeList.Contains(skillType);
    }

    public SkillType GetSkillRequirement(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Thrisd_2:            return SkillType.Thrisd_1;
            case SkillType.Hunger_2:            return SkillType.Hunger_1;
            case SkillType.Damage_I_2:          return SkillType.Damage_I_1;
            case SkillType.Damage_R_2:          return SkillType.Damage_R_1;
            case SkillType.AnimalNightVision:          return SkillType.Damage_I_2;

        }
        return SkillType.None;
    }

    public bool TryUnlockSkill(SkillType skillType)
    {
        if (skillPoint == 0 || IsSkillUnlocked(skillType)) { return false; }

        SkillType skillRequirements = GetSkillRequirement(skillType);

        if(skillRequirements != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequirements))
            {
                    skillPoint--;
                    OnSkillChange?.Invoke(skillPoint);
                    UnlockSkill(skillType);
                    return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            skillPoint--;
            OnSkillChange?.Invoke(skillPoint);
            UnlockSkill(skillType);
            return true;
        }
    }

}
