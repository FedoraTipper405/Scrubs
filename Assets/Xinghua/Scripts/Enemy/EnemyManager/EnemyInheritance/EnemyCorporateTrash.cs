using UnityEngine;
using static EnemyAI;

public class EnemyCorporateTrash : EnemyBaseController
{
    protected override void Start()
    {
        enemyAI.SetEnemyState(EnemyAI.EnemyState.Pacing);
        transform.position += new Vector3(-1, 1, 0);
        enemyData.canDrop = true;
    }

    protected override void AttackPlayer()
    {
        base.MoveToPlayer();
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // if player run move to the player again
        if (dist > enemyData.attackRange)
        {
            enemyAI.currentState = EnemyState.Pacing;
            return;
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

    protected override void Die()
    {
        base.Die();
        float rand = Random.value;
        if (rand <= dropChance)
        {
            Instantiate(dropPerfab, transform.position, Quaternion.identity);
        }
    }
}



