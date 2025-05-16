using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemyDatas;
    private bool isTrigger = false;
    private TriggerController triggerController;
    [SerializeField] private Transform enemyParent;
    private void Awake()
    {
        triggerController = GetComponentInParent<TriggerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.GetComponent<PlayerMovement>() != null && isTrigger == false)
        {
            foreach (var enemyPrefab in enemyDatas)
            {
                SpawnEnemy(enemyPrefab, transform.position);
            }
            isTrigger = true;
        }
    }

    private void SpawnEnemy(EnemyData data, Vector3 position)
    {
        GameObject enemy = Instantiate(data.enemyPrefab, position + new Vector3(12,0,0), Quaternion.identity);//change the spawn positon to free code
        enemy.transform.SetParent(enemyParent);
        if (triggerController != null)
        {
            triggerController.enemiesLeft.Add(enemy);
            Debug.Log("Enemy in the scene" + triggerController.enemiesLeft.Count);
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
