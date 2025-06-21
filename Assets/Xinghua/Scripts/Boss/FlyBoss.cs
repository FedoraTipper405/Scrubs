using System.Collections;
using UnityEngine;

public class FlyBoss : BaseBoss
{
    public enum FlyBossState
    {
        Idle,
        Flying,
        Attack,
        Recovering,
    }
    public FlyBossState currentState;


    [Header("Attack")]
    private bool canAttack = false;
    public float attackValue = 20f;
    [SerializeField] private float bulletScale = 0.2f;

    [Header("Land")]
    public Vector3 landPosition;


    [Header("Flying")]
    public float radius = 5f;
    public float heightOffset = 5f;
    public float floatAmplitude = 0.5f;
    public float floatSpeed = 2f;
    private Vector3 lastTargetPos;
    private float angle;
    private bool hasReachedTarget = false;
    private Vector3 lastDirection;

    [HideInInspector] public Transform player;
    private Shooter shooter;
    private Animator anim;
    private float damageAmount;
    [SerializeField] private float bonusDamage;
    private Health health;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
        health = GetComponentInChildren<Health>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        SetCurrentState(FlyBossState.Flying);
        isFinalBoss = false;
    }
    public void SetCurrentState(FlyBossState state)
    {
        currentState = state;
    }

    private void Update()
    {

        switch (currentState)
        {
            case FlyBossState.Idle:
                // Idle();
                break;
            case FlyBossState.Flying:
                Flying();
                Attack();
                break;
            /* case FlyBossState.Attack:
                 Attack();
                 break;*/
            case FlyBossState.Recovering:
                Recovering();
                break;
        }
    }
    private void Idle()
    {
        anim.SetBool("isFlying", true);
    }
    private void MovingToPlayer()
    {

        canAttack = Vector3.Distance(transform.position, player.transform.position) <= stopDistance;
        if (!canAttack)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
            //anim
        }

        else
        {
            Debug.Log("boss arrive attack position");
            //anim
        }
    }

    private void Flying()
    {
        if (player == null) return;

        angle += speed * Time.deltaTime;
        if (angle > 360f) angle -= 360f;


        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        float floatY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;


        Vector3 targetPos = player.position + new Vector3(x, heightOffset + floatY, z);
        transform.position = targetPos;

        if (!hasReachedTarget && Vector3.Distance(transform.position, lastTargetPos) < 0.1f)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            hasReachedTarget = true;
        }

        Vector3 currentDirection = (player.position - transform.position).normalized;

        if (currentDirection.x > 0)
        {

            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        anim.SetBool("isFlying", true);
        anim.SetBool("isRecover", false);

        Attack();
    }
    private void Recovering()
    {
        StartCoroutine(Land());
    }

    private IEnumerator Land()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = landPosition;
        anim.SetBool("isRecover", true);
    }

    protected void Attack()
    {
        shooter = GetComponent<Shooter>();
        shooter.BossShoot(bulletScale);
    }

    public void OnRecoverEnd()
    {
        shooter.bulletPool.Clear();
        SetCurrentState(FlyBossState.Flying);
    }

    public override void TakeDamage(float amount)
    {
        // Debug.Log("hit amount:" + amount);
        SetCurrentState(FlyBossState.Recovering);

        if (currentState == FlyBossState.Recovering)
        {
            damageAmount = bonusDamage * amount;
        }
        else
        {
            damageAmount = amount;
        }

        if (currentHealth >= damageAmount)
        {
            currentHealth -= damageAmount;
            base.GetHitFlash();
        }
        else
        {
            currentHealth = 0;
            HandleDie();
        }

        health.UpdateHealthUI(currentHealth, maxHealth);
    }
    private void HandleDie()
    {
        anim.SetTrigger("isDeath");
    }

}
