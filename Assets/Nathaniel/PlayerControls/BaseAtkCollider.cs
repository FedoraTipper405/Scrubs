using UnityEngine;

public class BaseAtkCollider : MonoBehaviour
{
    float damage;
    float knockback;
    bool canHit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canHit)
        {
            if(collision.gameObject.layer == 6)
            {
                //if(collision.gameObject.GetComponent<EnemyBaseController>() != null){
                //(collision.gameObject.GetComponent<EnemyBaseController>().TakeDamage(damage);
                //}
            }
        }
    }
}
