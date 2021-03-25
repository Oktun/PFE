using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSkillTree : MonoBehaviour
{
    [SerializeField] private PlayerSkillHolder player;
    [SerializeField] private UI_SkillTree uiSkillTree;

    private void Start()
    {
        uiSkillTree.SetPlayerSkills(player.PlayerSkills);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!player.PlayerSkills.IsSkillUnlocked(PlayerSkills.SkillType.AnimalNightVision)) //false State
            {
                Debug.Log("Skill IS STILL LOCKED !!!");
            }
            else
            {
                Debug.Log("Skill UNLOCKED :) .... ");

            }
        }
    }
}
