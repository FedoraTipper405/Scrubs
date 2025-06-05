using UnityEngine;

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
    private bool canAttack = false;
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
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case BruteState.Idle:
                break;
            case BruteState.Attack:
                MovingToPlayer();
                Attack();
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
            renderer.flipX = false;
        }
        else
        {
            renderer.flipX = true;
        }
    }

    private void SetCurrentState(BruteState state)
    {
        currentState = state;
    }

    private void Attack()
    {
        MovingToPlayer(); 
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
        Debug.Log("detect distance");
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distToPlayer > stopDistance)
        {
            return false;
        }
        Debug.Log(distToPlayer + stopDistance);
        return true;
    }

    private void MovingToPlayer()
    {
      
        canAttack = Vector3.Distance(transform.position, player.transform.position) <= stopDistance;
        if (!canAttack)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
            //anim.SetBool("isMoving", true);
        }
    
        else
        {
            Debug.Log("boss arrive attack position");
            //anim.SetBool("isAttack",true);
        } 
    }

    private void Recovering()
    {
        Debug.Log("boss recovering now");
       // anim.SetBool("isRecovering",true); 
       //maybe when recovering can spawn some enemy
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
        }
        else
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
        health.UpdateHealthUI(currentHealth, maxHealth);
    }

    private void OnAttackEnd()
    {
        SetCurrentState(BruteState.Attack);
    }

}