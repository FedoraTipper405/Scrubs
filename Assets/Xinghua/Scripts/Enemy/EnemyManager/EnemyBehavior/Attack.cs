using UnityEngine;

public class Attack : MonoBehaviour
{
    private float damage;
    private void Start()
    {
        Debug.Log(transform.parent.name + "start attack script ");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(this.name + "attack ");
        HandleAttack(other.gameObject);
    }

    private void HandleAttack(GameObject obj)
    {
        var targetPlayer = obj.gameObject.GetComponent<PlayerHealth>();
        var attackerEnemy = gameObject.GetComponentInParent<EnemyBaseController>();
        var attackerBoss = gameObject.GetComponentInParent<BaseBoss>();

        if (targetPlayer != null && attackerEnemy != null)
        {
            targetPlayer.TakeDamage(attackerEnemy.enemyData.damage);
        }
        else if(targetPlayer != null && attackerBoss != null)
        {
            targetPlayer.TakeDamage(attackerBoss.damage);
        }
        
       
    }

}
