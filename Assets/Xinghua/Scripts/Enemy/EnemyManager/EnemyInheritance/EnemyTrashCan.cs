using UnityEngine;
using static EnemyAI;

public class EnemyTrashCan : EnemyBaseController
{
    protected override void Start()
    {
        base.Start();
        enemyAI.SetEnemyState(EnemyState.Pacing);
    }


    protected override void AttackPlayer()
    {
        if (player == null) return;
        base.AttackPlayer();
        MoveToPlayer();

        //even within the range still move to player if player location changed
        Vector3 dir = (GetStopPosition() - transform.position).normalized;
        transform.position += dir * (enemyData.moveSpeed * 0.5f) * Time.deltaTime;

        // keep attack
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            //player process the damage
            lastAttackTime = Time.time;
        }
    }
}
