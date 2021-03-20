using UnityEngine;
using System.Collections;
using UnityEngine.AI;


public class EnemyBomber : Enemy
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
            transform.LookAt(targetDetected);
            meshAgentComponent.isStopped = true;
            if (hasExplosed == false)
            {
                ExploseHimSelf();
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

    private void ExploseHimSelf()
    {
        Vector3 explosionPos = transform.position;
        this.spawnVfxOn_REF.SpawnVFX(this.transform,
            this.deathVFX.GetRandomFromList(), targetDetected);
        transform.gameObject.SetActive(false);

        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            
            if ((hit.CompareTag("Zombie")) && hit != null)
            {
                hit.transform.GetComponent<Enemy>().Disable();
                hit.gameObject.SetActive(false);
            }
        }
    }

    
}
