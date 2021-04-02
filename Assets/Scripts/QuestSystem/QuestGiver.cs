using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public PlayerQuestHolder player;

    [Space]
    [Header("Quests")]
    public List<QuestState> questState;

    [Space]
    [Header("Quests Index")]
    public int currentQuestStateIndex = 0;

    [Space]
    [Header("UI Quests")]
    [SerializeField] private GameObject questWindow;
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text rewardText;

    private void QuestWindowGameObject(bool b) => questWindow.SetActive(b);

    public void OpenQuestWindow()
    {
        QuestWindowGameObject(true);
        titleText.text = questState[currentQuestStateIndex].quest.title;
        descriptionText.text = questState[currentQuestStateIndex].quest.discription;
        rewardText.text = questState[currentQuestStateIndex].quest.xpReward.ToString();
    }

    public void AcceptQuest()
    {
        QuestWindowGameObject(false);
        questState[currentQuestStateIndex].isActivated = true;
        player.questState = questState[currentQuestStateIndex];
    }

    //button to Check state
    public void CheckQuestState()
    {
        if(player.questState.quest == null)
        {
            Debug.Log("There's no Quest Assigned !! ");
        }
        else
        {
            if (player.CheckIfQuestIsCompleted() == true)
            {
                Debug.Log("Quest Completed");
                questState[currentQuestStateIndex].Complete();
                player.questState.quest = null;
            }
            else
            {
                Debug.Log("Quest NOTTTT Completed !!!!");
            }
        }
    }

}
