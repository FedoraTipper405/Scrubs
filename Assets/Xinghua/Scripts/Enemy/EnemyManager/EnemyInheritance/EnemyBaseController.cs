using UnityEngine;
using UnityEngine.Events;
using static EnemyAI;

public class EnemyBaseController : MonoBehaviour
{
    public EnemyData enemyData;
    private float currentHealth;
    protected EnemyAI enemyAI;

    [HideInInspector]
    protected Transform player;
    [Header("general")]
    protected float distToPlayer;
    protected float stopDistance = 2f;
    [Header("Attack")]
    protected float lastAttackTime;
 

    [Header("Pacing")]
    [SerializeField] private float patrolRadius = 1f;
    private Vector3[] patrolOffsets;
    private int currentPatrolIndex = 0;
    [SerializeField] protected float pacingDistance = 2f;
    Vector3 stopOffset;

    [Header("Die")]
    [SerializeField] protected float dropChance = 0.1f;
    [SerializeField] protected GameObject dropPerfab;

    protected Animator animator;
    private KnockBack knockBack;

    protected virtual void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        knockBack = GetComponent<KnockBack>();
        SetEnemyValue();
        SetPacingLocation();
    }

    protected virtual void SetEnemyValue()
    {
        currentHealth = enemyData.maxHealth;
        //other value need been set too if during the play been changed......
    }

    protected virtual void Update()
    {
        CheckDistance();
        switch (enemyAI.currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Pacing:
                Peacing();
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break;
        }
        FlipTowardsPlayer();
        EnemySpawnManager.Instance.CheckEnemyNumberInTheScene();
    }

    private void SetPacingLocation()
    {
        patrolOffsets = new Vector3[]
            {
                Vector3.up * patrolRadius,
                Vector3.down * patrolRadius,
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
            stopDistance = pacingDistance;
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
    private void CheckDistance()
    {
     
        var currentDistance = Vector3.Distance(transform.position, player.transform.position);
       // Debug.Log("check distance:"+ currentDistance +"arrange:"+  enemyData.attackRange);
        if (currentDistance <= enemyData.attackRange)
        {
            enemyAI.SetEnemyState(EnemyState.Attack);
        }
    }

    protected void Peacing()
    {
        animator.SetBool("isMoving", true);

        if (player == null) return;
        Vector3 targetPoint = GetStopPosition() + patrolOffsets[currentPatrolIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, enemyData.moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolOffsets.Length;
        }
    }

    
    protected void MoveToPlayer()
    {
        animator.SetBool("isMoving", true);
        distToPlayer = Vector3.Distance(transform.position, GetStopPosition());
        Vector3 dir = (GetStopPosition() - transform.position).normalized;
        transform.position += dir * enemyData.moveSpeed * Time.deltaTime;
    }

    protected virtual void AttackPlayer()
    {

    }

    private void OnDisableAttack()
    {
        if (EnemyAttackManager.Instance != null)
            EnemyAttackManager.Instance.StopAttack(this.gameObject);
    }
    protected bool isDead = false;
    public virtual void TakeDamage(int amount,GameObject sender)
    {
        if(knockBack !=null)
        {
            knockBack.PlayKnockBackFeedBack(sender);

        }
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        if (currentHealth > 0)
        {

            //play hit animation
           
        }
        else
        {
            if (isDead) return;
            Die();
        }
    }

    protected virtual void Die()
    {
        animator.SetTrigger("isDeath");
        isDead = true;
        if (enemyData.canDrop) return;

    }

    public void OnDeath()
    {
        if (enemyData.canDrop && Random.value < 0.6f)//this can be 0.1f； 0.6 is for test
        {
            GameObject item = Instantiate(dropPerfab, transform.position, Quaternion.identity);
            item.SetActive(true);
            if (item != null)
            {
                Destroy(item, 8f);
            };
        }


        Destroy(gameObject);
        EnemySpawnManager.Instance.enemiesInTheScene.Remove(gameObject);
        EnemyAttackManager.Instance.currentAttackers.Remove(gameObject);
    }


}