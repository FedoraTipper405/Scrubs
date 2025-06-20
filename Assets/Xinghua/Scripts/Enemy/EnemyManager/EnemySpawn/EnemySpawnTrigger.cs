using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public int total = 0;
    public int activeTriggersCount = 0;

    public void SetTaskEnemyInCurrentWave()
    {
        total = 0;
        EnemySpawner[] spawners = GetComponentsInChildren<EnemySpawner>();
        foreach (var spawner in spawners)
        {
            if (spawner.enemyDatas != null)
            {
                total += spawner.enemyDatas.Count;
            }

        }
        if (total > 0 && EnemyTriggerManager.Instance != null)
        {
            EnemyTriggerManager.Instance.taskEnemieCount = total;
        }
        
    }

}


