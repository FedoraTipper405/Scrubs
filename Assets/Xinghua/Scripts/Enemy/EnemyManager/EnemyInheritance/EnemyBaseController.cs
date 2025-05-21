using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static EnemyAI;

public class EnemyBaseController : MonoBehaviour
{
    public EnemyData enemyData;
    private float currentHealth;
    private Vector3 patrolCenter;
    protected EnemyAI enemyAI;

    [HideInInspector]
    protected Transform player;
    [Header("general")]
    protected float distToPlayer;
    protected float stopDistance;
    [Header("Attack")]
    protected float lastAttackTime;


    [Header("Pacing")]
    private Vector3[] patrolOffsets;
    private int currentPatrolIndex = 0;

    Vector3 stopOffset;

    [Header("Die")]
    [SerializeField] protected float dropChance = 0.8f;
    [SerializeField] protected GameObject dropPerfab;
    protected bool isDead;
    protected Animator animator;
    private KnockBack knockBack;


    protected virtual void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        knockBack = GetComponentInChildren<KnockBack>();
        SetEnemyValue();
        SetPacingLocation();
        isDead = false;
    }

    protected virtual void SetEnemyValue()
    {
        currentHealth = enemyData.maxHealth;
        //other value need been set too if during the play been changed......
        patrolCenter = GetStopPosition();
    }

    protected virtual void Update()
    {
        if (enemyAI != null)
        {
            // Debug.Log("enemyAI" + enemyAI);
            switch (enemyAI.currentState)
            {
                case EnemyState.Idle:
                    break;
                case EnemyState.Pacing:
                    Pacing();
                    break;
                case EnemyState.Attack:
                    AttackPlayer();
                    break;
            }
        }
        FlipTowardsPlayer();
        // EnemySpawnTrigger.Instance.CheckEnemyNumberInTheScene();
    }

    private void SetPacingLocation()
    {
        patrolOffsets = new Vector3[]
            {
                Vector3.up * enemyData.pacingRadius,
                Vector3.down *enemyData.pacingRadius,

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
    protected Vector3 GetStopPosition()//if attack stopDistance 1f;pacing 2f for test
    {
        if (enemyAI.currentState == EnemyAI.EnemyState.Attack)
        {
            stopDistance = enemyData.attackRange;//1f need been made para
        }
        else
        {
            stopDistance = enemyData.pacingRadius;
        }

        if (player.position.x < transform.position.x)
        {

            stopOffset = new Vector3(stopDistance, 0, 0);
        }
        else if (player.position.x >= transform.position.x)
        {
            stopOffset = new Vector3(-stopDistance, 0, 0);
        }

        return player.position + stopOffset;
    }


    protected virtual void Pacing()
    {

    }
    protected void PatrolAround()
    {
        Vector3 targetPoint = patrolCenter + patrolOffsets[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, enemyData.moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint) <= 0.3f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolOffsets.Length;

        }
    }

    protected void MoveToPlayer()
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", true);
        }

        Vector3 dir = (player.position - transform.position).normalized;

        if (IsArrivedTargetPosition() == false)
        {
            transform.position += dir * enemyData.moveSpeed * Time.deltaTime;
        }

    }

    protected bool IsArrivedTargetPosition()
    {
        distToPlayer = Vector3.Distance(transform.position, GetStopPosition());
        if (distToPlayer > stopDistance)
        {
            return false;
        }
        return true;
    }
    protected virtual void AttackPlayer()
    {

    }

    public virtual void TakeDamage(int amount, GameObject sender)
    {
        if (currentHealth > amount)
        {
            currentHealth -= amount;
        }
        else
        {
            currentHealth = 0;
            Die();
        }
        if (knockBack != null)
        {
            knockBack.PlayKnockBackFeedBack(sender);
        }

    
       
    }

    protected virtual void Die()
    {
        animator.SetTrigger("isDeath");
        isDead = true;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
        EnemyTriggerManager.Instance.HandleEnemyChange(gameObject);
        if (enemyData.canDrop && UnityEngine.Random.value < dropChance)
        {
            GameObject item = Instantiate(dropPerfab, transform.position, Quaternion.identity);
            item.SetActive(true);
            if (item != null)
            {
                Destroy(item, 8f);
            };
        }
    }


}