using UnityEngine;
using System.Collections;
public class Brute : BaseBoss
{
    public enum BruteState
    {
        Idle,
        Attack,
        Recovering,
    }
    private BruteState currentState;

    [HideInInspector] private Transform player;
    private float distToPlayer;
    protected Vector3 stopOffset;

    [Header("Animator")]
    private Animator anim;

    [Header("Attack")]
    private float damageAmount;
    [SerializeField]private float bonusDamage;
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
                MovingToPlayer();
                TryAttack();
                break;
            case BruteState.Recovering:
                Recovering();
                break;
        }
    }
    private void FixedUpdate()
    {
        FlipTowardsPlayer();
    }
    protected override void FlipTowardsPlayer()
    {
        //Debug.Log("flip the boss" + player.transform);
        if (player == null) return;

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
        if(IsArrivedAttackPosition() == true)
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
        anim.SetBool("isRecovering", false);

    }

    private void HandleAttackAction()
    {
        anim.SetBool("isAttack", true);
        
    }
    private void Recovering()
    {
        anim.SetBool("isRecovering",true); 
        StartCoroutine(EndRecover());
    }

    public override void TakeDamage(float amount)
    {
        SetCurrentState(BruteState.Recovering);

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
            bossRenderer.color = new Color(1f,0f,0f,1f);//red
            StartCoroutine(EndFlash());
        }
        else
        {
            currentHealth = 0;
            Die();
           
        }
        health.UpdateHealthUI(currentHealth, maxHealth);
    }

     private IEnumerator EndFlash()
    {
        yield return new WaitForSeconds(0.25f);

        bossRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    private void BossAttackEnd()//this event attach to the attack animation
    {
        SetCurrentState(BruteState.Recovering);
    }

    private IEnumerator EndRecover()
    {
       //float random = Random.Range(1, 8)
        Debug.Log("end recover");
        yield return new WaitForSeconds(6f);
        SetCurrentState(BruteState.Attack);
       
    }
}