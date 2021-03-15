using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildAnimal : Enemy
{
    [Space]
    [SerializeField] private float meleeHurtZone = 2f;
    [SerializeField] private Color meleeHurtZoneGizmoColor = Color.red;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackTimer = 0f;
    bool isAttacking = false;
    [SerializeField] private int attackDamage;

    [Space]
    [Header("Bomber Settings")]
    [SerializeField] private float radius = 5.0F;
    [SerializeField] private float power = 10.0F;
    [SerializeField] private bool hasExplosed = false;


    protected override void Awake()
    {
        base.Awake();
        //transform.GetChild(Random.Range(0, transform.childCount - 1)).gameObject.SetActive(true);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = meleeHurtZoneGizmoColor;
        Gizmos.DrawWireSphere(this.transform.position, meleeHurtZone);
    }

    protected override void AttackState()
    {
        attackTimer += Time.deltaTime;

        Transform player = null;
        if (OverLap(meleeHurtZone, out player, "Player"))
        {
            transform.LookAt(tragetDetected);
            meshAgentComponent.isStopped = true;
            if (hasExplosed == false)
            {
                //ExploseHimSelf();
                hasExplosed = true;
            }

            if (attackCooldown <= attackTimer)
            {
                attackTimer -= attackCooldown;
                animationHandler.TriggerAttackAnimation(true);
            }
            else
            {
                animationHandler.TriggerIdleAnimation();
            }
            //Explosion
        }
        else
        {
            meshAgentComponent.isStopped = false;
            meshAgentComponent.SetDestination(tragetDetected.position);
            animationHandler.TriggerRunRangedAnimation();
            isAttacking = false;
            meshAgentComponent.speed = runningSpeed;
            tragetLastPosition = tragetDetected.position;
        }

        if (OverLap(agroRange) == false)
        {
            currentState = AIState.Searching;
            tragetDetected = null;
        }
    }
}
