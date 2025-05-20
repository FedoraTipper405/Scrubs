using UnityEngine;

public class Attack : MonoBehaviour
{
    private PlayerHealth player;
    private EnemyBaseController enemy;
    private float faceToFaceDistance = 0.2f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject.GetComponent<PlayerHealth>();
        enemy = gameObject.GetComponentInParent<EnemyBaseController>();
        if (player != null)
        {
            if (Mathf.Abs(player.transform.position.y - transform.position.y) <= faceToFaceDistance)
            {
                player.TakeDamage(enemy.enemyData.damage);
            }
        }
    }
}
