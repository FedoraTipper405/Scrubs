using System;
using System.Collections;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
    private float takeDamageCooldown =0.2f;
    private float lastDamageTime = -Mathf.Infinity;
  

    public event Action<GameObject> OnKnockBack;
  
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



        //Nathan Temp fix I hope

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void SetPacingLocation()
    {
       
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
        OnKnockBack?.Invoke(gameObject);
        if (Time.time - lastDamageTime < takeDamageCooldown)
            return;
        lastDamageTime = Time.time;

        if (currentHealth > amount)
        {
            currentHealth -= amount;
            //enemyAI.SetEnemyState(EnemyState.Idle);
           Health visuakHealth = GetComponentInChildren<Health>();
            if (visuakHealth != null)
            {
                visuakHealth.UpdateHealthUI(currentHealth,enemyData.maxHealth);
            }
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

        //event
        Debug.Log(this.name + "take damage:" + amount+"current health:" +currentHealth);
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