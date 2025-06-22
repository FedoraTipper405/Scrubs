using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAI;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager Instance;
    public List<GameObject> currentAttackers = new List<GameObject>();
    public List<GameObject> potentialAttackers = new List<GameObject>();

    public float minPercent = 0.4f;
    public float maxPercent = 0.8f;
    private float attacterPercent;
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
        attacterPercent = 0.4f;
    }
    private void FixedUpdate()
    {
        if (potentialAttackers.Count != 0)
        {
            GetCurrentAttacker();
        }
        HandleAttack();
        SetAttackerState();
    }
    public void ResetAttacterPercent()
    {
        attacterPercent = Random.Range(minPercent, maxPercent);
    }
    private List<GameObject> GetCurrentAttacker()
    {
        float random = Random.Range(minPercent, maxPercent);
        int percentNum = (int)(random * potentialAttackers.Count);

        if (currentAttackers.Count >= percentNum)
        {
            return currentAttackers;
        }

        for (int i = 0; i < percentNum ; i++)
        {
            var attacker = potentialAttackers[percentNum];
          
            AddAttacker(attacker);
        }
        return currentAttackers;
    }
    public void AddAttacker(GameObject obj)
    {
        if (!currentAttackers.Contains(obj))
        {
            currentAttackers.Add(obj);
        }
    }
    public void RemoveAttacker(GameObject obj)
    {
        if (currentAttackers.Contains(obj))
        {
            currentAttackers.Remove(obj);
        }
    }
    public void HandleAttack()//out of the range 
    {
        if (potentialAttackers.Count <= 4)
        {
            for (int i = 0; i < potentialAttackers.Count; i++)
            {
                AddAttacker(potentialAttackers[i]);
            }
        }
        else if (currentAttackers.Count >= (int)potentialAttackers.Count * maxPercent)
        {
            for (int i = 0; i < (int)currentAttackers.Count* minPercent; i++)
            {
                RemoveAttacker(currentAttackers[i]);
            }
        }
    }
    public void SetAttackerState()
    {
        GetCurrentAttacker();
        if (currentAttackers.Count <=  0)return;
        foreach (var obj in currentAttackers)
        {
           
            if (obj != null)
            {
                EnemyAI enemyAI = obj.GetComponent<EnemyAI>();
                enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);
            }
            else
            {
                Debug.Log("enemyAI not found");
            }
        }
    }
    public void RemoveAttackerState()
    {
        if (currentAttackers.Count <= 0) return;
        for (int i = 0; i < currentAttackers.Count; i++)
        {
            if (currentAttackers[i] != null )
            {
                EnemyAI enemyAI = currentAttackers[i].GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.SetEnemyState(EnemyAI.EnemyState.Pacing);
                    currentAttackers.Remove(currentAttackers[i]);
                }
            }
            else
            {
                Debug.Log("enemyAI not found");
            }
        }
    }
/*    public List<int> GetRandonAttackerIndex(int length, int number)
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
    }*/
}
