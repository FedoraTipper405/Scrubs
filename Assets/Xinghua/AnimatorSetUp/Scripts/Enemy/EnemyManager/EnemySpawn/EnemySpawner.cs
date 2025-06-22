using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();
    private bool isTrigger = false;


    private EnemySpawnTrigger currentActiveTrigger;
    private Vector3 spawnPos;
    private float enemyNeedBeenClear;
    private void Awake()
    {
        currentActiveTrigger = GetComponentInParent<EnemySpawnTrigger>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.GetComponent<PlayerMovement>() != null && isTrigger == false)
        {
            foreach (var data in enemyDatas)
            {
                SpawnEnemy(data, other.transform.position);//the position shoule related to the camera size,now just for temp
                currentActiveTrigger.gameObject.SetActive(true);
            }
            isTrigger = true;
        }
    }

    private void SpawnEnemy(EnemyData data, Vector3 position)
    {
        EnemyTriggerManager.Instance.enemiesKilled = 0;

        float randomOffsetX = Random.Range(-8f, 4f);
        float randomOffsetY = Random.Range(-1f, 1f);
        Vector3 spawnOffset = new Vector3(0 + randomOffsetX, randomOffsetY, 0);


        GameObject enemy = Instantiate(data.enemyPrefab, position, Quaternion.identity);
        enemy.transform.SetParent(EnemyTriggerManager.Instance.enemyParent, true);
        EnemyAttackManager.Instance.potentialAttackers.Add(enemy);

        EnemyTriggerManager.Instance.HandleEnemyChangeWithCamera(true, false);

       /* EnemyBaseController controller = enemy.GetComponent<EnemyBaseController>();
        controller.enemyData = data;*/

    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other != null && other.GetComponent<PlayerMovement>() != null)
        {
            isTrigger = true;
            int triggeredSpawner = currentActiveTrigger.activeTriggersCount;
            triggeredSpawner++;
            int totalSpawner = currentActiveTrigger.transform.childCount;// the totalspawner is 1 
            this.gameObject.SetActive(false);
        }
    }
}
