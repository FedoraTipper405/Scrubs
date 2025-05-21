using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager Instance;
    public int maxAttackers = 3;
    [HideInInspector]
    public List<GameObject>currentAttackers = new List<GameObject>();
   // private EnemySpawner triggerController;
  /*  void Awake()
    {
        triggerController = GetComponentInParent<EnemySpawner>();
    }*/
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetAttacker()
    {
        var result = GetRandonAttackerIndex(EnemyTriggerManager.Instance.enemiesLeft.Count, maxAttackers);

        foreach (var index in result)
        {
            if (EnemyTriggerManager.Instance != null)
            {
                var enemy = EnemyTriggerManager.Instance.enemiesClear[index];
                var obj = enemy.GameObject();
                if (currentAttackers.Contains(obj)) continue;
                currentAttackers.Add(obj);
                SetCurrentAttacker(obj);
            }
        }
    }
    public void SetCurrentAttacker(GameObject obj)
    {
        var enemyAI = obj.GetComponent<EnemyAI>();

        if (enemyAI != null)
        {
            enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);
        }
    }

    public List<int> GetRandonAttackerIndex(int length, int number)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < length; i++)
        {
            int numberToAttack = Random.Range(0, length);
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
