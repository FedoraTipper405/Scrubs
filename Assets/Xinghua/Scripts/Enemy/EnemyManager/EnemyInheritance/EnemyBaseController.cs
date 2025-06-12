using System;
using System.Collections;
using UnityEngine;
using static EnemyAI;

public class EnemyBaseController : MonoBehaviour
{
    public EnemyData enemyData;
    protected float currentHealth;
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
    protected Vector3[] patrolOffsets;
    private int currentPatrolIndex = 0;

    Vector3 stopOffset;

    [Header("Die")]
    [SerializeField] protected float dropChance = 0.8f;
    [SerializeField] protected GameObject dropPerfab;
    protected bool isDead;
    protected Animator animator;
    private KnockBack knockBack;
    [Header("Die")]
    private float takeDamageCooldown = 0.2f;
    private float lastDamageTime = -Mathf.Infinity;
    protected SpriteRenderer enemyRenderer;

    public event Action<GameObject,float> OnKnockBack;
    private void Awake()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
    }
    protected virtual void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        knockBack = GetComponentInChildren<KnockBack>();
        SetEnemyValue();

       
        
       
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

            switch (enemyAI.currentState)
            {
                case EnemyState.Idle:
                    Idle();
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

    protected virtual void SetPacingLocation()
    {

    }

    protected virtual void FlipTowardsPlayer()
    {
        if (player == null) return;

        if(enemyRenderer != null)
        {
            if (player.position.x < transform.position.x)
            {
                enemyRenderer.flipX = false;
            }
            else
            {
                enemyRenderer.flipX = true;
            }
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
    protected  void Idle()
    {
        animator.SetBool("isIdle",true);
        StartCoroutine(EndIdle());
    }

    private IEnumerator EndIdle()
    {
        yield return new WaitForSeconds(1f);
        enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);
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

    public virtual void TakeDamage(int amount, float knockBack ,GameObject sender)
    {
       // OnKnockBack?.Invoke(player.gameObject,knockBack);
        OnKnockBack?.Invoke(player.gameObject, 12f);
        if (Time.time - lastDamageTime < takeDamageCooldown)
            return;
        lastDamageTime = Time.time;
        if (currentHealth > amount)
        {
            currentHealth -= amount;
            enemyAI.SetEnemyState(EnemyState.Idle);
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySFX("PlayerKick", 1f);
            }
            else
            {
                Debug.Log("sound manager is null");
            }
        }
        else
        {
            currentHealth = 0;
            Die();
        }
     
        Health visuakHealth = GetComponentInChildren<Health>();
        if (visuakHealth != null)
        {
            visuakHealth.UpdateHealthUI(currentHealth, enemyData.maxHealth);
        }
        if (enemyRenderer != null)
        {
            enemyRenderer.color = new Color(1f, 0f, 0f, 1f);//red
            StartCoroutine(EndFlash());
        }

    }

    private IEnumerator EndFlash()
    {
        yield return new WaitForSeconds(0.25f);

        enemyRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    protected virtual void Die()
    {
        animator.SetTrigger("isDeath");
        isDead = true;
    }

    protected virtual void OnDeath()
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