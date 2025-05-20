using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    private void Start()
    {
        GetAllEnemyCurrentWave();
        if (EnemyTriggerManager.Instance != null)
        { EnemyTriggerManager.Instance.taskEnemies.Clear(); }
    }

    public void GetAllEnemyCurrentWave()
    {
        int total = 0;
        EnemySpawner[] spawners = GetComponentsInChildren<EnemySpawner>(true);
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


