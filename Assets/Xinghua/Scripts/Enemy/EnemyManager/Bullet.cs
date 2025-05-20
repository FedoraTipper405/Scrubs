using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 moveDirection;
    private EnemyBaseController enemyOwner;
    public void Shoot(Vector3 direction, GameObject owner)
    {
        direction.z = 0;
        moveDirection = direction;
        enemyOwner = owner.GetComponent<EnemyBaseController>();
        Destroy(gameObject, 3f);
    }
    private void FixedUpdate()
    {
        transform.position += moveDirection * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player != null && enemyOwner != null)
        {
            player.TakeDamage(enemyOwner.enemyData.damage);
            Destroy(gameObject);
        }

    }
}
