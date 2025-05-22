using UnityEngine;

public class RifleFistCollider : BaseAtkCollider
{
    bool hasHit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (hasHit)
        {
            canHit = false;
            hasHit = false;
        }
    }
    protected override void AttackHandler(GameObject hitEnemy)
    {
        if (canHit)
        {
            hasHit = false;
          //  Debug.Log("1");
            if (hitEnemy.layer == 6)
            {
             //   Debug.Log("2");
                if (hitEnemy.GetComponent<EnemyBaseController>() != null)
                {

                    //    Debug.Log("3");
                    hitEnemy.GetComponent<EnemyBaseController>().TakeDamage((int)Mathf.Ceil(currentDamage), playerGameobject);
                    hasHit = true;
                }
            }
            
        }
    }
}

