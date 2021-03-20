using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeacefulAnimal : Enemy
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
        Flee();
        meshAgentComponent.speed = runningSpeed;
        animationHandler.TriggerRunRangedAnimation();

        if (OverLap(agroRange) == false)
        {
            currentState = AIState.Patrol;
            targetDetected = null;
        }

    }

    private void Flee()
    {
        Vector3 directionToPlayer = transform.position - targetDetected.position;
        Vector3 fleePosition = transform.position + directionToPlayer;
        meshAgentComponent.SetDestination(fleePosition);
        meshAgentComponent.isStopped = false;
    }
}
