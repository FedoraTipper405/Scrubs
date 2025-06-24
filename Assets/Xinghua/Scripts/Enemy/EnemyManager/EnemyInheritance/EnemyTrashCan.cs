using UnityEngine;
using static EnemyAI;

public class EnemyTrashCan : EnemyBaseController
{
    //private float pacingTimer = 0f;
    //private float maxPacingTime = 8f;
    protected void Awake()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    protected override void Start()
    {
        base.Start();
        enemyAI.SetEnemyState(EnemyState.Attack);
        enemyData.canDrop = true;
        SetPacingLocation();
    }

    protected override void Pacing()
    {
        if (player == null) return;
      
       
        if (IsArrivedTargetPosition() == true)
        {
            MoveToPlayer();
            animator.SetBool("isMoving", true);
            animator.SetBool("isIdle", false);
            // PatrolAround();
        }
        else
        {
            HandleAttackAction();
        }
      /*  pacingTimer += Time.deltaTime;
        if (pacingTimer >= maxPacingTime)
        {
            pacingTimer = 0f;
            enemyAI.SetEnemyState(EnemyState.Attack);
        }*/
    }

    private void HandleAttackAction()
    {
        animator.SetTrigger("isAttack");
        SoundManager.Instance.PlaySFX("TrashcanAttk", 1f);
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
        base.AttackPlayer();
        if (IsArrivedTargetPosition() != true)
        {
            MoveToPlayer();
            animator.SetBool("isMoving", true);
            animator.SetBool("isIdle", false);
            // PatrolAround();
        }
        else if (Time.time - lastAttackTime >= enemyData.attackCooldown && IsArrivedTargetPosition() == true)
        {

            HandleAttackAction();
           
            lastAttackTime = Time.time;
        }
        //even within the range still move to player if player location changed
      /*  Vector3 dir = (GetStopPosition() - transform.position).normalized;
        transform.position += dir * (enemyData.moveSpeed * 0.5f) * Time.deltaTime;*/

        // keep attack
        if (Time.time - lastAttackTime >= enemyData.attackCooldown)
        {
            //player process the damage
            lastAttackTime = Time.time;
        }
    }

    public void OnAttackEnd()
    {
        animator.SetBool("isAttack", false);
    }
    protected override void Die()
    {
        base.Die();
     //   SoundManager.Instance.PlaySFX("TrashcanDeath", 0.8f);
    }
}
