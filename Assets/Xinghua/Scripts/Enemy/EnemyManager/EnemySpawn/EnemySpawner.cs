using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    private bool isTrigger = false;

    [SerializeField] private Transform enemyParent;
    private EnemySpawnTrigger currentActiveTrigger;
    private float enemyNeedBeenClear;
    private void Awake()
    {
        currentActiveTrigger = GetComponentInParent<EnemySpawnTrigger>();
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.GetComponent<PlayerMovement>() != null && isTrigger == false)
        {
            foreach (var enemyPrefab in enemyDatas)
            {
                SpawnEnemy(enemyPrefab, transform.position);//the position shoule related to the camera size,now just for temp
                currentActiveTrigger.gameObject.SetActive(true);
            }
            isTrigger = true;
        }
    }

    private void SpawnEnemy(EnemyData data, Vector3 position)
    {
        float randomOffsetX = Random.Range(-20f, 0);
        float randomOffsetY = Random.Range(-0.5f, 0);
        Vector3 spawnOffset = new Vector3(12 + randomOffsetX, randomOffsetY, 0);
        GameObject enemy = Instantiate(data.enemyPrefab, position + spawnOffset, Quaternion.identity);
        enemy.transform.SetParent(enemyParent);

        if (EnemyTriggerManager.Instance != null)
        {
            EnemyTriggerManager.Instance.enemiesInTheScene.Add(enemy);
           // Debug.Log("enemy count:" + EnemyTriggerManager.Instance.enemiesInTheScene.Count);
            EnemyTriggerManager.Instance.CheckEnemyNumberInTheScene();
        }

        EnemyBaseController controller = enemy.GetComponent<EnemyBaseController>();
        controller.enemyData = data;

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.GetComponent<PlayerMovement>() != null)
        {
            isTrigger = true;
            this.gameObject.SetActive(false);

           
            
         /*   if (currentActiveTrigger != null)
            {
                {
                    currentActiveTrigger.AddTrigger(gameObject);
                }

            }*/
        }
    }
}
