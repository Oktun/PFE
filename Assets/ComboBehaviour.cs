using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboBehaviour : StateMachineBehaviour
{
    [SerializeField] private int comboValue = 0;
    [SerializeField] private string comboParamterName = string.Empty;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(comboParamterName, comboValue);
    }
   
}
