using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float walkRange = 10f;
    public float runRange = 5f;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= walkRange)
        {
            // Chase player
            agent.SetDestination(player.position);

            if (distance <= runRange)
            {
                // Run
                agent.speed = runSpeed;
                animator.SetFloat("Speed", runSpeed);
            }
            else
            {
                // Walk
                agent.speed = walkSpeed;
                animator.SetFloat("Speed", walkSpeed);
            }
        }
        else
        {
            // Idle
            agent.ResetPath();
            animator.SetFloat("Speed", 0f);
        }
    }
}
