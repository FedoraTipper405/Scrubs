using UnityEngine;

public class TargetTest : MonoBehaviour
{
    [SerializeField] EnemySpawnManager trigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyBaseController>();
        if (enemy != null)
        {
            enemy.TakeDamage(2,gameObject);//200 just for enemy code test , the value should from player data
            trigger.enemiesInTheScene.Remove(other.gameObject);
        }
    }
    public void TakeDamage()
    {
        Debug.Log("take damage!!!!!!!!!!!");
    }
}
