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

    public int enemyWaveLeft;
    public List<GameObject> enemiesInTheScene;
    private bool hasTriggeredNext = false;
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

    

    private void Update()
    {
        Debug.Log("left" + enemyWaveLeft);
        if (enemyWaveLeft <= 0 && !hasTriggeredNext)
        {
            hasTriggeredNext = true;
            EnableNextTrigger();
            Debug.Log("cam can move true");
            OnLock?.Invoke();
        }
        else if (enemyWaveLeft > 0)
        {
            hasTriggeredNext = false; 
            Debug.Log("cam move false");
            OnMove?.Invoke();
        }
    }
    public void CheckEnemyNumberInTheScene()
    {
        if (enemiesInTheScene.Count <= 1)
        {
            EnableNextTrigger();
        }

    }
    public void AddTrigger(GameObject obj)
    {
        enemyTriggers.Add(obj.transform);
        Debug.Log("triggerCollider :" + enemyTriggers.Count);
    }
    private void EnableNextTrigger()
    {
        hasTriggeredNext = true;
        currentIndex++;
        if (currentIndex < enemyTriggers.Count)
        {

            enemyTriggers[currentIndex].gameObject.SetActive(true);
        }
    }

}
