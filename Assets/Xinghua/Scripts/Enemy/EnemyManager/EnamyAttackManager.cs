using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager Instance;
    public int maxAttackers = 2;
    private List<EnemyAI> currentAttackers = new List<EnemyAI>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool TryRequestAttack(EnemyAI enemy)
    {
        if (currentAttackers.Contains(enemy)) return true;

        if (currentAttackers.Count < maxAttackers)
        {
            Debug.Log($"{enemy.name} begin attack");
            currentAttackers.Add(enemy);
            return true;
        }
        Debug.Log($"{enemy.name} is refused attack");
        return false;
    }

    public void StopAttack(EnemyAI enemy)
    {
        if (currentAttackers.Contains(enemy))
        {
            currentAttackers.Remove(enemy);
        }
    }
}
