using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(AIAnimationHandler))]
public abstract class Enemy : MonoBehaviour {

    protected AIAnimationHandler animationHandler = null;

    [Space]
    [Header("Speeds")]
    [SerializeField] protected float walkingSpeed = 1f;
    [SerializeField] protected float runningSpeed = 2.5f;

    public enum AIState {
        OverWatch,
        Patrol,
        Attack,
        Searching,
        GoingToPoint,
    }

    // State
    [SerializeField] protected AIState currentState;

    //Components
    protected NavMeshAgent meshAgentComponent = null;

    // Patrol Settigns
    [SerializeField] protected List<Vector3> wayPoints = new List<Vector3>();
    [SerializeField] protected int currentWayPointIndex = 0;

    // Enemy Stats
    [Header("States Stats")]
    [SerializeField] protected float detectionRange = 10f;
    [SerializeField] protected Color detectionRangeGizmoColor = Color.blue;
    [SerializeField] protected Transform tragetDetected = null;

    // Searching state variables
    protected Vector3? tragetLastPosition = null;
    protected bool isAtTragetLastPosition = false;
    protected bool isFirstTimeToSearch = true;
    [Space]
    [SerializeField] protected float searchingTime = 2f;

    [Space]
    [SerializeField] protected float agroRange = 20f;
    [SerializeField] protected Color agroRangeGizmoColor = Color.cyan;

    [Space]
    [Header("VFX Settings")]
    [SerializeField] protected SpawnVfxOnZombie spawnVfxOn_REF;
    [SerializeField] protected GameObject deathVFX;
    [SerializeField] protected List<GameObject> gameObjectVFX = new List<GameObject>();

    [Space]
    [Header("Zombies & Animals Def")]
    [SerializeField] protected bool isAnimal = false;
    [SerializeField] protected bool isZombie = true;



    private float timer = 0f;
    private float waitTime = 4f;
    
    // Going To Point State 
    protected Vector3 goToPoint;

    protected virtual void Awake () {
        meshAgentComponent = GetComponent<NavMeshAgent>();
        animationHandler = GetComponent<AIAnimationHandler>();

    }

    protected virtual void OnEnable()
    {
        if(isZombie == true)
        {
            spawnVfxOn_REF.SpawnVFX(new Vector3(transform.position.x, 0, transform.position.z),
                gameObjectVFX.GetRandomFromList());
        }
    }

    protected virtual void OnDisable()
    {
        if(isZombie == true)
        {
            spawnVfxOn_REF.SpawnVFX(new Vector3(transform.position.x, 0, transform.position.z),
                deathVFX);
        }
    }

    private void Start()
    {
        if (wayPoints.Count < 2)
        {
            int spawnPointsCount = Random.Range(3, 10);
            float range = agroRange * 2;

            for (int i = 0; i < spawnPointsCount; i++)
            {
                float z = Random.Range(-range, range);
                float x = Random.Range(-range, range);

                var pointToAdd = new Vector3(
                    transform.position.x + x,
                    transform.position.y,
                    transform.position.z + z
                    );

                wayPoints.Add(pointToAdd);
            }
        }

        for (int i = 0; i < wayPoints.Count; i++)
        {
            if (NavMesh.SamplePosition(wayPoints[i], out NavMeshHit hit, 200f, 0))
            {
                wayPoints[i] = hit.position;
            }
        }
    }

    public void Goto (Vector3 goToPoint)
    {
        this.goToPoint = goToPoint;
        currentState = AIState.GoingToPoint;
        print("&");
    }

    protected virtual void Update () => StateCheck();

    protected virtual void StateCheck () {
        switch (currentState) {
            case AIState.OverWatch: OverWatch(); break;
            case AIState.Attack: AttackState(); break;
            case AIState.Searching: Searching(); break;
            case AIState.Patrol: Patrol(); break;
            case AIState.GoingToPoint: GoToPoint(); break;
        }
    }

    protected virtual void GoToPoint ()
    {
        meshAgentComponent.SetDestination(goToPoint);
        animationHandler.TriggerRunAnimation();
        meshAgentComponent.speed = runningSpeed;
        meshAgentComponent.isStopped = false;

        if (OverLap(agroRange, out tragetDetected))
        {
            currentState = AIState.Attack;
        }
    }

    protected bool OverLap (float radius, string tagtoCheck = "Player") {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, radius);

        if (hits != null) {
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].gameObject.tag == tagtoCheck) {
                    return true;
                }
            }
        }

        return false;
    }

    protected bool OverLap (float radius, out Transform hit, string tagtoCheck = "Player") {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, radius);

        if (hits != null) {
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].gameObject.tag == tagtoCheck) {
                    hit = hits[i].transform;
                    return true;
                }
            }
        }

        hit = null;
        return false;
    }

    protected bool CheckIfTargetInRange (string targetTag = "Player") {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, detectionRange);

        if (hits != null) {
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].gameObject.tag == targetTag) {
                    tragetDetected = hits[i].transform;
                    return true;
                }
            }
        }

        tragetDetected = null;
        return false;
    }

    protected virtual void OnDrawGizmos () {
        Gizmos.color = detectionRangeGizmoColor;
        Gizmos.DrawWireSphere(this.transform.position, detectionRange);

        Gizmos.color = agroRangeGizmoColor;
        Gizmos.DrawWireSphere(this.transform.position, agroRange);
    }

    protected IEnumerator SearchingRoutine () {
        yield return new WaitForSeconds((float)searchingTime);

        if (wayPoints.Count == 0) {
            currentState = AIState.OverWatch;
        } else {
            currentState = AIState.Patrol;
        }

        isFirstTimeToSearch = true;
    }

    protected abstract void AttackState();

    protected virtual void Patrol () {
        if(wayPoints.Count == 0)
        {
            currentState = AIState.OverWatch;
        }

        meshAgentComponent.isStopped = false;
        meshAgentComponent.SetDestination(wayPoints[currentWayPointIndex]);

        meshAgentComponent.speed = walkingSpeed;

        if (meshAgentComponent.hasPath == false) {
            if (meshAgentComponent.pathPending == false) {
                animationHandler.TriggerIdleAnimation();
                currentWayPointIndex++;
                currentWayPointIndex %= wayPoints.Count;
                currentState = AIState.OverWatch;
            }
        } else {
            animationHandler.TriggerWalkAnimation();
        }

        if (CheckIfTargetInRange()) {
            currentState = AIState.Attack;
        }
    }

    protected virtual void OverWatch () {
        meshAgentComponent.isStopped = true;
        animationHandler.TriggerIdleAnimation();

        if (OverLap(agroRange, out tragetDetected)) {
            currentState = AIState.Attack;
        }
        if (wayPoints.Count != 0)
        {
            if (timer >= waitTime)
            {
                timer = 0f;
                currentState = AIState.Patrol;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    protected virtual void Searching () {

        if (timer >= waitTime)
        {
            timer = 0f;
            currentState = AIState.Patrol;
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (isAtTragetLastPosition == false) {
            meshAgentComponent.SetDestination((Vector3)tragetLastPosition);
            meshAgentComponent.isStopped = false;
        }

        if (meshAgentComponent.hasPath == false) {
            isAtTragetLastPosition = true;
            if (isFirstTimeToSearch) {
                isFirstTimeToSearch = false;
                StartCoroutine(SearchingRoutine());
            }
        }

        if (CheckIfTargetInRange()) {
            currentState = AIState.Attack;
            isAtTragetLastPosition = false;
            isFirstTimeToSearch = true;
            StopCoroutine(SearchingRoutine());
        }
    }
}
