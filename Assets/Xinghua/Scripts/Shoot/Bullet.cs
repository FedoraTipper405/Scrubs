using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 moveDirection;
    private float damage;//different shooter have different damage; for example boss and camper both can shoot ,the damage is different
    public void Shoot(Vector3 direction,float amount)
    {
        SoundManager.Instance.PlaySFX("Shoot",0.6f);
        direction.z = 0;
        moveDirection = direction;
        damage = amount;
        Destroy(gameObject, 3f);
    }
    private void FixedUpdate()
    {
        transform.position += moveDirection * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponentInChildren<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
