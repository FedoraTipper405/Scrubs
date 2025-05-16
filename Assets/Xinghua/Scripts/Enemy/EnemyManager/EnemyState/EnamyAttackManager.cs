using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager Instance;
    public int maxAttackers = 3;
    private List<GameObject> currentAttackers = new List<GameObject>();
    [SerializeField] private List<EnemyBaseController> enemyList;
    private TriggerController triggerController;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        triggerController = GetComponentInParent<TriggerController>();

    }
  
    public void SetCurrentAttacker()
    {
        int numberToAttack = Random.Range(1, maxAttackers); 

        int availableCount = Mathf.Min(numberToAttack, enemyList.Count);

        for (int i = 0; i < availableCount; i++)
        {
            int randomIndex = Random.Range(0, enemyList.Count);
            EnemyBaseController chosenEnemy = enemyList[randomIndex];

            if (chosenEnemy != null && chosenEnemy.GetComponent<EnemyAI>() != null)
            {
                chosenEnemy.GetComponent<EnemyAI>().SetEnemyState(EnemyAI.EnemyState.Attack);
            }
        }
    }
    public bool TryRequestAttack(GameObject enemy)
    {
        if (currentAttackers.Contains(enemy)) return true;

        if (currentAttackers.Count < maxAttackers)
        {
            currentAttackers.Add(enemy);
            return true;
        }
        Debug.Log($"{enemy.name} is refused attack");
        return false;
    }

    public void StopAttack(GameObject enemy)
    {
        if (currentAttackers.Contains(enemy))
        {
            currentAttackers.Remove(enemy);
        }
    }
}
