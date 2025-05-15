using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public enum EnemyState
    {
        Idle,
        Pacing,
        MovingToPlayer,
        Attack,
    }

    [HideInInspector]
    public EnemyState currentState = EnemyState.Idle;
    private EnemyData enemyData;
    private Transform player;

    [Header("Attack")]
    private float lastAttackTime;
    private float attackCooldown;

    [Header("Pacing")]
    [SerializeField] private float patrolRadius = 3f;
    [SerializeField] private float avoidPlayerRadius = 1.0f;
    [SerializeField] private float detectRange = 1.0f;
    private Vector3[] patrolOffsets;
    private int currentPatrolIndex = 0;
    private Vector3 pacingStartPos;
    private Vector3 pacingTargetPos;

    private void Awake()
    {
        EnemyController controller = this.GetComponent<EnemyController>();
        if (controller != null)
        {
            enemyData = controller.enemyData;
        }
        else
        {
            Debug.Log("controller null");
        }
    }
    void Start()
    {
        pacingStartPos = transform.position;
        pacingTargetPos = pacingStartPos + Vector3.right * enemyData.pacingRange;


        player = FindAnyObjectByType<PlayerMovement>().transform;

        SetPacingLocation();
        currentState = EnemyState.Pacing;
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
    void Update()
    {
        // Debug.Log(this.name + "current state" + currentState);
        switch (currentState)
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

    void Patrol()
    {
        if (player == null) return;

        Vector3 targetPoint = player.position + patrolOffsets[currentPatrolIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, enemyData.moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolOffsets.Length;
        }
    }
    void DetectPlayer()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < detectRange) // detection range
        {
            currentState = EnemyState.MovingToPlayer;
        }
    }

    void MoveToPlayer()
    {
        Debug.Log("move to player");
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > enemyData.attackRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * enemyData.moveSpeed * Time.deltaTime;
        }

        else
        {
            bool canAttack = EnemyAttackManager.Instance.TryRequestAttack(this);
            if (canAttack)
            {
                currentState = EnemyState.Attack;
                lastAttackTime = Time.time;
            }
            else
            {

                currentState = EnemyState.Pacing;
            }
        }
    }
    void AttackPlayer()
    {
        Debug.Log("Enemy" + this.name + " attacks player!");
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // if player run move to the player again
        if (dist > enemyData.attackRange)
        {
            currentState = EnemyState.MovingToPlayer;
            return;
        }

        //even within the range still move to player if player location changed
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * (enemyData.moveSpeed * 0.5f) * Time.deltaTime;

        // keep attack
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            //player process the damage
            lastAttackTime = Time.time;
        }
    }
    private void OnDisableAttack()
    {
        if (EnemyAttackManager.Instance != null)
            EnemyAttackManager.Instance.StopAttack(this);
    }



}
