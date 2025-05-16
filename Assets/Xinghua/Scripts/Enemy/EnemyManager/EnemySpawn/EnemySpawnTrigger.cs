using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    //manage the enemy in the scene and the trigger for spawn enemy
    public List<GameObject> triggerColliders;
    public int currentIndex = 0;
    [HideInInspector]
    public List<GameObject> enemiesLeft;
    [SerializeField]EnemyAttackManager enemyAttackManager;
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
        enemiesLeft = new List<GameObject>();
    }

    private void Update()
    {
        if (enemiesLeft.Count <= 0)
        {
            EnableNextTrigger();
            enemyAttackManager.SetCurrentAttacker();
        }
    }

    private void EnableNextTrigger()
    {
        currentIndex++;

        if (currentIndex < triggerColliders.Count)
        {
            triggerColliders[currentIndex].SetActive(true);
        }

        this.enabled = false;
    }
}
