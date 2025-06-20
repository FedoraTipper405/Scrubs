using UnityEngine;

[CreateAssetMenu(fileName ="NewEnemyData",menuName ="Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int damage;
    public int maxHealth;
    public float moveSpeed;
    public float attackRange;
    public float pacingRadius;
    public float stopDistance;
    public GameObject enemyPrefab;
    public float attackCooldown;
    public float dropHealItemChance;
    public float dropMoneyChance;
    public bool canDrop;
}
