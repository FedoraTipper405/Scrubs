using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();
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
            foreach (var data in enemyDatas)
            {
                SpawnEnemy(data, transform.position);//the position shoule related to the camera size,now just for temp
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

        EnemyTriggerManager.Instance.taskEnemies.Add(enemy);
       // EnemyAttackManager.Instance.SetCurrentAttacker(enemy);
        EnemyBaseController controller = enemy.GetComponent<EnemyBaseController>();
        controller.enemyData = data;

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.GetComponent<PlayerMovement>() != null)
        {
            isTrigger = true;
            currentActiveTrigger.activeTriggersCount++;
            EnemyTriggerManager.Instance.CheckTriggerState(currentActiveTrigger.activeTriggersCount, currentActiveTrigger.transform.childCount);
            this.gameObject.SetActive(false);
        }
    }
}
