using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager Instance;
    public int maxAttackers = 3;
    //[HideInInspector]
    public List<GameObject> currentAttackers = new List<GameObject>();
    private Transform enemyHolder;
    private Transform attackerHolder;
    private Transform enemyAttacker;
    public int currentAttackNumber = 0;
    int potentialAttackerCount = 0;
    //public List<GameObject> potentialAttackers = new List<GameObject>();

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
        enemyHolder = transform.GetChild(0);
    }
    private void FixedUpdate()
    {
        if (enemyHolder.childCount !=0)
        {
            SetCurrentAttacker();
        } 
    }

    public void SetCurrentAttacker()
    {
        if(currentAttackNumber >= maxAttackers)
        {
            return;
        }
     
        for (int i = 0; i < maxAttackers; i++)
        {
            int random = Random.Range(0, enemyHolder.childCount);
            var enemy = enemyHolder.GetChild(random);
            var obj = enemy.gameObject;
            currentAttackers.Add(obj);
          
            SetAttackerState(obj);
            currentAttackNumber++;
        }
    }

    public void SetAttackerState(GameObject obj)
    {
        EnemyAI enemyAI = obj.GetComponent<EnemyAI>();

        if (enemyAI != null)
        {
            enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);
        }
        else
        {
            Debug.Log("enemyAI not found");
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
}
