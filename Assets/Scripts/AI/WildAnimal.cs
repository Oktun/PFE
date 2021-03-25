using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DivinityGaz.HealthSystem;


public class WildAnimal : Enemy
{
    [Space]
    [SerializeField] private float meleeHurtZone = 2f;
    [SerializeField] private Color meleeHurtZoneGizmoColor = Color.red;
    [SerializeField] private float damageToDeal = 20f;


    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackTimer = 0f;
    bool isAttacking = false;

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
            Vector3 vectorLookPos = new Vector3(targetDetected.position.x, transform.position.y,
                targetDetected.position.z);
            transform.LookAt(vectorLookPos);
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
                player.GetComponent<HealthComponent>().TakeDamage(damageToDeal);
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
            meshAgentComponent.SetDestination(targetDetected.position);
            animationHandler.TriggerRunRangedAnimation();
            isAttacking = false;
            meshAgentComponent.speed = runningSpeed;
            tragetLastPosition = targetDetected.position;
        }

        if (OverLap(agroRange) == false)
        {
            currentState = AIState.Searching;
            targetDetected = null;
        }
    }
}
