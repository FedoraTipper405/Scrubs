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
    public EnemyState currentState = EnemyState.Idle;

    private EnemyData enemyData;
    private Vector3 pacingStartPos;
    private Vector3 pacingTargetPos;
    private Transform player;
    private float lastAttackTime;
    private float attackCooldown;

    private int currentPatrolIndex = 0;
    private Vector3[] patrolOffsets;
    private float patrolRadius = 3f;
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


        player = FindAnyObjectByType<Target>().transform;

        patrolOffsets = new Vector3[]
 {
        Vector3.left * patrolRadius,
        Vector3.right * patrolRadius,
        (Vector3.left + Vector3.up).normalized * patrolRadius,
        (Vector3.right + Vector3.up).normalized * patrolRadius,
        (Vector3.left + Vector3.down).normalized * patrolRadius,
        (Vector3.right + Vector3.down).normalized * patrolRadius
 };
        currentState = EnemyState.Pacing;
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
               // DetectPlayer();
                break;

                /*    case EnemyState.MovingToPlayer:
                        MoveToPlayer();
                        break;


                    case EnemyState.Attack:
                        AttackPlayer();
                        break;
                 */
        }
    }

    void Patrol()
    {
        
        Debug.Log("patrol");
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

        if (dist < 1f) // detection range
        {
            currentState = EnemyState.MovingToPlayer;
        }
    }

    void MoveToPlayer()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > enemyData.attackRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * enemyData.moveSpeed * Time.deltaTime;
        }
        else
        {
            currentState = EnemyState.Attack;
        }
    }
    void AttackPlayer()
    {

        Debug.Log("Enemy attacks player!");


    }


}
