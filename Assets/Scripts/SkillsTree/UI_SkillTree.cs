using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTree : MonoBehaviour
{
    [SerializeField] private PlayerSkills playerSkills;
    [SerializeField] private Color lockColor;
    [SerializeField] private Color unLockColor;
    [SerializeField] private Text skillPointText;

    [System.Serializable]
    public class ButtonTextString
    {
        public string damageIncrease2 = string.Empty;
        public string damageIncrease1 = string.Empty;
        public string damageReduce2 = string.Empty;
        public string damageReduce1 = string.Empty;
        public string hunger1 = string.Empty;
        public string hunger2 = string.Empty;
        public string thirsd2 = string.Empty;
        public string thirsd1 = string.Empty;
        public string animalVision = string.Empty;
    }

    [SerializeField] private ButtonTextString buttonText;

    private void Awake()
    {
        //Text Action Subcriber
        playerSkills.OnSkillChange += SkillTextChanger;

        //Buttons Action
        transform.Find(buttonText.damageIncrease2).GetComponent<Button>().onClick.AddListener(DamageIncrease2_Listenner);

        transform.Find(buttonText.damageIncrease1).GetComponent<Button>().onClick.AddListener(DamageIncrease1_Listenner);

        transform.Find(buttonText.damageReduce2).GetComponent<Button>().onClick.AddListener(DamageReduce2_Listenner);

        transform.Find(buttonText.damageReduce1).GetComponent<Button>().onClick.AddListener(DamageReduce1_Listenner);

        transform.Find(buttonText.hunger2).GetComponent<Button>().onClick.AddListener(Hunger2_Listenner);

        transform.Find(buttonText.hunger1).GetComponent<Button>().onClick.AddListener(Hunger1_Listenner);

        transform.Find(buttonText.thirsd2).GetComponent<Button>().onClick.AddListener(Thirsd2_Listenner);

        transform.Find(buttonText.thirsd1).GetComponent<Button>().onClick.AddListener(Thirsd1_Listenner);

        transform.Find(buttonText.animalVision).GetComponent<Button>().onClick.AddListener(AnimalVision_Listenner);
 
    }


    private void SkillTextChanger(int value)
    {
        skillPointText.text = value.ToString();
    }

    #region ButtonsListenners

    private void DamageIncrease2_Listenner()
    {
        if(playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Damage_I_2))
        {
            transform.Find(buttonText.damageIncrease2).GetComponent<Image>().color = unLockColor;
        }
    }

    private void DamageIncrease1_Listenner()
    {
        if(playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Damage_I_1))
        {
            transform.Find(buttonText.damageIncrease1).GetComponent<Image>().color = unLockColor;
        }
    }

    private void DamageReduce2_Listenner()
    {
        if (playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Damage_R_2))
        {
            transform.Find(buttonText.damageReduce2).GetComponent<Image>().color = unLockColor;
        }

    }

    private void DamageReduce1_Listenner()
    {
        if (playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Damage_R_1))
        {
            transform.Find(buttonText.damageReduce1).GetComponent<Image>().color = unLockColor;
            Debug.Log("Cannot unlock !");
        }

        //Debug.Log("health 2 Skill unlocked");
    }

    private void Hunger1_Listenner()
    {
        if(playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Hunger_1))
        {
            transform.Find(buttonText.hunger1).GetComponent<Image>().color = unLockColor;
        }
    }

    private void Hunger2_Listenner()
    {
        if(playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Hunger_2))
        {
            transform.Find(buttonText.hunger2).GetComponent<Image>().color = unLockColor;
        }
    }

    private void Thirsd2_Listenner()
    {
        if(playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Thrisd_2))
        {
            transform.Find(buttonText.thirsd2).GetComponent<Image>().color = unLockColor;
        }
    }

    private void Thirsd1_Listenner()
    {
        if(playerSkills.TryUnlockSkill(PlayerSkills.SkillType.Thrisd_1))
        {
            transform.Find(buttonText.thirsd1).GetComponent<Image>().color = unLockColor;
        }
    }

    private void AnimalVision_Listenner()
    {
        if(playerSkills.TryUnlockSkill(PlayerSkills.SkillType.AnimalNightVision))
        {
            transform.Find(buttonText.animalVision).GetComponent<Image>().color = unLockColor;
        }
    }

    #endregion

    public void SetPlayerSkills(PlayerSkills playerSkills)
    { 
        this.playerSkills = playerSkills;
    }

}
