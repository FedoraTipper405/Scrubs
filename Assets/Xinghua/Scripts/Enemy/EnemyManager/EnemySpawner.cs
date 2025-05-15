using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemyDatas;
    void Start()
    {
        
        SpawnEnemy(enemyDatas[0], new Vector3(0, 0, 0));

      
        SpawnEnemy(enemyDatas[1], new Vector3(2, 0, 0));

       
        SpawnEnemy(enemyDatas[2], new Vector3(4, 0, 0));
    }

    void SpawnEnemy(EnemyData data, Vector3 position)
    {
        GameObject enemy = Instantiate(data.enemyPrefab, position, Quaternion.identity);
        EnemyController controller = enemy.GetComponent<EnemyController>();
        controller.enemyData = data;
    }
}
