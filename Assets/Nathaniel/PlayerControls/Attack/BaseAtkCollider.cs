using UnityEngine;

public class BaseAtkCollider : MonoBehaviour
{
    protected float currentDamage;
    protected float currentKnockback;
    protected bool canHit = false;
    [SerializeField] protected GameObject playerGameobject;
    [SerializeField] SOPlayerStats stats;
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
        currentDamage = damage * (stats.strengthMultPerLevel * stats.strengthLevel - stats.strengthMultPerLevel + 1);
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
                    playerGameobject.transform.GetChild(2).GetComponent<PlayerHealth>().GainSpecial(5);
                    hitEnemy.GetComponent<EnemyBaseController>().TakeDamage((int)Mathf.Ceil(currentDamage), currentKnockback,playerGameobject);
                    canHit = false;
                }
                //xh add
                if (hitEnemy.GetComponent<BaseBoss>() != null)
                {
      
                    hitEnemy.GetComponent<BaseBoss>().TakeDamage((int)Mathf.Ceil(currentDamage));//50 need be valuable
                    canHit = false;
                }
                //xh code end
            }
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        AttackHandler(collision.gameObject);
       
    }
}
