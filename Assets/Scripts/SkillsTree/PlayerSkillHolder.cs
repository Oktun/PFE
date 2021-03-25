using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillHolder : MonoBehaviour
{
    public int damageIncreaseAmount = 5;
    public int damageReduceAmount = 100;
    public int thirdAmount = 10;
    public int hungerAmount = 10;

    [SerializeField] private PlayerSkills playerSkills;

    private void Awake()
    {
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
    }

    void Update()
    {
        
    }

    public void PlayerSkills_OnSkillUnlocked(PlayerSkills.SkillType s)
    {
        switch(s)
        {
            case PlayerSkills.SkillType.Damage_I_2:
                SetDamageIncrease(50);
                Debug.Log("HealthMax_1 event");
                break;
            case PlayerSkills.SkillType.Damage_I_1:
                SetDamageIncrease(100);
                break;
            case PlayerSkills.SkillType.Damage_R_2:
                SetDamageReduce(10);
                break;
            case PlayerSkills.SkillType.Damage_R_1:
                SetDamageReduce(20);
                break;
            case PlayerSkills.SkillType.Thrisd_2:
                SetThrisd(15);
                break;
            case PlayerSkills.SkillType.Thrisd_1:
                SetThrisd(30);
                break;
            case PlayerSkills.SkillType.Hunger_2:
                SetHunger(15);
                break;
            case PlayerSkills.SkillType.Hunger_1:
                SetHunger(30);
                break;
            case PlayerSkills.SkillType.AnimalNightVision:
                SetAnimalNightVision();
                break;
        }    
    }

    private void SetAnimalNightVision()
    {
        Debug.Log("AnimalNightVision Activated !!!");
    }

    private void SetHunger(int v)
    {
        hungerAmount += v;
    }

    private void SetThrisd(int v)
    {
        thirdAmount += v;
    }

    private void SetDamageReduce(int v)
    {
        damageReduceAmount += v;
    }

    private void SetDamageIncrease(int v)
    {
        damageIncreaseAmount += v;
    }

    public PlayerSkills PlayerSkills => playerSkills;

}
