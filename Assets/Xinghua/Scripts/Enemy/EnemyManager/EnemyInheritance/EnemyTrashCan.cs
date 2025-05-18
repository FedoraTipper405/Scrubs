using UnityEngine;
using static EnemyAI;

public class EnemyTrashCan : EnemyBaseController
{
    protected void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    protected override void Start()
    {
        base.Start();
        enemyAI.SetEnemyState(EnemyState.Pacing);
        enemyData.canDrop = true;
       
    }

    protected override void Pacing()
    {
        if (player == null) return;
        animator.SetBool("isMoving", true);
        MoveToPlayer();
        if (IsArrivedTargetPosition() == true)
        {
            PatrolAround();
        }
    }
    protected override void AttackPlayer()
    {
        if (player == null) return;
        base.AttackPlayer();

        if (Time.time - lastAttackTime >= enemyData.attackCooldown && IsArrivedTargetPosition() == true && enemyAI.currentState == EnemyState.Attack)
        {

            animator.SetBool("isAttack", true);
            lastAttackTime = Time.time;
        }
        //even within the range still move to player if player location changed
        Vector3 dir = (GetStopPosition() - transform.position).normalized;
        transform.position += dir * (enemyData.moveSpeed * 0.5f) * Time.deltaTime;

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

}
