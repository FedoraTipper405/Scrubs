using UnityEngine;

public class BaseAtkCollider : MonoBehaviour
{
    float currentDamage;
    float currentKnockback;
    bool canHit = false;
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canHit)
        {
            Debug.Log("1");
            if (collision.gameObject.layer == 6)
            {
                Debug.Log("2");
                if (collision.gameObject.GetComponent<EnemyBaseController>() != null){

                    Debug.Log("3");
                    collision.gameObject.GetComponent<EnemyBaseController>().TakeDamage((int)Mathf.Ceil(currentDamage));
                }
            }
            canHit = false;
        }
    }
}
