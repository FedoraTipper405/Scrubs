using UnityEngine;

public class MultiTargetCollider : BaseAtkCollider
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
                    hitEnemy.GetComponent<EnemyBaseController>().TakeDamage((int)Mathf.Ceil(currentDamage), currentKnockback, playerGameobject);//xh add the currentKockBack 
                    hasHit = true;
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
}
