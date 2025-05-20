using System.Collections;
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
    // public List<EnemyData> enemiesInTheScene;

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
            }
            else
            {
                childGameObject.SetActive(false);
            }
        }
    }
    public List<GameObject> GetObjectsNotInList(List<GameObject> bigList, List<GameObject> smallList)
    {
        List<GameObject> result = new List<GameObject>();

        foreach (GameObject obj in enemiesClear)
        {
            if (!bigList.Contains(obj))
            {
                result.Add(obj);
            }
        }

        return result;
    }
    public void HandleEnemyChange(GameObject obj)
    {

        enemiesClear.Add(obj);
        GetObjectsNotInList(taskEnemies, enemiesClear);
        {
            //Debug.Log( enemiesClear.Count+ "/" +taskEnemieCount );
            if (enemiesClear.Count >= taskEnemieCount)
            {
                //Debug.Log("cam can move true");
                OnMove?.Invoke();
                EnableNextTrigger();
            }
            StartCoroutine(LockCamera());
        }
    }
    private IEnumerator LockCamera()
    {
        if (enemiesClear.Count >0) 
        {
            OnLock?.Invoke();
           // Debug.Log("cam move false");
            yield return new WaitForSeconds(12f);
        }
        else
        {
            yield return null;
        }
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
