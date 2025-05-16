using UnityEngine;
using static EnemyAI;

public class EnemyBaseController : MonoBehaviour
{
    public EnemyData enemyData;
    private float currentHealth;
    protected EnemyAI enemyAI;

    [HideInInspector]
    protected Transform player;
    /*  [Header("position")]
      [SerializeField] protected Vector3 spawnOffset;*/
    [Header("Attack")]
    protected float lastAttackTime;
    protected float attackCooldown;
    protected float stopDistance;
    [Header("Pacing")]
    [SerializeField] private float patrolRadius = 3f;
    [SerializeField] private float detectRange = 1.0f;
    private Vector3[] patrolOffsets;
    // private int currentPatrolIndex = 0;
    private Vector3 pacingStartPos;
    private Vector3 pacingTargetPos;
    [Header("Die")]
    [SerializeField] protected float dropChance = 0.1f;
    [SerializeField] protected GameObject dropPerfab;
    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }
    protected virtual void Start()
    {
        SetEnemyValue();
        pacingStartPos = transform.position;
        pacingTargetPos = pacingStartPos + Vector3.right * enemyData.pacingRange;
        SetPacingLocation();

    }
    protected virtual void SetEnemyValue()
    {
        currentHealth = enemyData.maxHealth;
        //other value need been set too if during the play been changed......
    }

    protected virtual void Update()
    {
        switch (enemyAI.currentState)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.Pacing:
                Patrol();
                DetectPlayer();
                break;

            case EnemyState.MovingToPlayer:
                MoveToPlayer();
                break;

            case EnemyState.Attack:
                AttackPlayer();
                break;

        }
    }
    protected Vector3 GetTargetPosition()
    {
        Vector3 StopPosition;
        if (player.position.x < transform.position.x)
        {
            StopPosition = new Vector3(1, 1, 1);
        }
        else if (player.position.x >= transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        StopPosition = player.position + new Vector3(2, 0, 0);
        return StopPosition;
    }

    private void SetPacingLocation()
    {
        patrolOffsets = new Vector3[]
            {
                Vector3.left * patrolRadius,
                Vector3.right * patrolRadius,
                (Vector3.left + Vector3.up).normalized * patrolRadius,
                (Vector3.right + Vector3.up).normalized * patrolRadius,
                (Vector3.left + Vector3.down).normalized * patrolRadius,
                (Vector3.right + Vector3.down).normalized * patrolRadius
            };
    }
    void FlipTowardsPlayer()
    {
        if (player == null) return;


        if (player.position.x < transform.position.x)
        {

            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void Patrol()
    {
        if (player == null) return;
        // Vector3 targetPoint = player.position + patrolOffsets[currentPatrolIndex];
        Vector3 targetPoint = player.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, enemyData.moveSpeed * Time.deltaTime);

        /*   if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
           {
               currentPatrolIndex = (currentPatrolIndex + 1) % patrolOffsets.Length;
           }*/
    }
    void DetectPlayer()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < detectRange) // detection range
        {
            enemyAI.currentState = EnemyState.MovingToPlayer;
        }
    }

    protected virtual void MoveToPlayer()
    {
        FlipTowardsPlayer();
    }
    protected virtual void AttackPlayer()
    {
        FlipTowardsPlayer();
    }
    private void OnDisableAttack()
    {
        if (EnemyAttackManager.Instance != null)
            EnemyAttackManager.Instance.StopAttack(this.gameObject);
    }

    protected virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}