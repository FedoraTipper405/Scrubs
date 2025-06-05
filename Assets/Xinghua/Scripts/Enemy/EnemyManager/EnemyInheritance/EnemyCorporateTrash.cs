using UnityEngine;
using static EnemyAI;

public class EnemyCorporateTrash : EnemyBaseController
{
    private float pacingTimer;
    private float maxPacingTime;

    protected void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        if (enemyAI != null)
        {
            enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);
        }
      
        transform.position += new Vector3(-1, 1, 0);
        enemyData.canDrop = true;
        currentHealth = enemyData.maxHealth;
       SetPacingLocation();
    }

    protected override void SetPacingLocation()
    {
        patrolOffsets = new Vector3[]
            {
                Vector3.up * enemyData.pacingRadius,
                Vector3.down *enemyData.pacingRadius,
            };
    }

    protected override void AttackPlayer()
    {

        if (player == null) return;

        MoveToPlayer();
        if (IsArrivedTargetPosition() == true)
        {
            PatrolAround();
        }
        pacingTimer += Time.deltaTime;

        if (pacingTimer >= maxPacingTime)
        {
            pacingTimer = 0f;
            enemyAI.SetEnemyState(EnemyState.Attack);
            animator.SetTrigger("isAttack");
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



