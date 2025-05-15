using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData;
    private float currentHealth;
    void Start()
    {
        SetEnemyValue();

    }

    private void SetEnemyValue()
    {
        currentHealth = enemyData.maxHealth;
        //other value need been set too......
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}