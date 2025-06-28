using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float chaseDistance = 8f;
    public float stopChaseDistance = 15f;
    public float waitTimeAtPoint = 2f;

    private NavMeshAgent agent;
    private Animator anim;
    private Transform player;
    private int currentPointIndex = 0;
    private float waitTimer = 0f;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (patrolPoints.Length > 0)
            GoToNextPoint();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            if (!isChasing)
            {
                isChasing = true;
                Debug.Log("Chasing player");
            }

            agent.SetDestination(player.position);
            anim.SetBool("isWalking", true);
        }
        else if (isChasing && distanceToPlayer > stopChaseDistance)
        {
            isChasing = false;
            waitTimer = 0f;
            Debug.Log("Stopped chasing, returning to patrol");
            GoToNextPoint();
        }

        if (!isChasing)
        {
            // Check if the agent has reached its patrol point
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                anim.SetBool("isWalking", false);
                waitTimer += Time.deltaTime;

                if (waitTimer >= waitTimeAtPoint)
                {
                    GoToNextPoint();
                    waitTimer = 0f;
                }
            }
            else
            {
                anim.SetBool("isWalking", true);
            }
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentPointIndex].position);
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        anim.SetBool("isWalking", true);
    }
}
