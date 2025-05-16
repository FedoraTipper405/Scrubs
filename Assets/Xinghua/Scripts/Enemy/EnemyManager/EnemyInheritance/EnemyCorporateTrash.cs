using UnityEngine;
using static EnemyAI;

public class EnemyCorporateTrash : EnemyBaseController
{
    protected override void Start()
    {
        enemyAI.SetEnemyState(EnemyAI.EnemyState.Pacing);
        transform.position += new Vector3(-1, 1, 0);
        Debug.Log("base state" + enemyAI.currentState);
    }
    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // if player run move to the player again
        if (dist > enemyData.attackRange)
        {
            enemyAI.currentState = EnemyState.Pacing;
            return;
        }

        //even within the range still move to player if player location changed
        Vector3 dir = (GetTargetPosition()  - transform.position).normalized;
        transform.position += dir * (enemyData.moveSpeed * 0.5f) * Time.deltaTime;

        // keep attack
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            //player process the damage
            lastAttackTime = Time.time;
        }
    }
    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();
        float dist = Vector3.Distance(transform.position, player.position);


        if (dist > enemyData.attackRange)
        {
            Vector3 dir = (GetTargetPosition() - transform.position).normalized;
            transform.position += dir * enemyData.moveSpeed * Time.deltaTime;
        }

        else
        {
            bool canAttack = EnemyAttackManager.Instance.TryRequestAttack(this.gameObject);
            if (canAttack)
            {
                enemyAI.currentState = EnemyState.Attack;
                lastAttackTime = Time.time;
            }
            else
            {

                enemyAI.currentState = EnemyState.Pacing;
            }
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



