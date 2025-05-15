using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemyDatas;
    private bool isTrigger = false;
    private List<GameObject> enemiesLeft;
    [SerializeField] private Transform enemyParent;
    private void Start()
    {
        enemiesLeft = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if no enemy in the scene the trigger will work 

        if (other != null && other.GetComponent<PlayerMovement>() != null && isTrigger == false)
        {
            foreach (var enemyPrefab in enemyDatas)
            {
                SpawnEnemy(enemyPrefab, transform.position);//here may be give a random positon or out of the cam
            }
            isTrigger = true;
        }
    }

    private void SpawnEnemy(EnemyData data, Vector3 position)
    {
        GameObject enemy = Instantiate(data.enemyPrefab, position, Quaternion.identity);
        enemy.transform.SetParent(enemyParent);
        enemiesLeft.Add(enemy);
        EnemyController controller = enemy.GetComponent<EnemyController>();
        controller.enemyData = data;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.GetComponent<PlayerMovement>() != null && isTrigger == true)
        {
            isTrigger = true;
        }
    }
}
