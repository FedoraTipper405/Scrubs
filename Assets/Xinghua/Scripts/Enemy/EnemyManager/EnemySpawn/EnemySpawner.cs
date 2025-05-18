using UnityEngine;
using static EnemyAI;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemyDatas;
    private bool isTrigger = false;
    private EnemySpawnManager triggerController;
    [SerializeField] private Transform enemyParent;
    private void Awake()
    {
        triggerController = GetComponentInParent<EnemySpawnManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.GetComponent<PlayerMovement>() != null && isTrigger == false)
        {
            foreach (var enemyPrefab in enemyDatas)
            {
                SpawnEnemy(enemyPrefab, transform.position);//the position shoule related to the camera size,now just for temp
            }
            isTrigger = true;
        }
    }

    private void SpawnEnemy(EnemyData data, Vector3 position)
    {
        float randomOffsetX = Random.Range(-20f, 0);
        float randomOffsetY = Random.Range(-0.5f,0);
        Vector3 spawnOffset = new Vector3(12 + randomOffsetX, randomOffsetY, 0);
        GameObject enemy = Instantiate(data.enemyPrefab, position + spawnOffset, Quaternion.identity);
        enemy.transform.SetParent(enemyParent);

        if (triggerController != null)
        {
            triggerController.enemiesInTheScene.Add(enemy);
        }
                          
        EnemyBaseController controller = enemy.GetComponent<EnemyBaseController>();
        controller.enemyData = data;

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.GetComponent<PlayerMovement>() != null)
        {
            isTrigger = true;
        }
    }
}
