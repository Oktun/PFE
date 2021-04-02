using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DivinityGaz.InventorySystem;

[CreateAssetMenu(fileName = "New Quests Goal", menuName = "QuestSystem/Quest Goal")]
public class QuestGoal : ScriptableObject
{
    public Item item;

    public int requireAmount;
}
