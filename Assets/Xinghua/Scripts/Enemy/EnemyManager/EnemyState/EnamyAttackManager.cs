using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{

    public int maxAttackers = 3;
    [HideInInspector]
    public List<GameObject> currentAttackers = new List<GameObject>();
    private EnemySpawner triggerController;
    void Awake()
    {
        triggerController = GetComponentInParent<EnemySpawner>();
    }

    private void Update()
    {
        if (currentAttackers.Count <= 1 && EnemyTriggerManager.Instance.enemiesInTheScene.Count >= 1)
        {
            SetCurrentAttacker();
        }
    }
    public void SetCurrentAttacker()
    {
        var result = GetRandonAttackerIndex(EnemyTriggerManager.Instance.enemiesInTheScene.Count, maxAttackers);

        foreach (var index in result)
        {
            var enemy = EnemyTriggerManager.Instance.enemiesInTheScene[index].gameObject;
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
