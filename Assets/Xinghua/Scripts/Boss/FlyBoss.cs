using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Brute;

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

    [HideInInspector]public Transform player;
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
                break;
            case FlyBossState.Flying:
                Flying();
                Attack();
                break;
            case FlyBossState.Attack:
                Attack();
                break;
            case FlyBossState.Recovering:
                Recovering();
                break;
        }
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
    private float t;
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

    }
    private void Recovering()
    {
        Debug.Log("rocover now play reload anim");
        anim.SetBool("isRecover",true);
        shooter.bulletPool.Clear();
        StartCoroutine(Land());
    }

    private IEnumerator Land()
    {
        yield return new WaitForSeconds(4f);
        transform.position = landPosition;
        anim.SetBool("isRecover", true);
    }

    protected void Attack()
    {
        anim.SetBool("isRecover",false);
        shooter.Shoot();
    }
    public void OnRecoverEnd()
    {
        //fly and shoot again
       
    }
    public override void TakeDamage(float amount)
    {
        Debug.Log("hit amount:" + amount);
        SetCurrentState(FlyBossState.Recovering);

        if (currentState == FlyBossState.Recovering)
        {
            damageAmount = bonusDamage * amount;
        }
        else
        {
            damageAmount = amount;
        }

       // Debug.Log(this.name + " TakeDamage before" + "current:"+currentHealth + "max:"+maxHealth + "damage apply:" + damageAmount);
        if (currentHealth >= damageAmount)
        {
            currentHealth -= damageAmount;
        }
        else
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
       // Debug.Log(this.name + " TakeDamage after" + "current:" + currentHealth + "max:" + maxHealth + "damage apply:" + damageAmount);
        health.UpdateHealthUI(currentHealth, maxHealth);
    }
}
