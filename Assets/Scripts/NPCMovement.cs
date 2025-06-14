using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    public Transform player;
    public Transform[] targets;
    public float[] idleTime;
    public int targetIndex = 0;
    public float countUp = 0.0f;
    public float patrolDistance = 10.0f;
    public float chaseDistance = 5.0f;
    public float attackDistance = 1.5f;

    enum Mode { Patrol, Chase, Attack };
    Mode npcMode = Mode.Patrol;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector3.Distance(player.position, transform.position);
        if (distToPlayer < attackDistance && npcMode == Mode.Chase)
        {
            AttackPlayer();
        }
        else if (distToPlayer < chaseDistance && npcMode == Mode.Patrol)
        {
            ChasePlayer();
        }
        else if (distToPlayer < patrolDistance && npcMode == Mode.Chase)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        Debug.Log(distToPlayer);

    }

    void AttackPlayer()
    {
        agent.destination = transform.position;
        npcMode = Mode.Attack;
    }

    void ChasePlayer()
    {
        agent.destination = player.position;
        npcMode = Mode.Chase;
    }

    void Patrol()
    {
        npcMode = Mode.Patrol;
        float distToTarget = Vector3.Distance(targets[targetIndex].position, transform.position);
        agent.destination = targets[targetIndex].position;
        if (distToTarget < 0.7f)
        {
            anim.SetBool("isWalking", false);
            countUp += Time.deltaTime;
            if (countUp > idleTime[targetIndex])
            {
                if (targetIndex < targets.Length - 1)
                {
                    targetIndex++;
                }
                else
                {
                    targetIndex = 0;
                }
                countUp = 0.0f;
            }
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }
}