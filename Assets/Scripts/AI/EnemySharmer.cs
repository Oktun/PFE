using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySharmer : Enemy
{

    [Space]
    [SerializeField] private float meleeHurtZone = 2f;
    [SerializeField] private Color meleeHurtZoneGizmoColor = Color.red;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackTimer = 0f;
    bool isAttacking = false;
    [SerializeField] private int attackDamage;

    [Header("SpawnWalkersSettings")]
    [SerializeField] private List<GameObject> zombieList;
    [SerializeField] private float timeBetweenSpawn = 10f;
    [SerializeField] private Transform[] transformsSpawn;
    private float counterSpawn = 0f;
    

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
            meshAgentComponent.isStopped = true;
            transform.LookAt(tragetDetected);

            if (attackCooldown <= attackTimer)
            {
                attackTimer -= attackCooldown;
                animationHandler.TriggerAttackAnimation(true);
                //player.GetComponent<PlayerHealth>().healthSystem.Damage(attackDamage, 1);
            }
            else
            {
                animationHandler.TriggerIdleAnimation();
            }

        }
        else
        {
            meshAgentComponent.isStopped = false;
            meshAgentComponent.SetDestination(tragetDetected.position);
            animationHandler.TriggerRunRangedAnimation();
            isAttacking = false;
            meshAgentComponent.speed = runningSpeed;
            tragetLastPosition = tragetDetected.position;
            //Spawn Walkers
            SpawnWalkers();

        }

        if (OverLap(agroRange) == false)
        {
            currentState = AIState.Searching;
            tragetDetected = null;
        }
    }

    private void SpawnWalkers()
    {
        if(counterSpawn>= timeBetweenSpawn)
        {
            Instantiate(zombieList.GetRandomFromList(), transformsSpawn[Random.Range(0, 3)].position, Quaternion.identity);
            counterSpawn = 0f;
        }
        else
        {
            counterSpawn += Time.deltaTime;
        }
    }

}
