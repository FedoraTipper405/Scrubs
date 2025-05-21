using UnityEngine;
using static EnemyAI;

public class EnemyTrashCan : EnemyBaseController
{
    private float pacingTimer = 0f;
    private float maxPacingTime = 8f;
    protected void Awake()
    {
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
        animator.SetBool("isMoving", true);
        MoveToPlayer();
        if (IsArrivedTargetPosition() == true)
        {
            PatrolAround();
        }
        pacingTimer += Time.deltaTime;
      // Debug.Log("pacingTimer"+ pacingTimer + "maxPacingTime" + maxPacingTime);
        if (pacingTimer >= maxPacingTime)
        {
            pacingTimer = 0f;
            enemyAI.SetEnemyState(EnemyState.Attack);
        }
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
        animator.SetBool("isMoving", true);
        if (Time.time - lastAttackTime >= enemyData.attackCooldown && IsArrivedTargetPosition() == true && enemyAI.currentState == EnemyState.Attack)
        {

            animator.SetBool("isAttack", true);
            SoundManager.Instance.PlaySFX("TrashcanAttk",1f);
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
    protected override void Die()
    {
        base.Die();
        SoundManager.Instance.PlaySFX("TrashcanDeath", 0.8f);
    }
}
