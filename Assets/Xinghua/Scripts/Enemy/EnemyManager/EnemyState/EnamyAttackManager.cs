using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager Instance;
    public int maxAttackers = 3;
    [HideInInspector]
    public List<GameObject> currentAttackers = new List<GameObject>();
    private EnemySpawnManager triggerController;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        triggerController = GetComponentInParent<EnemySpawnManager>();

    }
    private void FixedUpdate()
    {
        if(currentAttackers.Count <= 1 && EnemySpawnManager.Instance.enemiesInTheScene.Count >=1)
        {
            SetCurrentAttacker();
        }
    }
    public void SetCurrentAttacker()
    {
        var result = GetRandonAttackerIndex(EnemySpawnManager.Instance.enemiesInTheScene.Count, maxAttackers);
        foreach (var index in result)
        {
            var enemy = EnemySpawnManager.Instance.enemiesInTheScene[index].gameObject;
            if (currentAttackers.Contains(enemy)) continue;
            currentAttackers.Add(enemy.gameObject);
            var enemyAI = enemy.gameObject.GetComponent<EnemyAI>();
            enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);
        }
    }

    public List<int> GetRandonAttackerIndex(int length, int number)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < length; i++)
        {
            int numberToAttack = UnityEngine.Random.Range(0, length);
            if (result.Contains(numberToAttack))
            {
                continue;
            }
            result.Add(numberToAttack);
            if (result.Count == number)
            {
                break;
            }
        }
        return result;
    }

    public void StopAttack(GameObject enemy)
    {
        if (currentAttackers.Contains(enemy))
        {
            currentAttackers.Remove(enemy);
        }
    }
}
