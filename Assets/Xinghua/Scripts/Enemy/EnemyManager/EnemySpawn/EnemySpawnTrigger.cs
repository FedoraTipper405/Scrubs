using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    //manage the enemy in the scene and the trigger for spawn enemy
    public static EnemySpawnManager Instance;
    public List<GameObject> triggerColliders;
    public int currentIndex = 0;
    [HideInInspector]
    public List<GameObject> enemiesInTheScene;
    [SerializeField] EnemyAttackManager enemyAttackManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

    }
    private void Start()
    {
        for (int i = 0; i < triggerColliders.Count; i++)
        {
            if (i == 0)
            {
                triggerColliders[i].SetActive(true);
            }
            else
            {
                triggerColliders[i].SetActive(false);
            }
        }
        enemiesInTheScene = new List<GameObject>();
    }

 
    public void CheckEnemyNumberInTheScene()
    {
        if (enemiesInTheScene.Count <= 1)
        {
            EnableNextTrigger();
        }
    }

    private void EnableNextTrigger()
    {
        currentIndex++;
        if (currentIndex < triggerColliders.Count)
        {
            triggerColliders[currentIndex].SetActive(true);
        }

    }
}
