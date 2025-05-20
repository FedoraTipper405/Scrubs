using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    //manage the enemy in the scene and the trigger for spawn enemy

 
    public List<EnemyData> allEnemies = new List<EnemyData>();
    //private int currentIndex = 0;
    private int enemyCount;
    //[SerializeField] private List<GameObject> expectedEnemies;
    private List<GameObject> triggeredList = new List<GameObject>();
    private int enemyWaveDeathCount = 0;
    private void Start()
    {
        GetAllEnemyCurrentWave();
       
    }
    private void GetAllEnemyCurrentWave()
    {
        allEnemies.Clear();
        foreach (Transform enemy in transform)
        {
            EnemySpawner enemySpawner = enemy.GetComponentInChildren<EnemySpawner>();
            foreach (var enemyData in enemySpawner.enemyDatas)
            {
                allEnemies.Add(enemyData);
            }
        }
    }
 
    public void HandleWaveEnemyCount()
    {
        enemyWaveDeathCount++;
        Debug.Log("enemyWaveLeft: " + enemyWaveDeathCount);
    }


}
