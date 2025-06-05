using UnityEditor;
using UnityEngine;

public class BaseBoss : MonoBehaviour
{
    [SerializeField]protected float speed;
    [SerializeField] protected float stopDistance = 1f;
    public float currentHealth;
    [SerializeField] protected float maxHealth;
    protected SpriteRenderer renderer;
    public float damage;

    public virtual void TakeDamage(float amount)
    {

    }
   
 
    protected virtual void FlipTowardsPlayer()
    {
       
    }

  
}
