using System.Collections.Generic;
using UnityEngine;
using static FlyBoss;

public class Shooter : MonoBehaviour
{
    // the fly boss and camper share this code
    [SerializeField] GameObject bulletPerfab;
    [SerializeField] Transform bulletStartPoint;
    [SerializeField] private int maxBulletNum;
    private Bullet bullet;
    public float shootCooldown = 1.5f;
    private float shootTimer = 0f;
    public float damageAmount;
    public List<GameObject> bulletPool = new List<GameObject>();
    private FlyBoss flyBoss;


    [SerializeField] private float shootSpeed;
    private void Awake()
    {
        flyBoss = GetComponent<FlyBoss>();
        bullet = GetComponent<Bullet>();
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
    }
    public void ResetBulletNum()
    {
        bulletPool.Clear();
    }
    public void BossShoot(float bulletScale)
    {
        if (shootTimer > 0f) return;
        shootTimer = shootCooldown;
       
        GameObject newBullet = Instantiate(bulletPerfab, bulletStartPoint.position, Quaternion.identity);
       
        bullet = newBullet.GetComponentInChildren<Bullet>();
        bullet.transform.localScale = new Vector3(bulletScale, bulletScale, bulletScale);
        bulletPool.Add(newBullet);
        if (bulletPool.Count >= maxBulletNum)
        {
            flyBoss.SetCurrentState(FlyBossState.Recovering);
            flyBoss.landPosition = flyBoss.player.transform.position + new Vector3(2, 0, 0);
        }
        Vector3 direction = (flyBoss.player.position + new Vector3(0, 1, 0) - bulletStartPoint.transform.position).normalized;
        bullet.Shoot(direction, damageAmount, shootSpeed);
    }
}
