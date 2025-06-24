using System;
using UnityEditor;
using UnityEngine;

public class BaseBoss : MonoBehaviour
{
    [SerializeField]protected float speed;
    [SerializeField] protected float stopDistance = 1f;
    public float currentHealth;
    [SerializeField] protected float maxHealth;
    protected SpriteRenderer bossRenderer;
    public float damage;
    public event Action OnDeath;
    public virtual void TakeDamage(float amount)
    {

    }
   
 
    protected virtual void FlipTowardsPlayer()
    {
       
    }

    protected void Die()
    {
        Destroy(gameObject);
        //sound
        GameManager.Instance.CheckLevelState();
        OnDeath?.Invoke();


    }


}
