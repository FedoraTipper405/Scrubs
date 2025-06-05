using System.Collections.Generic;
using UnityEngine;

public class EnemyCamper : EnemyBaseController
{
    [SerializeField] GameObject bulletPerfab;
    [Header("Shoot")]
    [SerializeField] float shootCooldown = 1.5f;
    private float shootTimer = 0f;
    private Bullet bullet;
    [SerializeField] GameObject bulletStartPoint;
    private Shooter shooter;
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        shooter = GetComponent<Shooter>();

    }
    protected override void Start()
    {
        base.Start();
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);

        if (gameObject != null && player != null)
        {
            transform.position = player.position + new Vector3(4f, 0, 0);
        }

    }
    protected override void Update()
    {
        base.Update();
        shootTimer -= Time.deltaTime;
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (shootTimer > 0f) return;
        shootTimer = shootCooldown;
        GameObject newBullet = Instantiate(bulletPerfab, bulletStartPoint.transform.position, Quaternion.identity);
        bullet = newBullet.GetComponentInChildren<Bullet>();
        Vector3 direction = (player.position + new Vector3(0, 1, 0) - bulletStartPoint.transform.position).normalized;
        bullet.Shoot(direction, shooter.damageAmount);
    }
}
