using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Quest",menuName = "QuestSystem/Quest")]
public class Quest : ScriptableObject
{
    public string title;

    [TextArea(2, 5)]
    public string discription;
    public int xpReward;

    public List<QuestGoalState> questGoals;
  
}
