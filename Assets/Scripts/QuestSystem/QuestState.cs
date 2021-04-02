using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestState 
{
    public Quest quest;
    public bool isActivated;

    public void Complete()
    {
        isActivated = false;
    }
}
