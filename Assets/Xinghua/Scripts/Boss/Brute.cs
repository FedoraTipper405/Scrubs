using System.Collections;
using UnityEngine;
public class Brute : BaseBoss
{
    public enum BruteState
    {
        Idle,
        Attack,
        Recovering,
    }
    public BruteState currentState;

    [HideInInspector] private Transform player;
    private float distToPlayer;
    protected Vector3 stopOffset;

    [Header("Animator")]
    private Animator anim;
    [Header("Recover")]
    [SerializeField] private float maxRecoverTime;

    [Header("Attack")]
    private float damageAmount;
    [SerializeField] private float bonusDamage;
    private Vector3 attackPosition;
    [SerializeField] GameObject healthHPBar;
    private Health health;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        SetCurrentState(BruteState.Attack);
        health = GetComponentInChildren<Health>();
        healthHPBar.SetActive(true);
        player = FindAnyObjectByType<PlayerMovement>().transform;
        bossRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case BruteState.Idle:
                break;
            case BruteState.Attack:
                TryAttack();
                break;
            case BruteState.Recovering:
                RecoveringBegin();
                break;
        }
    }
    private void FixedUpdate()
    {
        FlipTowardsPlayer();
    }
    protected override void FlipTowardsPlayer()
    {
        if (player == null ||currentState == BruteState.Recovering) return;

        if (player.position.x < transform.position.x)
        {
            bossRenderer.flipX = false;
        }
        else
        {
            bossRenderer.flipX = true;
        }
    }

    private void SetCurrentState(BruteState state)
    {
        currentState = state;
    }

    private void TryAttack()
    {
        if (IsArrivedAttackPosition() == true)
        {
            HandleAttackAction();
        }
        else
        {
            MovingToPlayer();
        }

    }

    protected Vector3 GetStopPosition()
    {

        if (player.position.x < transform.position.x)
        {

            stopOffset = new Vector3(stopDistance, 0, 0);
        }
        else if (player.position.x >= transform.position.x)
        {
            stopOffset = new Vector3(-stopDistance, 0, 0);
        }

        return player.position + stopOffset;
    }

    protected bool IsArrivedAttackPosition()
    {
        //Debug.Log("detect distance");
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distToPlayer > stopDistance)
        {
            return false;
        }
        return true;
    }

    private void MovingToPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        anim.SetBool("isMoving", true);
    }

    private void HandleAttackAction()
    {
        anim.SetBool("isAttack", true);
    }

    public override void TakeDamage(float amount)
    {
       // SetCurrentState(BruteState.Recovering);

        if (currentState == BruteState.Recovering)
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
            SoundManager.Instance.PlaySFX("PlayerKick", 0.9f);
            base.GetHitFlash();

        }
        else
        {
            currentHealth = 0;
            Die();

        }
        health.UpdateHealthUI(currentHealth, maxHealth);
    }

    private void BossAttackEnd()//anim event
    {
        SetCurrentState(BruteState.Recovering);
    }

    private void RecoveringBegin()
    {
        anim.SetBool("isRecoverBegin", true);
        anim.SetBool("isAttack", false);
        anim.SetBool("isMoving", false);
    }
 
    public void OnRecoveringLoop()//anim event
    {
        StartCoroutine(OnRecovering());
    }

    public IEnumerator OnRecovering()
    {
        anim.SetBool("isRecoverLoop", true);
        float random = Random.Range(3, maxRecoverTime);
        yield return new WaitForSeconds(random);
        RecoverFinish();
    }
    private void RecoverFinish()
    {
        // Debug.Log(this.name + "RecoverFinish");
        anim.SetBool("isRecoverLoop", false);
        anim.SetBool("isRecoverBegin", false);
        anim.SetBool("isRecoverFinish", true);
    }
    public void OnRecoverFinishAnimEnd()//anim event
    {
        anim.SetBool("isRecoverFinish", false);
        SetCurrentState(BruteState.Attack);
    }
}