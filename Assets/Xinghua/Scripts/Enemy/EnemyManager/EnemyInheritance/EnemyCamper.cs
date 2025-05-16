using System.Collections.Generic;
using UnityEngine;
using static EnemyAI;

public class EnemyCamper : EnemyBaseController
{
    [SerializeField] GameObject bulletPerfab;
    private List<GameObject> bulletPool = new List<GameObject>();
    [Header("Shoot")]
    [SerializeField] float shootCooldown = 1.5f;
    private float shootTimer = 0f;
    private Bullet bullet;
    [SerializeField] GameObject bulletStartPoint;
    protected override void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);

        transform.position = player.position + new Vector3(4f, 0, 0);
    }

    protected override void Update()
    {
        switch (enemyAI.currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break;

        }
        shootTimer -= Time.deltaTime;
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (shootTimer > 0f) return;
        shootTimer = shootCooldown;
        GameObject newBullet = Instantiate(bulletPerfab,bulletStartPoint.transform.position, Quaternion.identity);
        bulletPool.Add(newBullet);
        bullet = newBullet.GetComponentInChildren<Bullet>();
        Vector3 direction = (player.position + new Vector3(0, 1, 0) - bulletStartPoint.transform.position).normalized;
        bullet.Shoot(direction);
    }
 

}
