using UnityEngine;

public class EnemyTrashCan : EnemyBaseController
{
    private float pacingTimer = 0f;
    private float maxPacingTime = 8f;
    protected void Awake()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    protected override void Start()
    {
        base.Start();
        //enemyAI.SetEnemyState(EnemyState.Pacing);
        SetPacingLocation();
        enemyData.canDrop = true;
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
            animator.SetBool("isMoving", true);
            animator.SetBool("isIdle", false);
        }
        pacingTimer += Time.deltaTime;
        if (pacingTimer >= maxPacingTime)
        {
            pacingTimer = 0f;
            /*enemyAI.SetEnemyState(EnemyState.Attack);
            animator.SetBool("isAttack", true);*/
           // EnemyAttackManager.Instance.HandleAttack();
        }
    }

    private void HandleAttackAction()
    {
        animator.SetTrigger("isAttack");
        SoundManager.Instance.PlaySFX("TrashcanAttk", 1f);
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
