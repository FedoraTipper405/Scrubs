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

    [Header("Drop")]
    [SerializeField] protected float dropHealItemChance = 0.8f;
    [SerializeField] protected GameObject dropItemPrefab;
    [SerializeField] protected float dropMoneyChance = 0.8f;
    [SerializeField] protected GameObject dropMoneyPrefab;

    protected bool isDead;
    protected Animator animator;
    private KnockBack knockBack;
    [Header("Die")]
    private float takeDamageCooldown = 0.2f;
    private float lastDamageTime = -Mathf.Infinity;
    protected SpriteRenderer enemyRenderer;
    private Vector3 personalOffset;
    public event Action<GameObject, float> OnKnockBack;
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
        personalOffset = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, 0);
    }

    protected virtual void SetEnemyValue()
    {
        currentHealth = enemyData.maxHealth;
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

    }
    public float avoidStrength = 2f;
    void FixedUpdate()
    {
        if (player == null) return;
        MoveToPlayer();
    }
    protected virtual void SetPacingLocation()
    {
        patrolOffsets = new Vector3[]
            {
                Vector3.up * enemyData.pacingRadius,
                Vector3.down *enemyData.pacingRadius,
           /*     Vector3.left *enemyData.pacingRadius,
                 Vector3.right * enemyData.pacingRadius,*/
            };
    }

    protected virtual void FlipTowardsPlayer()
    {
        if (player == null) return;
        Vector3 scale = transform.localScale;
        if (enemyRenderer != null)
        {
            if (player.position.x < transform.position.x)
            {
                scale.x = 1f;
                transform.localScale = scale;
            }
            else
            {
                scale.x = -1f;
                transform.localScale = scale;
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
        return player.position + stopOffset +personalOffset;
    }
    protected virtual void Idle()
    {
        animator.SetBool("isIdle", true);
        StartCoroutine(EndIdle());
    }

    private IEnumerator EndIdle()
    {
        yield return new WaitForSeconds(0.1f);
        enemyAI.SetEnemyState(EnemyAI.EnemyState.Pacing);
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
        if (enemyAI.currentState == EnemyState.Attack || enemyAI.currentState == EnemyState.Pacing)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            if (IsArrivedTargetPosition() == false)
            {
                transform.position += dir * enemyData.moveSpeed * Time.deltaTime;

                Vector2 moveDir = (player.position - transform.position).normalized;
                Vector2 avoidDir = CalculateAvoidance();

                Vector2 finalDir = (moveDir + avoidDir * avoidStrength).normalized;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints2D.None;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.MovePosition(rb.position + finalDir * enemyData.moveSpeed * Time.fixedDeltaTime);
                }
            }
        }
    }
    private Vector2 CalculateAvoidance()
    {
        Vector2 avoid = Vector2.zero;

        foreach (var other in EnemyAttackManager.Instance.potentialAttackers)
        {
            if (other == this) continue;

            Vector2 diff = (Vector2)(transform.position - other.transform.position);
            float distance = diff.magnitude;

            if (distance < enemyData.avoidDistance && distance > 0.01f)
            {
                avoid += diff.normalized / (distance * distance);
            }
        }

        return avoid;
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

    public virtual void TakeDamage(int amount, float knockBack, GameObject sender)
    {
       
        OnKnockBack?.Invoke(player.gameObject, knockBack);
        if (Time.time - lastDamageTime < takeDamageCooldown)
            return;
        lastDamageTime = Time.time;

        if (currentHealth > amount)
        {
            currentHealth -= amount;
            enemyAI.SetEnemyState(EnemyAI.EnemyState.Idle);
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
        EnemyAttackManager.Instance.RemoveAttacker(gameObject);
        EnemyAttackManager.Instance.potentialAttackers.Remove(gameObject);
    }

    protected virtual void OnDeath()
    {
        EnemyTriggerManager.Instance.HandleEnemyChangeWithCamera(false, true);
       
        Destroy(gameObject);
        

        if (enemyData.canDrop && UnityEngine.Random.value < enemyData.dropHealItemChance)
        {
            GameObject item = Instantiate(dropItemPrefab, transform.position, Quaternion.identity);


            item.SetActive(true);
            if (item != null)
            {
                Destroy(item, 8f);
            };
        }
        float offsetX = 0.6f;
        if (enemyData.canDrop && UnityEngine.Random.value < enemyData.dropMoneyChance)
        {
            GameObject money = Instantiate(dropMoneyPrefab, transform.position + new Vector3(offsetX, 0, 0), Quaternion.identity);

            money.SetActive(true);
            if (money != null)
            {
                Destroy(money, 8f);
            };
        }
    }


}