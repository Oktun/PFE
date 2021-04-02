using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestHolder : MonoBehaviour
{
    public QuestState questState = null;

    public bool CheckIfQuestIsCompleted()
    {
        var quest = questState.quest;
        if (quest != null)
        {
            var questList = quest.questGoals;
            foreach (QuestGoalState goal in questList)
            {
                if(goal.IsReached() == false)
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }
}
