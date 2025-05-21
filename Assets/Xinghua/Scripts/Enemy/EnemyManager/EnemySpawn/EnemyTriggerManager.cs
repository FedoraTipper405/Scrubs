using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTriggerManager : MonoBehaviour
{
    public static EnemyTriggerManager Instance;
    private List<Transform> enemyTriggers = new List<Transform>();
    [Header("Camera related")]
    public UnityEvent OnMove;
    public UnityEvent OnLock;


    [HideInInspector]
    public int taskEnemieCount;
    public List<GameObject> taskEnemies = new List<GameObject>();
    public List<GameObject> enemiesClear;
    public List<GameObject> enemiesLeft = new List<GameObject>();//this list is all the enemies in the scene left

    bool isAllSpawnerEnable = true;
    private int currentIndex = 0;
    private void Start()
    {

        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            GameObject childGameObject = childTransform.gameObject;

            enemyTriggers.Add(childTransform);
            if (i == 0)
            {
                childGameObject.SetActive(true);
                EnemySpawnTrigger spawnTrigger = enemyTriggers[0].GetComponent<EnemySpawnTrigger>();
                if (spawnTrigger != null)
                {
                    spawnTrigger.GetAllEnemyCurrentWave();
                }
            }
            else
            {
                childGameObject.SetActive(false);
            }
        }

    }
    public List<GameObject> GetObjectsNotInList(List<GameObject> bigList, List<GameObject> smallList)
    {


        foreach (GameObject obj in enemiesClear)
        {
            if (!bigList.Contains(obj))
            {
                enemiesLeft.Add(obj);
            }
        }

        return enemiesLeft;
    }

    public void HandleEnemyChange(GameObject obj)
    {
        enemiesClear.Add(obj);
       // Debug.Log(enemiesClear.Count + "/" + taskEnemieCount);
        GetObjectsNotInList(taskEnemies, enemiesClear);
        if (enemiesClear.Count == taskEnemieCount && isAllSpawnerEnable == true)
        {

            OnMove?.Invoke();
            EnableNextTrigger();
        }
    }

    public void CheckTriggerState(int value, int total)
    {

        if (value == total)
        {

            LockCamera();
        }
    }

    private void LockCamera()
    {
        OnLock?.Invoke();
        Debug.Log("cam move false");

    }
    public void RemoveTrigger(GameObject obj)
    {
        enemyTriggers.Remove(obj.transform);
    }

    public void AddTrigger(GameObject obj)
    {
        enemyTriggers.Add(obj.transform);
    }

    private void EnableNextTrigger()
    {
        //Debug.Log("EnableNextTrigger");
        taskEnemieCount = 0;
        enemiesClear.Clear();
        currentIndex++;
        if (currentIndex < enemyTriggers.Count)
        {
            enemyTriggers[currentIndex].gameObject.SetActive(true);

            EnemySpawnTrigger spawnTrigger = enemyTriggers[currentIndex].GetComponent<EnemySpawnTrigger>();
            if (spawnTrigger != null)
            {
                spawnTrigger.GetAllEnemyCurrentWave();
            }
        }
    }

}
