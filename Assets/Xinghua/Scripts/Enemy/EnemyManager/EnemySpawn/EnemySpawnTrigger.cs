using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public int total = 0;
    public int activeTriggersCount = 0;


    public void GetAllEnemyCurrentWave()
    {
        total = 0;
        EnemySpawner[] spawners = GetComponentsInChildren<EnemySpawner>();
        foreach (var spawner in spawners)
        {
            if (spawner.enemyDatas != null)
            {
               // Debug.Log("single enemy count start" + spawner.name+ spawner.enemyDatas.Count);
                Debug.Log("total before" + total);
                total += spawner.enemyDatas.Count;
                Debug.Log("total after" + total);
            }

        }
        Debug.Log("###final enemy count"+total);
        if (total > 0 && EnemyTriggerManager.Instance != null)
        {
            EnemyTriggerManager.Instance.taskEnemieCount = total;
        }
    }

}


