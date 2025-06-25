using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTriggerManager : MonoBehaviour
{
    public static EnemyTriggerManager Instance;
    private List<Transform> enemyTriggers = new List<Transform>();
    public Transform enemyParent;
    [Header("Camera related")]
    public UnityEvent OnMove;
    public UnityEvent OnLock;


    public int taskEnemieCount;
    public List<GameObject> taskEnemies = new List<GameObject>();
    public int enemiesKilled;

  //  bool isAllSpawnerEnable = true;
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
                    spawnTrigger.SetTaskEnemyInCurrentWave();
                }
            }
            else
            {
                childGameObject.SetActive(false);
            }
        }
    }
    public void HandleEnemyChangeWithCamera(bool spawn, bool die)
    {
        if (spawn == true)
        {
            LockCamera();

        }
        if (die == true)
        {
            enemiesKilled++;
            LockCamera();
        }
        if (enemiesKilled >= taskEnemieCount)
        {
            MenuManager.Instance.ShowGo();
            OnMove?.Invoke();
            EnableNextTrigger();
            //enemiesClear.Clear();
        } 
    }


    public void LockCamera()
    {
       
        OnLock?.Invoke();
    }

    private void EnableNextTrigger()
    {
        taskEnemieCount = 0;
        taskEnemies.Clear();
        EnemyAttackManager.Instance.currentAttackers.Clear();
        EnemyAttackManager.Instance.ResetAttacterPercent();
        currentIndex++;
        if (currentIndex < enemyTriggers.Count)
        {
            enemyTriggers[currentIndex].gameObject.SetActive(true);

            EnemySpawnTrigger spawnTrigger = enemyTriggers[currentIndex].GetComponent<EnemySpawnTrigger>();
            if (spawnTrigger != null)
            {
                //  triggeredCount = 0;
                spawnTrigger.SetTaskEnemyInCurrentWave();
            }
        }
        else
        {
            Debug.Log("Last enemy wave");
        }
    }

}
