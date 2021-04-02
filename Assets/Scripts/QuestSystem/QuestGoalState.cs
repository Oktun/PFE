using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DivinityGaz.InventorySystem;

[System.Serializable]
public class QuestGoalState  
{
    public QuestGoal questGoal;

    public int currentAmount;

    public bool IsReached() => currentAmount >= questGoal.requireAmount;

    //Must Subscrive to event PickItem 
    public void CheckItem(Item item)
    {
        if (this.questGoal.item == item)
            currentAmount++;
    }
}
