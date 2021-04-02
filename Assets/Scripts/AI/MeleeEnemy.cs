using UnityEngine;
using System.Collections;
using DivinityGaz.HealthSystem;

public class MeleeEnemy : Enemy {

    [Space]
    [SerializeField] private float meleeHurtZone = 2f;
    [SerializeField] private Color meleeHurtZoneGizmoColor = Color.red;
    [SerializeField] private float damageToDeal = 20f;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackTimer = 0f;
    bool isAttacking = false;

    [Header("Zombie Teleporter Settings")]
    [SerializeField] private bool isTeleporter = false;
    private bool hasTeleported = true;
    [SerializeField] private Transform playerTeleportPosition = null;


    protected override void Awake()
    {
        base.Awake();
        //transform.GetChild(Random.Range(0, transform.childCount - 1)).gameObject.SetActive(true);
    }

    

    protected override void OnDrawGizmos () {
        base.OnDrawGizmos();

        Gizmos.color = meleeHurtZoneGizmoColor;
        Gizmos.DrawWireSphere(this.transform.position + Vector3.up, meleeHurtZone);
    }

    protected override void AttackState () {
        attackTimer += Time.deltaTime;

        Transform player = null;
        if (OverLap(meleeHurtZone, out player)) {
            meshAgentComponent.isStopped = true;
            Vector3 vectorLookPos= new Vector3(targetDetected.position.x, transform.position.y,
                targetDetected.position.z);
            transform.LookAt(vectorLookPos);

            if (attackCooldown <= attackTimer) {
                attackTimer -= attackCooldown;
                animationHandler.TriggerAttackAnimation(true);
                player.GetComponent<HealthComponent>().TakeDamage(damageToDeal);
            } else {
                //animationHandler.TriggerIdleAnimation();
            }

        } else {
            meshAgentComponent.isStopped = false;
            meshAgentComponent.SetDestination(targetDetected.position);
            animationHandler.TriggerRunRangedAnimation();
            isAttacking = false;
            meshAgentComponent.speed = runningSpeed;
            tragetLastPosition = targetDetected.position;
            if (isTeleporter)
            {
                if (hasTeleported)
                {
                    transform.position = playerTeleportPosition.position;
                    spawnVfxOn_REF.SpawnVFX(this.transform, spawnVFX.GetRandomFromList());
                    hasTeleported = false;
                }
            }
        }

        if (OverLap(agroRange) == false) {
            currentState = AIState.Searching;
            targetDetected = null;
            if (isTeleporter)
            {
                hasTeleported = true;
            }
        }
    }

}
