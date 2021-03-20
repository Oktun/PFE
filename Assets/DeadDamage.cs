using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadDamage : StateMachineBehaviour
{
    [SerializeField] private Enemy enemy;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.DealDamage();
    }

}
