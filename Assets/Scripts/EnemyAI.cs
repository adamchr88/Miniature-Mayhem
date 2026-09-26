using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;
    public GameManager gameManager;

    [Header("AI Settings")]
    public float detectionRange = 6f;
    public float attackRange = 1.2f;
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 3.5f;

    private NavMeshAgent agent;
    private Animator animator;
    private int patrolIndex = 0;
    private bool isChasing = false;

    public TextMeshProUGUI warningText;

    public AudioManager audioManager;
    private bool alertPlayed = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (agent == null)
        {
            Debug.Log("Spider has no NavMeshAgent");
            return;
        }

        agent.isStopped = false;
        agent.updatePosition = true;

        // Stops the spider root rotating/flipping weirdly
        agent.updateRotation = false;

        if (!agent.isOnNavMesh)
        {
            Debug.Log("Spider is NOT on NavMesh");
            return;
        }

        Debug.Log("Spider is on NavMesh");

        if (patrolPoints.Length > 0)
        {
            agent.speed = patrolSpeed;
            SetCurrentPatrolPoint();
        }
        else
        {
            Debug.Log("No patrol points assigned");
        }
    }

    void Update()
    {
        if (player == null || agent == null || !agent.isOnNavMesh)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (gameManager != null)
            {
                gameManager.GameOver();
            }

            return;
        }

        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        FaceMovementDirection();
        UpdateAnimation();
    }

    void Patrol()
    {
        isChasing = false;
        agent.speed = patrolSpeed;

        if (patrolPoints.Length == 0)
        {
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            patrolIndex++;

            if (patrolIndex >= patrolPoints.Length)
            {
                patrolIndex = 0;
            }

            SetCurrentPatrolPoint();
        }

        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }

        alertPlayed = false;
    }

    void ChasePlayer()
    {
        isChasing = true;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        if (warningText != null)
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "Spider is chasing you!";
        }

        if (!alertPlayed && audioManager != null)
        {
            audioManager.PlaySpiderAlert();
            alertPlayed = true;
        }
    }

    void SetCurrentPatrolPoint()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        bool pathSet = agent.SetDestination(patrolPoints[patrolIndex].position);

        Debug.Log("Spider moving to: " + patrolPoints[patrolIndex].name + " | Path set: " + pathSet);
    }

    void FaceMovementDirection()
    {
        Vector3 moveDirection = agent.velocity;
        moveDirection.y = 0f;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 8f);
        }
    }

    void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
        animator.SetBool("IsChasing", isChasing);
    }


}