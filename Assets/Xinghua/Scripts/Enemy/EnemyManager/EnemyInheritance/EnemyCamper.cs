using UnityEngine;

public class EnemyCamper : EnemyBaseController
{
    [SerializeField] GameObject bulletPerfab;
    [Header("Shoot")]
    [SerializeField] float shootCooldown = 1.5f;
    [SerializeField] private float shootSpeed = 10f;
    private float shootTimer = 0f;
    private Bullet bullet;
    [SerializeField] GameObject bulletStartPoint;
    private Shooter shooter;
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        shooter = GetComponent<Shooter>();
        enemyRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.SetEnemyState(EnemyAI.EnemyState.Attack);
        SetPosiiton();
    }
    private void SetPosiiton()
    {

        float randomX = Random.Range(-6f, 0f);
        float randomY = Random.Range(-1f, 1f);
        Vector3 spawnPos = transform.position + new Vector3(randomX, randomY, 0f);

        transform.position = spawnPos;
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
        bullet.Shoot(direction, shooter.damageAmount, shootSpeed);
        SoundManager.Instance.PlaySFX("Shoot", 0.6f);
    }
}
