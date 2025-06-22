using UnityEngine;

public class EnemyCorporateTrash : EnemyBaseController
{
    private float pacingTimer = 0f;
    private float maxPacingTime = 5f;

    protected void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        enemyRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        enemyData.canDrop = true;
        currentHealth = enemyData.maxHealth;
        SetPacingLocation();
    }

    protected override void Pacing()
    {
        if (player == null) return;

        if (IsArrivedTargetPosition() == true)
        {
            PatrolAround();
            animator.SetBool("isMoving", true);
        }
        else
        {
            MoveToPlayer();
        }
        pacingTimer += Time.deltaTime;
        if (pacingTimer >= maxPacingTime)
        {
            pacingTimer = 0f;
            //EnemyAttackManager.Instance.SetCurrentAttacker();
           // enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);
         
        }
    }
    protected override void AttackPlayer()
    {
        if (player == null) return;

        if (IsArrivedTargetPosition() == false)
        {
            MoveToPlayer();
            PatrolAround();
        }
        else
        {
            HandlrAttackAction();
        }

       // pacingTimer += Time.deltaTime;

       /* if (pacingTimer >= maxPacingTime)
        {
            pacingTimer = 0f;
            //EnemyAttackManager.Instance.SetCurrentAttacker();
            enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);
            animator.SetTrigger("isAttack");
        }*/
    }

    private void HandlrAttackAction()
    {
        if(animator != null)
        {
            animator.SetTrigger("isAttack");
            animator.SetBool("isMoving",false);
        }
    }

    protected override void Die()
    {
        base.Die();
        //   SoundManager.Instance.PlaySFX("TrashcanDeath", 0.8f);
    }
    /* protected override void Die()
     {
         base.Die();
         float rand = Random.value;
         if (rand <= dropChance)
         {
             Instantiate(dropPerfab, transform.position, Quaternion.identity);
         }
     }*/
}



