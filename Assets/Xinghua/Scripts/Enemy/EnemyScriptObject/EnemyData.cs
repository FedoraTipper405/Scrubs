using UnityEngine;

[CreateAssetMenu(fileName ="NewEnemyData",menuName ="Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    public bool canMove;
    public int maxHealth;
    public float avoidDistance;
    [Header("AI")]
    public int damage;
    public float moveSpeed;
    public float paceSpeed;
    public float attackRange;
    public float attackCooldown;
    public float pacingRadius;
    public float maxPacingTime;
    public float knockBackStrength;
    [Header("drop")]
    public bool canDrop;
    public GameObject coinPrefab;
    public GameObject healthPrefab;
    public float dropHealItemChance;
    public float dropMoneyChance;
    
}
