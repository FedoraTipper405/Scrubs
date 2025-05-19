using UnityEngine;

public class TargetTest : MonoBehaviour
{
    [SerializeField] EnemySpawnManager trigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyBaseController>();
        if (enemy != null)
        {
            enemy.TakeDamage(2,gameObject);
            trigger.enemiesInTheScene.Remove(other.gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("take damage to player:"+ amount);
    }
}
