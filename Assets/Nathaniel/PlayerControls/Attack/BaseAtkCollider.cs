using UnityEngine;

public class BaseAtkCollider : MonoBehaviour
{
    protected float currentDamage;
    protected float currentKnockback;
    protected bool canHit = false;
    [SerializeField] protected GameObject playerGameobject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PrepareForAttack(float damage, float knockback)
    {
        currentDamage = damage;
        currentKnockback = knockback;
        canHit = true;
    }
    protected virtual void AttackHandler(GameObject hitEnemy)
    {
        if (canHit)
        {
           // Debug.Log("1");
            if (hitEnemy.layer == 6)
            {
              //  Debug.Log("2");
                if (hitEnemy.GetComponent<EnemyBaseController>() != null)
                {

                 //   Debug.Log("3");
                    hitEnemy.GetComponent<EnemyBaseController>().TakeDamage((int)Mathf.Ceil(currentDamage), playerGameobject);
                    canHit = false;
                }
            }
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        AttackHandler(collision.gameObject);
    }
}
