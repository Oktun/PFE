using UnityEngine;
using System.Collections;

public class MeleeEnemy : Enemy {

    [Space]
    [SerializeField] private float meleeHurtZone = 2f;
    [SerializeField] private Color meleeHurtZoneGizmoColor = Color.red;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackTimer = 0f;
    bool isAttacking = false;
    [SerializeField] private int attackDamage;

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
        Gizmos.DrawWireSphere(this.transform.position, meleeHurtZone);
    }

    protected override void AttackState () {
        attackTimer += Time.deltaTime;

        Transform player = null;
        if (OverLap(meleeHurtZone, out player)) {
            meshAgentComponent.isStopped = true;
            transform.LookAt(tragetDetected);

            if (attackCooldown <= attackTimer) {
                attackTimer -= attackCooldown;
                animationHandler.TriggerAttackAnimation(true);
                //player.GetComponent<PlayerHealth>().healthSystem.Damage(attackDamage, 1);
            } else {
                animationHandler.TriggerIdleAnimation();
            }

        } else {
            meshAgentComponent.isStopped = false;
            meshAgentComponent.SetDestination(tragetDetected.position);
            animationHandler.TriggerRunRangedAnimation();
            isAttacking = false;
            meshAgentComponent.speed = runningSpeed;
            tragetLastPosition = tragetDetected.position;
            if (isTeleporter)
            {
                if (hasTeleported)
                {
                    transform.position = playerTeleportPosition.position;
                    spawnVfxOn_REF.SpawnVFX(new Vector3(transform.position.x, 0, transform.position.z),
                        gameObjectVFX.GetRandomFromList());
                    hasTeleported = false;
                }
            }
        }

        if (OverLap(agroRange) == false) {
            currentState = AIState.Searching;
            tragetDetected = null;
            if (isTeleporter)
            {
                hasTeleported = true;
            }
        }
    }

}
