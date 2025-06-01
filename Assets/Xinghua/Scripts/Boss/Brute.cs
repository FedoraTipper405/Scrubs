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

    [SerializeField] private Transform player;
    private float distToPlayer;
    [SerializeField]private float stopDistance = 1f;
    protected Vector3 stopOffset;

    [Header("Animator")]
    private Animator anim;

    [Header("Attack")]
    private bool canAttack = false;
    public float maxHealth;
    private float damageAmount;
    [SerializeField]private float bonusDamage;
    private Vector3 attackPosition;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Debug.Log("Animation" + anim.name);
       
    }
    private void Start()
    {
        currentHealth = maxHealth;
       
        SetCurrentState(BruteState.Attack);

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
            transform.position += dir * moveSpeed * Time.deltaTime;
            anim.SetBool("isMoving", true);
        }
    
        else
        {
            Debug.Log("boss arrive attack position");
            anim.SetBool("isAttack",true);
        } 
    }

    private void Recovering()
    {
        Debug.Log("boss recovering now");
        anim.SetBool("isRecovering",true); 
    }

    public void TakeDamage(float amount)
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
        }
        else
        {
            currentHealth = 0;
        }
    }

    private void OnAttackEnd()
    {
        SetCurrentState(BruteState.Attack);
    }

}